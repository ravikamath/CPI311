using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameEngine.UI
{
    public class Group : Element
    {
        public List<Element> Children { get; set; }
        public int SelectedIndex { get; set; }

        public Group()
        {
            Children = new List<Element>();
            SelectedIndex = 0;
        }

        public override void Update()
        {
            if (Children.Count > 0)
            {
                int newIndex = SelectedIndex;
                if (Input.IsKeyPressed(Keys.Up))
                    newIndex = (SelectedIndex + Children.Count - 1) % Children.Count;
                if (Input.IsKeyPressed(Keys.Down))
                    newIndex = (SelectedIndex + 1) % Children.Count;
                if (newIndex != SelectedIndex)
                {
                    Children[SelectedIndex].Selected = false;
                    Children[SelectedIndex = newIndex].Selected = true;
                }
            }
            foreach (Element child in Children)
                child.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Element child in Children)
                child.Draw(spriteBatch);
        }

    }
}
