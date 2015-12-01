#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;
#endregion

namespace GameEngine.Labs
{
    public class Lab12 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Lab12()
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
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin();
            // Any 2D stuff goes here!
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
