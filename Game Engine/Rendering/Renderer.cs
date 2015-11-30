using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Renderer : Component, IRenderable
    {
        public Material Material { get; set; }

        public virtual void Draw() {}
    }
}
