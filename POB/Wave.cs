using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace POB
{
    /// <summary>
    /// Wave of color that passes during the game
    /// </summary>
    public class Wave
    {
        /// <summary>
        /// Position of the wave
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Color ID of the wave
        /// </summary>
        public string color;
        /// <summary>
        /// Velocity of the wave
        /// </summary>
        public Vector2 Velocity;
        /// <summary>
        /// Texture (color) of the wave
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// hit box of the wave
        /// </summary>
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 1200, 1200);
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Initialize wave with given properties
        /// </summary>
        /// <param name="Position">Position of the wave</param>
        /// <param name="Color">Color ID of the wave</param>
        /// <param name="Velocity">Velocity of the wave</param>
        /// <param name="Texture">Texture (color) of the wave</param>
        public Wave (Vector2 Position, string Color, Vector2 Velocity, Texture2D Texture)
        {
            this.Position = Position;
            color = Color;
            this.Velocity = Velocity;
            this.Texture = Texture;
            bounds.X = Position.X;
            bounds.Y = Position.Y;
        }

        /// <summary>
        /// Update the position of the wave and hitbox
        /// </summary>
        public void Update()
        {
            Position += Velocity;
            bounds.X = Position.X-600;
            bounds.Y = Position.Y-600;
        }

        /// <summary>
        /// Draw the wave
        /// </summary>
        /// <param name="spriteBatch">Handles drawing sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds.ToRectangle(), Color.White);
        }
    }
}
