#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.AI;
using System.Collections.Generic;
using System;
#endregion

namespace GameEngine.Labs
{
    public class Lab09 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        AStarSearch search;
        List<Vector3> path;
        Random random;

        Camera camera;
        Model plane;
        Model sphere;

        public Lab09()
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

            random = new Random();
            search = new AStarSearch(100, 100);
            foreach (AStarNode node in search.Nodes)
                if (random.NextDouble() < 0.2)
                    search.Nodes[random.Next(100), random.Next(100)].Passable = false;
            path = new List<Vector3>();
            FindRandomPath();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            plane = Content.Load<Model>("Models/Plane");
            sphere = Content.Load<Model>("Models/Sphere");

            GameObject gameObject = new GameObject();
            gameObject.Transform.LocalPosition = Vector3.One * 50;
            gameObject.Transform.Rotate(Vector3.Right, -MathHelper.PiOver2);
            camera = gameObject.Add<Camera>();
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();
            if (Input.IsKeyPressed(Keys.Space))
                FindRandomPath();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix view = camera.View;
            Matrix projection = camera.Projection;

            (plane.Meshes[0].Effects[0] as BasicEffect).DiffuseColor = Color.DarkBlue.ToVector3();
            plane.Draw(Matrix.CreateScale(55, 1, 55) * Matrix.CreateTranslation(50, -5, 50), view, projection);
            (sphere.Meshes[0].Effects[0] as BasicEffect).DiffuseColor = Color.DarkRed.ToVector3();
            foreach (AStarNode node in search.Nodes)
                if (!node.Passable)
                    sphere.Draw(Matrix.CreateScale(0.5f, 0.05f, 0.5f) * Matrix.CreateTranslation(node.Position), view, projection);
            (sphere.Meshes[0].Effects[0] as BasicEffect).DiffuseColor = Color.WhiteSmoke.ToVector3();
            foreach (Vector3 position in path)
                sphere.Draw(Matrix.CreateScale(0.1f, 0.1f, 0.1f) * Matrix.CreateTranslation(position), view, projection);

            //spriteBatch.Begin();
            // Any 2D stuff goes here!
            //spriteBatch.End();
            base.Draw(gameTime);
        }

        void FindRandomPath()
        {
            while (!(search.Start = search.Nodes[random.Next(search.Cols), random.Next(search.Rows)]).Passable) ;
            while (!(search.End = search.Nodes[random.Next(search.Cols), random.Next(search.Rows)]).Passable) ;
            search.Search();
            path.Clear();
            AStarNode current = search.End;
            while (current != null)
            {
                path.Insert(0, current.Position);
                current = current.Parent;
            }
        }
    }
}
