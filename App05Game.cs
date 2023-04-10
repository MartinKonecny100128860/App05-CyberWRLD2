using App05MonoGame.Controllers;
using App05MonoGame.Screens;
using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace App05MonoGame
{
    public enum GameStates
    {
        Starting,
        PlayingLevel1,
        PlayingLevel2,
        Ending
    }

    /// <summary>
    /// This game creates a variety of sprites as an example.  
    /// There is no game to play yet. The spaceShip and the 
    /// asteroid can be used for a space shooting game, the player, 
    /// the coin and the enemy could be used for a pacman
    /// style game where the player moves around collecting
    /// random coins and the enemy tries to catch the player.
    /// 
    /// Last Updated 7th June 2021
    /// 
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class App05Game : Game
    {
        #region Constants

        public const int Game_Height = 720;
        public const int Game_Width = 1280;

        public const string GameName = "Game Name";
        public const string ModuleName = "BNU CO453 2021";
        public const string AuthorNames = "Derek & Andrei";
        public const string AppName = "App05: C# MonoGame";

        #endregion

        #region Properties

        // Which state the game is in, starting, playing etc.

        public GameStates GameState { get; set; }

        public string GameStateTitle { get; set; }
        
        public GraphicsDevice Graphics { get; set; }

        public bool Paused { get; set; }

        #endregion

        #region: Attributes

        // Essential XNA objects for Graphics manipulation

        private readonly GraphicsDeviceManager graphicsManager;
        private SpriteBatch spriteBatch;

        // Screens

        private StartScreen startScreen;
        private CoinsScreen coinsScreen;
        private AsteroidsScreen asteroidsScreen;

        #endregion

        /// <summary>
        /// Create a graphics manager and the root for
        /// all the game assets or content
        /// </summary>
        public App05Game()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Setup the game window size to HD_Height x HD_Width pixels
        /// Simple fixed playing area with no camera or scrolling
        /// </summary>
        protected override void Initialize()
        {
            GameState = GameStates.Starting;
            GameStateTitle = GameName + ": Start Screen";

            graphicsManager.PreferredBackBufferWidth = Game_Width;
            graphicsManager.PreferredBackBufferHeight = Game_Height;

            graphicsManager.ApplyChanges();

            Graphics = graphicsManager.GraphicsDevice;
            Paused = false;

            base.Initialize();
        }

        /// <summary>
        /// use Content to load your game images, fonts,
        /// music and sound effects
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Music and SoundEffects

            SoundController.LoadContent(Content);
            SoundController.PlaySong("Adventure");

            startScreen = new StartScreen(this);
        }


        /// <summary>
        /// Called 60 frames/per second and updates the positions
        /// of all the drawable objects
        /// </summary>
        /// <param name="gameTime">
        /// Can work out the elapsed time since last call if
        /// you want to compensate for different frame rates
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            switch (GameState)
            {
                case GameStates.Starting:
                    startScreen.Update(gameTime); 
                    break;
                
                // Coins Game
                case GameStates.PlayingLevel1:
                    if (coinsScreen == null)
                        coinsScreen = new CoinsScreen(this);
                    coinsScreen.Update(gameTime);
                    break;
                
                // Asteroids Game
                case GameStates.PlayingLevel2:
                    if (asteroidsScreen == null)
                        asteroidsScreen = new AsteroidsScreen(this);
                    asteroidsScreen.Update(gameTime);
                    break;
                
                case GameStates.Ending:
                    break;
                
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Called 60 frames/per second and Draw all the 
        /// sprites and other drawable images here
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            spriteBatch.Begin();

            switch (GameState)
            {
                case GameStates.Starting:
                    startScreen.Draw(spriteBatch, gameTime);
                    break;

                case GameStates.PlayingLevel1:
                    if (coinsScreen != null)
                        coinsScreen.Draw(spriteBatch, gameTime);
                    break;

                case GameStates.PlayingLevel2:
                    if (asteroidsScreen != null)
                        asteroidsScreen.Draw(spriteBatch, gameTime);
                    break;

                case GameStates.Ending:
                    break;

                default:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
