﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    /// <summary>
    /// Enumeration for mouse buttons
    /// </summary>
    public enum Buttons { Left, Right, Middle };

    /// <summary>
    /// Handles inputs in the game. This is a static class and cannot
    /// be instantiated.
    /// </summary>
    public static class Input
    {
        #region Properties
        /// <summary>
        /// The keyboard state in the previous Update
        /// </summary>
        private static KeyboardState PreviousKeyboardState { get; set; }

        /// <summary>
        /// The keyboard state in the current Update
        /// </summary>
        private static KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        /// The mouse state in the previous Update
        /// </summary>
        private static MouseState PreviousMouseState { get; set; }

        /// <summary>
        /// The mouse state in the current Update
        /// </summary>
        private static MouseState CurrentMouseState { get; set; }
        #endregion

        /// <summary>
        /// Initializes the input states
        /// </summary>
        public static void Initialize()
        {
            PreviousKeyboardState = CurrentKeyboardState =
                Keyboard.GetState();
            PreviousMouseState = CurrentMouseState =
                Mouse.GetState();
        }

        /// <summary>
        /// Updates the input states by retrieving the current states
        /// of keyboard and mouse
        /// </summary>
        public static void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        #region Keyboard Methods

        public static bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) &&
                    PreviousKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key) &&
                    PreviousKeyboardState.IsKeyDown(key);
        }

        #endregion

        #region Mouse Methods
        public static Vector2 MousePosition
        {
            get { return new Vector2(CurrentMouseState.X, CurrentMouseState.Y); }
        }

        public static bool IsMousePressed(Buttons button)
        {
            switch (button)
            {
                case Buttons.Left: return PreviousMouseState.LeftButton == ButtonState.Released &&
                    CurrentMouseState.LeftButton == ButtonState.Pressed;
                case Buttons.Right: return PreviousMouseState.RightButton == ButtonState.Released &&
                    CurrentMouseState.RightButton == ButtonState.Pressed;
                case Buttons.Middle: return PreviousMouseState.MiddleButton == ButtonState.Released &&
                    CurrentMouseState.MiddleButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public static bool IsMouseReleased(Buttons button)
        {
            switch (button)
            {
                case Buttons.Left: return PreviousMouseState.LeftButton == ButtonState.Pressed &&
                    CurrentMouseState.LeftButton == ButtonState.Released;
                case Buttons.Right: return PreviousMouseState.RightButton == ButtonState.Pressed &&
                    CurrentMouseState.RightButton == ButtonState.Released;
                case Buttons.Middle: return PreviousMouseState.MiddleButton == ButtonState.Pressed &&
                    CurrentMouseState.MiddleButton == ButtonState.Released;
                default:
                    return false;
            }
        }
        #endregion

    }
}
