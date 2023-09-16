using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace POB
{
    /// <summary>
    /// Game created by Kyle Reading for CIS 508
    /// Date: 9/15/2023
    /// </summary>
    public class POBGame : Game
    {
        /// <summary>
        /// Graphics for the game
        /// </summary>
        private GraphicsDeviceManager _graphics;
        /// <summary>
        /// Spritebatch for drawing sprites
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Managers player input
        /// </summary>
        private InputManager input;
        /// <summary>
        /// Manages player's dash
        /// </summary>
        private DashManager dashManager;
        /// <summary>
        /// Manages color waves
        /// </summary>
        private WaveManager waveManager;

        /// <summary>
        /// Hero sprite
        /// </summary>
        private Hero hero;

        /// <summary>
        /// List of Desk obstacles
        /// </summary>
        private Desk[] desks;

        /// <summary>
        /// White screen background
        /// </summary>
        private Texture2D whiteScreen;

        /// <summary>
        /// Font for regular text
        /// </summary>
        private SpriteFont november;
        /// <summary>
        /// font for small text
        /// </summary>
        private SpriteFont november_small;

        /// <summary>
        /// tracks if the game is over
        /// </summary>
        private bool GameOver = true;

        /// <summary>
        /// timer for how long the hero is alive
        /// </summary>
        private TimeSpan Timer;

        /// <summary>
        /// the current state of the keyboard
        /// </summary>
        private KeyboardState currentKeyboardState;
        /// <summary>
        /// the previous state of the keyboard
        /// </summary>
        private KeyboardState previousKeyboardState;

        /// <summary>
        /// Start up game window
        /// </summary>
        public POBGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initialize all sprites/textures with their respective positions
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            input = new InputManager();
            dashManager = new DashManager();
            waveManager = new WaveManager();
            hero = new Hero(input, dashManager) { Position = new Vector2(450, 450) };


            desks = new Desk[]
            {
                new Desk(new Vector2(200, 200), Content.Load<Texture2D>("desk1"), Content.Load<Texture2D>("blue")),
                new Desk(new Vector2(200, 425), Content.Load<Texture2D>("desk2"), Content.Load <Texture2D>("blue")),
                new Desk(new Vector2(200, 650), Content.Load<Texture2D>("desk3"), Content.Load <Texture2D>("blue")),
                new Desk(new Vector2(200, 875), Content.Load<Texture2D>("desk4"), Content.Load <Texture2D>("blue")),

                new Desk(new Vector2(750, 200), Content.Load<Texture2D>("desk5"), Content.Load <Texture2D>("blue")),
                new Desk(new Vector2(750, 425), Content.Load<Texture2D>("desk1"), Content.Load <Texture2D>("blue")),
                new Desk(new Vector2(750, 650), Content.Load<Texture2D>("desk3"), Content.Load<Texture2D>("blue")),
                new Desk(new Vector2(750, 875), Content.Load<Texture2D>("desk1"), Content.Load <Texture2D>("blue"))
            };

            base.Initialize();
        }

        /// <summary>
        /// Load all textures and properties on all sprites/managers
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            hero.LoadContent(Content);
            dashManager.LoadContent(Content);
            waveManager.LoadContent(Content);
            whiteScreen = new Texture2D(GraphicsDevice, 1, 1);
            whiteScreen.SetData(new[] { Color.White });

            november = Content.Load<SpriteFont>("november");
            november_small = Content.Load<SpriteFont>("november-small");
        }

        /// <summary>
        /// Update every sprite/manager
        /// </summary>
        /// <param name="gameTime">The amount of time that has passed from starting the game</param>
        protected override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            if (GameOver)
            {
                if (currentKeyboardState.IsKeyDown(Keys.J) && !previousKeyboardState.IsKeyDown(Keys.J)
                    || currentKeyboardState.IsKeyDown(Keys.K) && !previousKeyboardState.IsKeyDown(Keys.K)
                    || currentKeyboardState.IsKeyDown(Keys.L) && !previousKeyboardState.IsKeyDown(Keys.L))
                {
                    GameOver = false;
                    hero.IsDead = false;
                    Timer = TimeSpan.Zero;
                    waveManager = new WaveManager();
                    waveManager.LoadContent(Content);

                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            waveManager.Update();
            input.Update(gameTime);

            foreach (var desk in desks)
            {
                if (hero.IsTouchingLeft(desk) || hero.Bounds.Right > 900)
                {
                    hero.Position.X -= 7;
                }
                if (hero.IsTouchingRight(desk) || hero.Bounds.Left < 0)
                {
                    hero.Position.X += 7;
                }
                if (hero.IsTouchingTop(desk) || hero.Bounds.Top > 900)
                {
                    hero.Position.Y -= 7;
                }
                if (hero.IsTouchingBottom(desk) || hero.Bounds.Bottom < 0)
                {
                    hero.Position.Y += 7;
                }

            }

            hero.Update();
            if (hero.IsDead)
            {
                GameOver = true;
            }
            if (!GameOver)
            {
                foreach (var wave in waveManager.Waves)
                {
                    if (hero.IsInside(wave) && hero.CurrentColor != wave.color)
                    {
                        hero.IsDead = true;
                    }
                    else
                    {
                        hero.IsDead = false;
                    }
                    // Debug: Debug.WriteLine(hero.IsDead);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw every sprite graphic
        /// </summary>
        /// <param name="gameTime">The amount of time that has passed from starting the game</param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            _spriteBatch.Draw(whiteScreen, GraphicsDevice.Viewport.Bounds, Color.White);
            if(GameOver)
            {
                _spriteBatch.DrawString(november, "POB", new Vector2(400, 200), Color.Black);
                _spriteBatch.DrawString(november_small, "WASD to move", new Vector2(90, 450), Color.Black);
                _spriteBatch.DrawString(november_small, "JKL to POB", new Vector2(650, 450), Color.Black);
                _spriteBatch.DrawString(november_small, "Match colors by POBing", new Vector2(285, 575), Color.Black);
                _spriteBatch.DrawString(november_small, "POB to start", new Vector2(350, 600), Color.Black);
            }
            if (!GameOver)
            {
                Timer += gameTime.ElapsedGameTime;
                GraphicsDevice.Clear(Color.CornflowerBlue);
                waveManager.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(november, $"{Timer.Minutes:D2}:{Timer.Seconds:D2}", new Vector2(375, 300), Color.Black);
            foreach (var desk in desks)
            {
                if (desk.Position.Y < hero.Position.Y) desk.Draw(_spriteBatch);
            }

            hero.Draw(gameTime, _spriteBatch);

            foreach (var desk in desks)
            {
                if (desk.Position.Y > hero.Position.Y) desk.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}