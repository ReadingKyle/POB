using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POB
{
    /// <summary>
    /// Individual dash streak
    /// </summary>
    public class DashEffect
    {
        /// <summary>
        /// Position of dash
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Texture of dash
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Initialize the dash effect
        /// </summary>
        /// <param name="position">The position of the dash</param>
        /// <param name="texture">The texture of the dash</param>
        public DashEffect (Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }

        /// <summary>
        /// Draw the dash streak
        /// </summary>
        /// <param name="spriteBatch">Handles drawing sprites</param>
        /// <param name="source">The bounding rectangle of the texture</param>
        public void Draw (SpriteBatch spriteBatch, Rectangle source)
        {
            spriteBatch.Draw(Texture, Position, source, Color.White, 0, new Vector2(64, 64), 2.0f, SpriteEffects.None, 0);
        }
    }
}
