using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POB
{
    /// <summary>
    /// Manages the dashing system of the hero
    /// </summary>
    public class DashManager
    {
        /// <summary>
        /// The puff of smoke texture that lingers longest
        /// </summary>
        private Texture2D cloudTexture;
        /// <summary>
        /// the dash texture of the hero
        /// </summary>
        private Texture2D dashTexture;
        /// <summary>
        /// The transition texture between the dash and the hero
        /// </summary>
        private Texture2D endTexture;
        /// <summary>
        /// An array of dashes that correspond to previous and current colors
        /// </summary>
        private DashEffect[] DashArray = new DashEffect[4];

        /// <summary>
        /// All possible dash textures
        /// </summary>
        #region textures
        private Texture2D dashdownblue;
        private Texture2D dashdownorange;
        private Texture2D dashdownpink;

        private Texture2D dashrightblue;
        private Texture2D dashrightorange;
        private Texture2D dashrightpink;

        private Texture2D dashleftblue;
        private Texture2D dashleftorange;
        private Texture2D dashleftpink;

        private Texture2D dashupblue;
        private Texture2D dashuporange;
        private Texture2D dashuppink;

        private Texture2D downcloudblue;
        private Texture2D downcloudorange;
        private Texture2D downcloudpink;

        private Texture2D rightcloudblue;
        private Texture2D rightcloudorange;
        private Texture2D rightcloudpink;

        private Texture2D leftcloudblue;
        private Texture2D leftcloudorange;
        private Texture2D leftcloudpink;

        private Texture2D upcloudblue;
        private Texture2D upcloudorange;
        private Texture2D upcloudpink;

        Dictionary<string, Texture2D> dashdown = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> dashright = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> dashleft = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> dashup = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> downcloud = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> rightcloud = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> leftcloud = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> upcloud = new Dictionary<string, Texture2D>();
        #endregion

        /// <summary>
        /// Load all textures
        /// </summary>
        /// <param name="content">Dash texture content</param>
        public void LoadContent (ContentManager content)
        {
            #region load textures
            dashdownblue = content.Load<Texture2D>("dashdownblue");
            dashdownorange = content.Load<Texture2D>("dashdownorange");
            dashdownpink = content.Load<Texture2D>("dashdownpink");

            dashrightblue = content.Load<Texture2D>("dashrightblue");
            dashrightorange = content.Load<Texture2D>("dashrightorange");
            dashrightpink = content.Load<Texture2D>("dashrightpink");

            dashleftblue = content.Load<Texture2D>("dashleftblue");
            dashleftorange = content.Load<Texture2D>("dashleftorange");
            dashleftpink = content.Load<Texture2D>("dashleftpink");

            dashupblue = content.Load<Texture2D>("dashupblue");
            dashuporange = content.Load<Texture2D>("dashuporange");
            dashuppink = content.Load<Texture2D>("dashuppink");

            downcloudblue = content.Load<Texture2D>("downcloudblue");
            downcloudorange = content.Load<Texture2D>("downcloudorange");
            downcloudpink = content.Load<Texture2D>("downcloudpink");

            rightcloudblue = content.Load<Texture2D>("rightcloudblue");
            rightcloudorange = content.Load<Texture2D>("rightcloudorange");
            rightcloudpink = content.Load<Texture2D>("rightcloudpink");

            leftcloudblue = content.Load<Texture2D>("leftcloudblue");
            leftcloudorange = content.Load<Texture2D>("leftcloudorange");
            leftcloudpink = content.Load<Texture2D>("leftcloudpink");

            upcloudblue = content.Load<Texture2D>("upcloudblue");
            upcloudorange = content.Load<Texture2D>("upcloudorange");
            upcloudpink = content.Load<Texture2D>("upcloudpink");

            dashdown["blue"] = dashdownblue;
            dashdown["orange"] = dashdownorange;
            dashdown["pink"] = dashdownpink;

            dashright["blue"] = dashrightblue;
            dashright["orange"] = dashrightorange;
            dashright["pink"] = dashrightpink;

            dashleft["blue"] = dashleftblue;
            dashleft["orange"] = dashleftorange;
            dashleft["pink"] = dashleftpink;

            dashup["blue"] = dashupblue;
            dashup["orange"] = dashuporange;
            dashup["pink"] = dashuppink;

            downcloud["blue"] = downcloudblue;
            downcloud["orange"] = downcloudorange;
            downcloud["pink"] = downcloudpink;

            rightcloud["blue"] = rightcloudblue;
            rightcloud["orange"] = rightcloudorange;
            rightcloud["pink"] = rightcloudpink;

            leftcloud["blue"] = leftcloudblue;
            leftcloud["orange"] = leftcloudorange;
            leftcloud["pink"] = leftcloudpink;

            upcloud["blue"] = upcloudblue;
            upcloud["orange"] = upcloudorange;
            upcloud["pink"] = upcloudpink;
            #endregion
        }

        /// <summary>
        /// Draws each dash texture
        /// </summary>
        /// <param name="spriteBatch">Handles drawing each sprite</param>
        /// <param name="Position">Position of the dash</param>
        /// <param name="dashTimer">Amount of time that has elapsed since dashing</param>
        /// <param name="source">bounds of the texture</param>
        /// <param name="currentColor">new color of the hero</param>
        /// <param name="previousColor">Previous color of the hero</param>
        /// <param name="directionFacing">the direction the hero is facing</param>
        public void Draw (SpriteBatch spriteBatch, Vector2 Position, double dashTimer, 
           Rectangle source, string currentColor, string previousColor, Vector2 directionFacing)
        {
            var staticSource = new Rectangle(0, 0, 64, 64);
            if (directionFacing == new Vector2(0, 1))
            {
                cloudTexture = downcloud[previousColor];
                dashTexture = dashdown[previousColor];
                endTexture = dashdown[currentColor];
            }
            else if (directionFacing == new Vector2(0, -1))
            {
                cloudTexture = upcloud[previousColor];
                dashTexture = dashup[previousColor];
                endTexture = dashup[currentColor];
            }
            else if (directionFacing == new Vector2(1, 0))
            {
                cloudTexture = rightcloud[previousColor];
                dashTexture = dashright[previousColor];
                endTexture = dashright[currentColor];
            }
            else if (directionFacing == new Vector2(-1, 0))
            {
                cloudTexture = leftcloud[previousColor];
                dashTexture = dashleft[previousColor];
                endTexture = dashleft[currentColor];
            }
            if (dashTimer >= 1 && dashTimer <= 10)
            {
                if (DashArray[0] != null)
                {
                    DashArray[0].Draw(spriteBatch, source);
                }
                else
                {
                    DashArray[0] = new DashEffect(Position, cloudTexture);
                }
            }
            if (dashTimer >= 4 && dashTimer < 10)
            {
                if (DashArray[1] != null)
                {
                    DashArray[1].Draw(spriteBatch, new Rectangle(0, 0, 64, 64));
                }
                else
                {
                    DashArray[1] = new DashEffect(Position, dashTexture);
                }
            }
            if (dashTimer >= 8 && dashTimer < 10)
            {
                if (DashArray[2] != null)
                {
                    DashArray[2].Draw(spriteBatch, new Rectangle(64, 0, 64, 64));
                }
                else
                {
                    DashArray[2] = new DashEffect(Position, dashTexture);
                }
            }
            spriteBatch.Draw(endTexture, Position, new Rectangle(128, 0, 64, 64), Color.White, 0, new Vector2(64, 64), 2.0f, SpriteEffects.None, 0);
            if (dashTimer >= 10)
            {
                Array.Clear(DashArray);
            }
        }
    }
}