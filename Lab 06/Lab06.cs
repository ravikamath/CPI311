#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
#endregion

namespace GameEngine.Labs
{
    public class Lab06 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Model model;
        Random random;

        BoxCollider boxCollider;
        List<Rigidbody> rigidbodies;
        List<Collider> colliders;
        List<GameObject> objects;
        GameObject cameraObject;
        Camera camera;

        public Lab06()
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
            rigidbodies = new List<Rigidbody>();
            colliders = new List<Collider>();
            objects = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/Arial");
            model = Content.Load<Model>("Models/Sphere");
            foreach (ModelMesh mesh in model.Meshes)
                foreach (BasicEffect effect in mesh.Effects)
                    effect.EnableDefaultLighting();

            cameraObject = new GameObject();
            cameraObject.Transform.LocalPosition = Vector3.Backward * 20;
            camera = cameraObject.Add<Camera>();

            boxCollider = new BoxCollider();
            boxCollider.Size = 10;
            AddSphere();
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();

            // First Iteration: simulate motion for gravity
            // if (objectTransform.LocalPosition.Y < 0 && rigidbody.Velocity.Y < 0)
            //    rigidbody.Impulse = -new Vector3(0,rigidbody.Velocity.Y,0) * 2.1f * rigidbody.Mass;

            if (Input.IsKeyPressed(Keys.Space))
                AddSphere();
            foreach (Rigidbody rigidbody in rigidbodies)
                rigidbody.Update();
            Vector3 normal;
            for (int i = 0; i < colliders.Count; i++)
            {
                if (boxCollider.Collides(colliders[i], out normal))
                {
                    if (Vector3.Dot(normal, rigidbodies[i].Velocity) < 0)
                        rigidbodies[i].Impulse += Vector3.Dot(normal, rigidbodies[i].Velocity) * -2 * normal;
                }
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    if (colliders[i].Collides(colliders[j], out normal))
                    {
                        // do resolution ONLY if they are colliding into one another
                        // if normal is from i to j
                        // dot(normal, vi) > 0 & dot(normal, vj) < 0) (A)
                        if (Vector3.Dot(normal, rigidbodies[i].Velocity) > 0 &&
                            Vector3.Dot(normal, rigidbodies[j].Velocity) < 0)
                            return;
                        Vector3 velocityNormal = Vector3.Dot(normal, rigidbodies[i].Velocity - rigidbodies[j].Velocity)
                                        * -2 * normal * rigidbodies[i].Mass * rigidbodies[j].Mass;
                        rigidbodies[i].Impulse += velocityNormal / 2;
                        rigidbodies[j].Impulse += -velocityNormal / 2;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.DepthStencilState = new DepthStencilState();

            foreach (GameObject gameObject in objects)
                model.Draw(gameObject.Transform.World, camera.View, camera.Projection);
            //spriteBatch.Begin();
            //spriteBatch.End();
            base.Draw(gameTime);
        }

        private void AddSphere()
        {
            GameObject gameObject = new GameObject();
            Rigidbody rigidbody = gameObject.Add<Rigidbody>();
            rigidbody.Mass = 1; // Lab 7: random mass
            //rigidbody.Acceleration = Vector3.Down * 9.81f;
            //rigidbody.Velocity = new Vector3((float)random.NextDouble() * 5, (float)random.NextDouble() * 5, (float)random.NextDouble() * 5);
            Vector3 direction = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            direction.Normalize();
            rigidbody.Velocity = direction * ((float)random.NextDouble() * 5 + 5);
            SphereCollider sphereCollider = gameObject.Add<SphereCollider>();
            sphereCollider.Radius = 2.5f * gameObject.Transform.LocalScale.Y;
            objects.Add(gameObject);
            colliders.Add(sphereCollider);
            rigidbodies.Add(rigidbody);
        }
    }
}
