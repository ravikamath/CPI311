#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.UI;
using System.Collections.Generic;
using System;
#endregion

namespace GameEngine.Labs
{
    public class Lab11 : Game
    {
        class Scene
        {
            public delegate void CallMethod();
            public CallMethod Update;
            public CallMethod Draw;
            public Scene(CallMethod update, CallMethod draw)
            { Update = update; Draw = draw; }
        }
        enum GameState { Menu, Play, Pause };
        Dictionary<GameState, Scene> scenes;
        Scene currentScene;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<Element> guiElements;

        public Lab11()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            guiElements = new List<Element>();
            scenes = new Dictionary<GameState, Scene>();
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
            font = Content.Load<SpriteFont>("Fonts/Georgia");

            Texture2D texture = Content.Load<Texture2D>("Textures/Square");
            Group group = new Group();

            Button playButton = new Button();
            playButton.Font = font;
            playButton.Texture = texture;
            playButton.Bounds = new Rectangle(50, 50, 300, 20);
            playButton.Action += PlayGame;
            playButton.Text = "Play";
            group.Children.Add(playButton);

            CheckBox optionBox = new CheckBox();
            optionBox.Texture = texture;
            optionBox.Font = font;
            optionBox.Box = texture;
            optionBox.Bounds = new Rectangle(50, 75, 300, 20);
            optionBox.Action += MakeFullScreen;
            optionBox.Text = "Full Screen";
            group.Children.Add(optionBox);

            guiElements.Add(group);

            scenes.Add(GameState.Menu, new Scene(MainMenuUpdate, MainMenuDraw));
            scenes.Add(GameState.Play, new Scene(PlayUpdate, PlayDraw));
            //scenes.Add(GameState.Pause, new Scene(PauseUpdate, PauseDraw));
            currentScene = scenes[GameState.Menu];
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);
            Input.Update();
            // Call the update of the "current state"
            currentScene.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Call the draw of the "current state"
            currentScene.Draw();
            base.Draw(gameTime);
        }

        void PlayGame(Element element)
        {
            currentScene = scenes[GameState.Play];
        }

        void MakeFullScreen(Element element)
        {
            Screen.IsFullScreen = (element as CheckBox).Checked;
        }

        void MainMenuUpdate()
        {
            if (Input.IsKeyPressed(Keys.Escape))
                Exit();
            foreach (Element element in guiElements)
                element.Update();
        }

        void MainMenuDraw()
        {
            spriteBatch.Begin();
            foreach (Element element in guiElements)
                element.Draw(spriteBatch);
            spriteBatch.End();
        }

        void PlayUpdate()
        {
            if (Input.IsKeyReleased(Keys.Escape))
                currentScene = scenes[GameState.Menu];
        }

        void PlayDraw()
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Play Mode! Press \"Esc\" to go back", Vector2.Zero, Color.Black);
            spriteBatch.End();
        }
    }
}
