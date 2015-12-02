using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GameEngine.UI
{
    public class Button : Element
    {
        public SpriteFont Font { get; set; }
        public String Text { get; set; }

        public override void Update()
        {
            if (Input.IsMouseReleased(0) &&
                    Bounds.Contains(Input.MousePosition))
                OnAction();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, Text,
                new Vector2(Bounds.X, Bounds.Y), Color.Black);
        }
    }
}
