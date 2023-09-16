using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace POB
{
    /// <summary>
    /// Handles the hero's input
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// The current state of the keyboard
        /// </summary>
        public KeyboardState currentKeyboardState;
        /// <summary>
        /// The previous state of the keyboard
        /// </summary>
        public KeyboardState priorKeyboardState;

        /// <summary>
        /// The direction the hero is moving
        /// </summary>
        public Vector2 DirectionMoving;

        /// <summary>
        /// The velocity in which the hero is moving
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// The direction the hero is facing
        /// </summary>
        public Vector2 DirectionFacing = new Vector2(0, 1);

        /// <summary>
        /// The current color of the hero
        /// </summary>
        public string color = "blue";

        /// <summary>
        /// Checks if the hero is dashing
        /// </summary>
        public bool Dash = false;

        /// <summary>
        /// Update the hero's input
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            Dash = false;

            #region movement
            float moveSpeed = 7.0f;

            DirectionMoving = new Vector2(0, 0);

            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                DirectionMoving.X = -moveSpeed;
                DirectionFacing = new Vector2(-1, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                DirectionMoving.X = moveSpeed;
                DirectionFacing = new Vector2(1, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                DirectionMoving.Y = -moveSpeed;
                DirectionFacing = new Vector2(0, -1);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                DirectionMoving.Y = moveSpeed;
                DirectionFacing = new Vector2(0, 1);
            }

            // Normalize the direction vector if moving diagonally
            if (DirectionMoving.LengthSquared() > 0)
            {
                DirectionMoving.Normalize();
                Velocity = DirectionMoving;
                DirectionMoving *= moveSpeed;
            }
            #endregion

            #region changeColor
            if (currentKeyboardState.IsKeyDown(Keys.J) && !priorKeyboardState.IsKeyDown(Keys.J)
                && color != "pink")
            {
                color = "pink";
                Dash = true;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.K) && !priorKeyboardState.IsKeyDown(Keys.K)
                        && color != "orange")
            {
                color = "orange";
                Dash = true;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.L) && !priorKeyboardState.IsKeyDown(Keys.L)
                    && color != "blue")
            {
                color = "blue";
                Dash = true;
            }

            #endregion
        }
    }
}
