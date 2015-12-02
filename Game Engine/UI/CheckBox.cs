using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.UI
{
    public class CheckBox : Element
    {
        public SpriteFont Font { get; set; }
        public String Text { get; set; }
        public Texture2D Box { get; set; }
        public bool Checked { get; set; }

        public override void Update()
        {
            if (Input.IsMouseReleased(0) &&
                    Bounds.Contains(Input.MousePosition))
            {
                Checked = !Checked;
                OnAction();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            int width = Math.Min(Bounds.Width, Bounds.Height);
            spriteBatch.Draw(Box,
                new Rectangle(Bounds.X, Bounds.Y, width, width),
                Checked ? Color.Red : Color.White);
            spriteBatch.DrawString(Font, Text,
                new Vector2(Bounds.X + width, Bounds.Y), Color.Black);
        }

    }
}
