using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace POB
{
    public class Hero
    {
        /// <summary>
        /// Input of the user
        /// </summary>
        private InputManager Input;

        /// <summary>
        /// Manages dash system
        /// </summary>
        private DashManager DashManager;

        /// <summary>
        /// Current texture of the hero
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Hitbox texture (for debugging)
        /// </summary>
        private Texture2D hitBoxTexture;

        /// <summary>
        /// current color of the hero
        /// </summary>
        public string CurrentColor = "blue";

        /// <summary>
        /// previous color of the hero
        /// </summary>
        private string PreviousColor;

        /// <summary>
        /// hitbox of the hero
        /// </summary>
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(450, 450), 30, 121);
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// All the possible textures of the hero
        /// </summary>
        #region textures
        private Texture2D stationary_white;
        private Texture2D stationary_pink;
        private Texture2D stationary_orange;
        private Texture2D stationary_blue;
        private Texture2D runright_white;
        private Texture2D runright_pink;
        private Texture2D runright_orange;
        private Texture2D runright_blue;
        private Texture2D runleft_white;
        private Texture2D runleft_pink;
        private Texture2D runleft_orange;
        private Texture2D runleft_blue;
        private Texture2D runback_white;
        private Texture2D runback_pink;
        private Texture2D runback_orange;
        private Texture2D runback_blue;
        private Texture2D runfront_white;
        private Texture2D runfront_pink;
        private Texture2D runfront_orange;
        private Texture2D runfront_blue;

        /// <summary>
        /// Assign every texture with an ID
        /// </summary>
        Dictionary<string, Texture2D> stationary = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> runright = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> runleft = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> runback = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> runfront = new Dictionary<string, Texture2D>();
        #endregion

        /// <summary>
        /// Position of the hero
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Previous position of the hero
        /// </summary>
        public Vector2 PreviousPosition;

        /// <summary>
        /// Original position of the hero
        /// </summary>
        public Vector2 OriginalPosition { get; set; }

        /// <summary>
        /// Timer for sprite animation
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Current frame of sprite animation
        /// </summary>
        private short animationFrame;

        /// <summary>
        /// Check if the hero is dashing
        /// </summary>
        private bool Dashing = false;
        /// <summary>
        /// Time taken while dashing
        /// </summary>
        private double DashTimer;

        /// <summary>
        /// Check if the hero is dead
        /// </summary>
        public bool IsDead = false;

        /// <summary>
        /// Load all the textures, and initial properties
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            #region load textures
            hitBoxTexture = content.Load<Texture2D>("pink");

            stationary_white = content.Load<Texture2D>("stationary_white");
            stationary_pink = content.Load<Texture2D>("stationary_pink");
            stationary_orange = content.Load<Texture2D>("stationary_orange");
            stationary_blue = content.Load<Texture2D>("stationary_blue");

            runright_white = content.Load<Texture2D>("runright_white");
            runright_pink = content.Load<Texture2D>("runright_pink");
            runright_orange = content.Load<Texture2D>("runright_orange");
            runright_blue = content.Load<Texture2D>("runright_blue");

            runleft_white = content.Load<Texture2D>("runleft_white");
            runleft_pink = content.Load<Texture2D>("runleft_pink");
            runleft_orange = content.Load<Texture2D>("runleft_orange");
            runleft_blue = content.Load<Texture2D>("runleft_blue");

            runback_white = content.Load<Texture2D>("runback_white");
            runback_pink = content.Load<Texture2D>("runback_pink");
            runback_orange = content.Load<Texture2D>("runback_orange");
            runback_blue = content.Load<Texture2D>("runback_blue");

            runfront_white = content.Load<Texture2D>("runfront_white");
            runfront_pink = content.Load<Texture2D>("runfront_pink");
            runfront_orange = content.Load<Texture2D>("runfront_orange");
            runfront_blue = content.Load<Texture2D>("runfront_blue");

            stationary["white"] = stationary_white;
            stationary["pink"] = stationary_pink;
            stationary["orange"] = stationary_orange;
            stationary["blue"] = stationary_blue;

            runright["white"] = runright_white;
            runright["pink"] = runright_pink;
            runright["orange"] = runright_orange;
            runright["blue"] = runright_blue;

            runleft["white"] = runleft_white;
            runleft["pink"] = runleft_pink;
            runleft["orange"] = runleft_orange;
            runleft["blue"] = runleft_blue;

            runback["white"] = runback_white;
            runback["pink"] = runback_pink;
            runback["orange"] = runback_orange;
            runback["blue"] = runback_blue;

            runfront["white"] = runfront_white;
            runfront["pink"] = runfront_pink;
            runfront["orange"] = runfront_orange;
            runfront["blue"] = runfront_blue;

            #endregion

            OriginalPosition = Position;
            texture = stationary_white;
        }

        /// <summary>
        /// Update the state in which the hero is in
        /// </summary>
        /// <param name="gameTime">The time elapsed during the game</param>
        public void Update()
        {
            bounds.X = Position.X-78;
            bounds.Y = Position.Y-120;
            if (Dashing && DashTimer < 10)
            {
                DashTimer++;
                PreviousPosition = Position;
                Position += Input.DirectionFacing * 25;
            }
            else if (Dashing && DashTimer >= 10)
            {
                Dashing = false;
                DashTimer = 0;
            }
            else
            {
                PreviousPosition = Position;
                Position += Input.DirectionMoving;
                if (Input.Dash)
                {
                    animationFrame = 0;
                    Dashing = true;
                    PreviousColor = CurrentColor;
                    CurrentColor = Input.color;
                }
                else
                {
                    if (Input.DirectionFacing == new Vector2(0, 1))
                    {
                        if (Input.DirectionMoving.Y > 0) texture = runfront[CurrentColor];
                        else
                        {
                            animationFrame = 0;
                            animationTimer = 0;
                            texture = stationary[CurrentColor];
                        }
                    }
                    if (Input.DirectionFacing == new Vector2(1, 0))
                    {
                        if (Input.DirectionMoving.X > 0) texture = runright[CurrentColor];
                        else
                        {
                            animationFrame = 1;
                            animationTimer = 0;
                            texture = stationary[CurrentColor];
                        }
                    }
                    if (Input.DirectionFacing == new Vector2(0, -1))
                    {
                        if (Input.DirectionMoving.Y < 0) texture = runback[CurrentColor];
                        else
                        {
                            animationFrame = 2;
                            animationTimer = 0;
                            texture = stationary[CurrentColor];
                        }
                    }
                    if (Input.DirectionFacing == new Vector2(-1, 0))
                    {
                        if (Input.DirectionMoving.X < 0) texture = runleft[CurrentColor];
                        else
                        {
                            animationFrame = 3;
                            animationTimer = 0;
                            texture = stationary[CurrentColor];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draw the hero
        /// </summary>
        /// <param name="gameTime">Elapsed time of the game</param>
        /// <param name="spriteBatch">Handles drawing sprites</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.05)
            {
                animationFrame++;
                if (animationFrame > 11) animationFrame = 0;
                animationTimer -= 0.05;
            }

            var source = new Rectangle(animationFrame * 64, 0, 64, 64);

            if (Dashing)
            {
                DashManager.Draw(spriteBatch, Position, DashTimer, source, CurrentColor, PreviousColor, Input.DirectionFacing);
            }
            else
            {
                spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(64, 64), 2.0f, SpriteEffects.None, 0);
            }

/*            // Debug
            Rectangle hitBox = Bounds.ToRectangle();
            spriteBatch.Draw(hitBoxTexture, new Vector2(hitBox.X, hitBox.Y), hitBox, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);*/
        }
        /// <summary>
        /// Initialize the hero by connecting the input and dash managers
        /// </summary>
        /// <param name="input">Input manager of the hero</param>
        /// <param name="dashManager">Dash manager of the hero</param>
        public Hero (InputManager input, DashManager dashManager)
        {
            Input = input;
            DashManager = dashManager;
        }

        /// <summary>
        /// Check if hero is touching the left side of a desk
        /// </summary>
        /// <param name="desk">A desk obstacle</param>
        /// <returns>whether or not the user is touching the left side of a desk</returns>
        public bool IsTouchingLeft(Desk desk)
        {
            return this.Bounds.Right + Input.DirectionMoving.X / 7 > desk.Bounds.Left &&
                this.Bounds.Left < desk.Bounds.Left &&
                this.Bounds.Bottom > desk.Bounds.Top &&
                this.Bounds.Top < desk.Bounds.Bottom;
        }

        /// <summary>
        /// Check if hero is touching the right side of a desk
        /// </summary>
        /// <param name="desk">A desk obstacle</param>
        /// <returns>whether or not the user is touching the right side of a desk</returns>
        public bool IsTouchingRight(Desk desk)
        {
            return this.Bounds.Left + Input.Velocity.X / 7 < desk.Bounds.Right &&
                this.Bounds.Right > desk.Bounds.Right &&
                this.Bounds.Bottom > desk.Bounds.Top &&
                this.Bounds.Top < desk.Bounds.Bottom;
        }

        /// <summary>
        /// Check if hero is touching the top side of a desk
        /// </summary>
        /// <param name="desk">A desk obstacle</param>
        /// <returns>whether or not the user is touching the top side of a desk</returns>
        public bool IsTouchingTop(Desk desk)
        {
            return this.Bounds.Bottom + Input.Velocity.Y / 7 > desk.Bounds.Top &&
                this.Bounds.Top < desk.Bounds.Top &&
                this.Bounds.Right > desk.Bounds.Left &&
                this.Bounds.Left < desk.Bounds.Right;
        }

        /// <summary>
        /// Check if hero is touching the bottom side of a desk
        /// </summary>
        /// <param name="desk">A desk obstacle</param>
        /// <returns>whether or not the user is touching the bottom side of a desk</returns>
        public bool IsTouchingBottom(Desk desk)
        {
            return this.Bounds.Top + Input.Velocity.Y / 7 < desk.Bounds.Bottom &&
                this.Bounds.Bottom > desk.Bounds.Bottom &&
                this.Bounds.Right > desk.Bounds.Left &&
                this.Bounds.Left < desk.Bounds.Right;
        }

        /// <summary>
        /// Check if hero is inside a color wave
        /// </summary>
        /// <param name="wave">A color wave obstacle</param>
        /// <returns>whether or not the user is inside a color wave</returns>
        public bool IsInside(Wave wave)
        {
            return this.Bounds.X > wave.Bounds.Left &&
                this.Bounds.X < wave.Bounds.Right &&
                this.Bounds.Y > wave.Bounds.Top &&
                this.Bounds.Y < wave.Bounds.Bottom;
        }
    }
}
