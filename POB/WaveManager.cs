using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace POB
{
    /// <summary>
    /// Manage the wave system in the game
    /// </summary>
    public class WaveManager
    {
        /// <summary>
        /// Collection of waves
        /// </summary>
        public List<Wave> Waves = new List<Wave>();
        /// <summary>
        /// Time between waves created
        /// </summary>
        private double waveTimer = 0;

        /// <summary>
        /// wave textures
        /// </summary>
        private Texture2D pink;
        private Texture2D orange;
        private Texture2D blue;

        /// <summary>
        /// List of color textures paired with color ID
        /// </summary>
        private List<(Texture2D, string)> colors;
        /// <summary>
        /// List of starting positions of the wave paired with velocities
        /// </summary>
        private List<(Vector2, Vector2)> StartingPositions;

        /// <summary>
        /// Speed of the wave
        /// </summary>
        private float speed;

        /// <summary>
        /// Load the textures of the waves
        /// </summary>
        /// <param name="content">Collection of textures</param>
        public void LoadContent (ContentManager content)
        {
            pink = content.Load<Texture2D>("pink");
            orange = content.Load<Texture2D>("orange");
            blue = content.Load<Texture2D>("blue");

            colors = new List<(Texture2D, string)>()
            {
                (pink, "pink"),
                (orange, "orange"),
                (blue, "blue")
            };

            StartingPositions = new List<(Vector2, Vector2)>()
            {
                (new Vector2(-600, 450), new Vector2(10, 0)),
                (new Vector2(450, 1800), new Vector2(0, -10)),
                (new Vector2(1800, 450), new Vector2(-10, 0)),
                (new Vector2(590, -600), new Vector2(0, 10)),
            };

            speed = (float)0.5;
        }

        /// <summary>
        /// Update the waves generated/remove hidden waves
        /// </summary>
        public void Update()
        {
            Random rnd = new Random();
            int colorIndex = rnd.Next(colors.Count());
            int positionIndex = rnd.Next(StartingPositions.Count());

            waveTimer++;
            if (waveTimer > 200 / speed)
            {
                speed += (float)0.02;
                Waves.Add(new Wave(StartingPositions[positionIndex].Item1, colors[colorIndex].Item2, new Vector2(StartingPositions[positionIndex].Item2.X * speed, StartingPositions[positionIndex].Item2.Y * speed), colors[colorIndex].Item1));
                waveTimer = 0;
            }
            if (waveTimer > 200 / speed)
            {
                if (Waves.Count > 0)
                {
                    Waves.RemoveAt(0);
                }
            }
            foreach (var wave in Waves)
            {
                wave.Update();
            }
        }

        /// <summary>
        /// Draw each wave in the collection
        /// </summary>
        /// <param name="spriteBatch">Handles drawing sprites</param>
        public void Draw (SpriteBatch spriteBatch)
        {
            foreach(var wave in Waves)
            {
                wave.Draw(spriteBatch);
            }
        }
    }
}
