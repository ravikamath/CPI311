#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;
#endregion

namespace GameEngine.Labs
{
    public class Lab05 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model model;
        GameObject parentObject;
        GameObject childObject;
        GameObject cameraObject;
        Texture2D texture;
        Camera camera;
        Effect effect;

        public Lab05()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Time.Initialize();
            Input.Initialize();
            Screen.Initialize(graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Let's load our model
            model = Content.Load<Model>("Models/Torus");
            parentObject = new GameObject();
            childObject = new GameObject();
            childObject.Transform.Parent = parentObject.Transform;
            childObject.Transform.LocalPosition = Vector3.Right * 10;
            cameraObject = new GameObject();
            cameraObject.Transform.LocalPosition = Vector3.Backward * 50;
            camera = cameraObject.Add<Camera>();
            texture = Content.Load<Texture2D>("Textures/Square");
            effect = Content.Load<Effect>("Effects/SimpleShading");
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();

            // Keep rotating my child object
            childObject.Transform.Rotate(Vector3.Right, Time.ElapsedGameTime);
            // Scale the parent if Shift+Up/Down is pressed
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                if (Input.IsKeyDown(Keys.Up))
                    parentObject.Transform.LocalScale += Vector3.One * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Down))
                    parentObject.Transform.LocalScale -= Vector3.One * Time.ElapsedGameTime;
            }
            else if (Input.IsKeyDown(Keys.LeftControl))
            {
                if (Input.IsKeyDown(Keys.Right))
                    parentObject.Transform.Rotate(Vector3.Forward, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Left))
                    parentObject.Transform.Rotate(Vector3.Backward, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Up))
                    parentObject.Transform.Rotate(Vector3.Right, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Down))
                    parentObject.Transform.Rotate(Vector3.Left, Time.ElapsedGameTime * 5);
            }
            // Otherwise, move the parent with respect to its axes
            else
            {
                if (Input.IsKeyDown(Keys.Right))
                    parentObject.Transform.LocalPosition += parentObject.Transform.Right * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Left))
                    parentObject.Transform.LocalPosition += parentObject.Transform.Left * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Up))
                    parentObject.Transform.LocalPosition += parentObject.Transform.Up * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Down))
                    parentObject.Transform.LocalPosition += parentObject.Transform.Down * Time.ElapsedGameTime;
            }

            // Control the camera
            if (Input.IsKeyDown(Keys.W)) // move forward
                cameraObject.Transform.LocalPosition += cameraObject.Transform.Forward * Time.ElapsedGameTime * 5;
            if (Input.IsKeyDown(Keys.S)) // move backwars
                cameraObject.Transform.LocalPosition += cameraObject.Transform.Backward * Time.ElapsedGameTime * 5;
            if (Input.IsKeyDown(Keys.A)) // rotate left
                cameraObject.Transform.Rotate(Vector3.Up, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.D)) // rotate right
                cameraObject.Transform.Rotate(Vector3.Down, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.Q)) // look up
                cameraObject.Transform.Rotate(Vector3.Right, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.E)) // look down
                cameraObject.Transform.Rotate(Vector3.Left, Time.ElapsedGameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.DepthStencilState = new DepthStencilState();
            Matrix view = camera.View;
            Matrix projection = camera.Projection;
            //model.Draw(parentObject.Transform.World, view, projection);
            model.Draw(childObject.Transform.World, view, projection);

            effect.CurrentTechnique = effect.Techniques[1];
            effect.Parameters["World"].SetValue(parentObject.Transform.World);
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["LightPosition"].SetValue(Vector3.Backward * 10 + Vector3.Right * 5);
            effect.Parameters["CameraPosition"].SetValue(cameraObject.Transform.Position);
            effect.Parameters["Shininess"].SetValue(20f);
            effect.Parameters["AmbientColor"].SetValue(new Vector3(0.2f, 0.2f, 0.2f));
            effect.Parameters["DiffuseColor"].SetValue(new Vector3(0.5f, 0, 0));
            effect.Parameters["SpecularColor"].SetValue(new Vector3(0, 0, 0.5f));
            effect.Parameters["DiffuseTexture"].SetValue(texture);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                foreach (ModelMesh mesh in model.Meshes)
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                        GraphicsDevice.Indices = part.IndexBuffer;
                        GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.StartIndex, part.PrimitiveCount);
                    }
            }
            //spriteBatch.Begin();
            // Any 2D stuff goes here!
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
