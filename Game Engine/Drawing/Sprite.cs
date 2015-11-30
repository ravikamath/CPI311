﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Sprite : IUpdateable, IDrawable
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Source { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects Effect { get; set; }
        public float Depth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Position = Vector2.Zero;
            Width = Texture.Width;
            Height = Texture.Height;
            Source = new Rectangle(0, 0, Width, Height);
            Color = Color.White;
            Rotation = 0;
            Origin = new Vector2(Width / 2, Height / 2);
            Scale = Vector2.One;
            Effect = SpriteEffects.None;
            Depth = 0;
        }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color,
                Rotation, Origin, Scale, Effect, Depth);
        }

    }
}
