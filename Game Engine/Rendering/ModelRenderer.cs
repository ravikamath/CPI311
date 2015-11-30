using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class ModelRenderer : Renderer
    {
        public Model Model { get; set; }

        public ModelRenderer(Model model)
        {
            Model = model;
        }

        public ModelRenderer() : this(null) { }

        public override void Draw()
        {
            if (Model == null)
                return;
            if (Material == null)
                Model.Draw(Transform.World, Camera.Current.View, Camera.Current.Projection);
            else
            {
                Matrix[] transforms = new Matrix[Model.Bones.Count];
                Model.CopyAbsoluteBoneTransformsTo(transforms);
                Matrix world = Transform.World;
                foreach (ModelMesh mesh in Model.Meshes)
                {
                    Material.Apply(transforms[mesh.ParentBone.Index] * world);
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        Screen.GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                        Screen.GraphicsDevice.Indices = part.IndexBuffer;
                        Screen.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.StartIndex, part.PrimitiveCount);
                    }
                }
            }
        }
    }
}
