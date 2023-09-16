using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace POB
{
    /// <summary>
    /// Desk sprite obstacle
    /// </summary>
    public class Desk
    {
        /// <summary>
        /// Position of the desk
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Hitbox of the desk
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// Texture of the desk
        /// </summary>
        private Texture2D Texture;
        /// <summary>
        /// texture for hitbox (for debugging)
        /// </summary>
        private Texture2D hitBoxTexture;

        /// <summary>
        /// Initialize desk with given properties
        /// </summary>
        /// <param name="position">Position of desk</param>
        /// <param name="texture">Texture of desk</param>
        /// <param name="hitBoxTexture">texture for hitbox (for debugging)</param>
        public Desk(Vector2 position, Texture2D texture, Texture2D hitBoxTexture)
        {
            Texture = texture;

            Position = position;
            Bounds = new BoundingRectangle(new Vector2(Position.X-135, Position.Y-120), 200, 30);
            this.hitBoxTexture = hitBoxTexture;
        }

        /// <summary>
        /// Draw the desk
        /// </summary>
        /// <param name="spriteBatch">Handles drawing sprites</param>
        public void Draw (SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, 48, 35);
            spriteBatch.Draw(Texture, Position, source, Color.White, 0, new Vector2(35, 48), 3.0f, SpriteEffects.None, 0);
/*
            // Debug
            Rectangle hitBox = Bounds.ToRectangle();
            spriteBatch.Draw(hitBoxTexture, new Vector2(hitBox.X, hitBox.Y), hitBox, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);*/
        } 
    }
}
