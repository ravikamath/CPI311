﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;
#endregion

namespace GameEngine.Labs
{
    public class Lab04 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model model;
        Transform parentTransform;
        Transform childTransform;
        Transform cameraTransform;
        Camera camera;

        public Lab04()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Time.Initialize();
            Input.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Let's load our model
            model = Content.Load<Model>("Models/Torus");
            // Ask our model to do "default" lighting"
            foreach (ModelMesh mesh in model.Meshes)
                foreach (BasicEffect effect in mesh.Effects)
                    effect.EnableDefaultLighting();
            parentTransform = new Transform();
            childTransform = new Transform();
            childTransform.Parent = parentTransform;
            childTransform.LocalPosition = Vector3.Right * 10;
            cameraTransform = new Transform();
            cameraTransform.LocalPosition = Vector3.Backward * 50;
            camera = new Camera();
            //camera.Transform = cameraTransform;
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();

            // Keep rotating my child object
            childTransform.Rotate(Vector3.Right, Time.ElapsedGameTime);
            // Scale the parent if Shift+Up/Down is pressed
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                if (Input.IsKeyDown(Keys.Up))
                    parentTransform.LocalScale += Vector3.One * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Down))
                    parentTransform.LocalScale -= Vector3.One * Time.ElapsedGameTime;
            }
            else if (Input.IsKeyDown(Keys.LeftControl))
            {
                if (Input.IsKeyDown(Keys.Right))
                    parentTransform.Rotate(Vector3.Forward, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Left))
                    parentTransform.Rotate(Vector3.Backward, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Up))
                    parentTransform.Rotate(Vector3.Right, Time.ElapsedGameTime * 5);
                if (Input.IsKeyDown(Keys.Down))
                    parentTransform.Rotate(Vector3.Left, Time.ElapsedGameTime * 5);
            }
            // Otherwise, move the parent with respect to its axes
            else
            {
                if (Input.IsKeyDown(Keys.Right))
                    parentTransform.LocalPosition += parentTransform.Right * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Left))
                    parentTransform.LocalPosition += parentTransform.Left * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Up))
                    parentTransform.LocalPosition += parentTransform.Up * Time.ElapsedGameTime;
                if (Input.IsKeyDown(Keys.Down))
                    parentTransform.LocalPosition += parentTransform.Down * Time.ElapsedGameTime;
            }

            // Control the camera
            if (Input.IsKeyDown(Keys.W)) // move forward
                cameraTransform.LocalPosition += cameraTransform.Forward * Time.ElapsedGameTime * 5;
            if (Input.IsKeyDown(Keys.S)) // move backwars
                cameraTransform.LocalPosition += cameraTransform.Backward * Time.ElapsedGameTime * 5;
            if (Input.IsKeyDown(Keys.A)) // rotate left
                cameraTransform.Rotate(Vector3.Up, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.D)) // rotate right
                cameraTransform.Rotate(Vector3.Down, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.Q)) // look up
                cameraTransform.Rotate(Vector3.Right, Time.ElapsedGameTime);
            if (Input.IsKeyDown(Keys.E)) // look down
                cameraTransform.Rotate(Vector3.Left, Time.ElapsedGameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.DepthStencilState = new DepthStencilState();
            Matrix view = camera.View;
            Matrix projection = camera.Projection;
            model.Draw(parentTransform.World, view, projection);
            model.Draw(childTransform.World, view, projection);

            spriteBatch.Begin();
            // Any 2D stuff goes here!
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
