using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.UI
{
    public class Element : IUpdateable, IDrawable
    {
        public delegate void ActionHandler(Element sender);

        public event ActionHandler Action;
        protected void OnAction()
        {
            if (Action != null) Action(this);
        }

        public Rectangle Bounds { get; set; }
        public Texture2D Texture { get; set; }
        public bool Selected { get; set; }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}
