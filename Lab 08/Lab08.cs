#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace GameEngine.Labs
{
    public class Lab08 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        List<GameObject> objects;
        List<Collider> colliders;
        List<Camera> cameras;
        Model cube;
        Model sphere;
        SoundEffect gunSound;
        
        public Lab08()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            objects = new List<GameObject>();
            colliders = new List<Collider>();
            cameras = new List<Camera>();
        }

        protected override void Initialize()
        {
            Time.Initialize();
            Input.Initialize();
            Screen.Initialize(graphics);
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gunSound = Content.Load<SoundEffect>("Sounds/Gun");
            cube = Content.Load<Model>("Models/Cube");
            sphere = Content.Load<Model>("Models/Sphere");

            GameObject gameObject;
            gameObject = new GameObject();
            gameObject.Transform.LocalPosition = new Vector3(1, 0, 0);
            objects.Add(gameObject);
            BoxCollider boxCollider = gameObject.Add<BoxCollider>();
            boxCollider.Size = 1;
            colliders.Add(boxCollider);

            gameObject = new GameObject();
            gameObject.Transform.LocalPosition = new Vector3(-1, 0, 0);
            objects.Add(gameObject);
            SphereCollider sphereCollider = gameObject.Add<SphereCollider>();
            sphereCollider.Radius = 1;
            colliders.Add(sphereCollider);
            
            // Front Camera
            gameObject = new GameObject();
            gameObject.Transform.LocalPosition = Vector3.Backward * 5;
            camera = gameObject.Add<Camera>();
            camera.Position = new Vector2(0f, 0f);
            camera.Size = new Vector2(0.5f, 1f);
            camera.AspectRatio = camera.Viewport.AspectRatio;
            cameras.Add(camera);
            // Top Down Camera
            gameObject = new GameObject();
            gameObject.Transform.LocalPosition = Vector3.Up * 10;
            gameObject.Transform.Rotate(Vector3.Right, -MathHelper.PiOver2);
            camera = gameObject.Add<Camera>();
            camera.Position = new Vector2(0.5f, 0f);
            camera.Size = new Vector2(0.5f, 1f);
            camera.AspectRatio = camera.Viewport.AspectRatio;
            cameras.Add(camera);

            camera = cameras[0];

        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape))
                Exit();
            Ray ray = camera.ScreenPointToWorldRay(Input.MousePosition);
            foreach (Collider collider in colliders)
            {
                collider.Transform.Rotate(Vector3.Up, Time.ElapsedGameTime);
                collider.Transform.Rotate(Vector3.Right, Time.ElapsedGameTime);
                collider.Transform.Rotate(Vector3.Forward, Time.ElapsedGameTime);

                if (collider.Intersects(ray))
                {
                    SoundEffectInstance soundInstance = gunSound.CreateInstance();
                    if (Input.IsMousePressed(0))
                    {
                        soundInstance.IsLooped = false;
                        soundInstance.Play();
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach(Camera camera in cameras)
            {
                GraphicsDevice.DepthStencilState = new DepthStencilState();
                GraphicsDevice.Viewport = camera.Viewport;
                Matrix view = camera.View;
                Matrix projection = camera.Projection;
                cube.Draw(objects[0].Transform.World, view, projection);
                sphere.Draw(objects[0].Transform.World, view, projection);
            }

            //spriteBatch.Begin();
            // Any 2D stuff goes here!
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
