using App05MonoGame.Controllers;
using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Screens
{
    public class AsteroidsScreen : IUpdateableInterface, IDrawableInterface
    {
        #region Attributes

        private App05Game game;
        private Texture2D backgroundImage;
        private Button pauseButton;

        // Arial large font and calibri small font

        private SpriteFont arialFont;
        private SpriteFont calibriFont;

        private PlayerSprite shipSprite;
        private Sprite asteroidSprite;

        #endregion
        public AsteroidsScreen(App05Game game)
        {
            this.game = game;
            LoadContent();
        }

        public void LoadContent()
        {
            backgroundImage = game.Content.Load<Texture2D>(
                "backgrounds/Space6000x4000");

            arialFont = game.Content.Load<SpriteFont>("fonts/arial");
            calibriFont = game.Content.Load<SpriteFont>("fonts/calibri");

            pauseButton = new Button(arialFont,
                game.Content.Load<Texture2D>("Controls/button-icon-png-200"))
            {
                Position = new Vector2(1100, 600),
                Text = "Pause",
                Scale = 0.6f
            };

            pauseButton.click += PauseGame;

            SetupSpaceShip();
            SetupAsteroid();
        }

        /// <summary>
        /// This is a single image sprite that rotates
        /// and move at a constant speed in a fixed direction
        /// </summary>
        private void SetupAsteroid()
        {
            Texture2D asteroid = game.Content.Load<Texture2D>(
               "Actors/asteroid-1");

            asteroidSprite = new Sprite(asteroid, 1200, 500)
            {
                Direction = new Vector2(-1, 0),
                Speed = 100,
                Scale = 0.2f,
                Rotation = MathHelper.ToRadians(3),
                RotationSpeed = 2f,
            };

        }

        /// <summary>
        /// This is a Sprite that can be controlled by a
        /// player using Rotate Left = A, Rotate Right = D, 
        /// Forward = Space
        /// </summary>
        private void SetupSpaceShip()
        {
            Texture2D ship = game.Content.Load<Texture2D>(
               "Actors/GreenShip");

            shipSprite = new PlayerSprite(ship, 200, 500)
            {
                Direction = new Vector2(1, 0),
                Speed = 200,
                DirectionControl = DirectionControl.Rotational
            };
        }


        private void PauseGame(object sender, System.EventArgs e)
        {
            game.Paused = !(game.Paused);
            
            if(game.Paused)
            {
                SoundController.PauseSong();
            }
            else 
            {
                SoundController.ResumeSong();
            }
        }

        /// <summary>
        /// Update player, enemy and coins
        /// </summary>
        public void Update(GameTime gameTime)
        {
            pauseButton.Update(gameTime);

            if(!game.Paused)
            {
                shipSprite.Update(gameTime);
                asteroidSprite.Update(gameTime);

                if (shipSprite.HasCollided(asteroidSprite) && shipSprite.IsAlive)
                {
                    SoundController.PlaySoundEffect(Sounds.Collisions);

                    shipSprite.IsActive = false;
                    shipSprite.IsAlive = false;
                    shipSprite.IsVisible = false;
                }
            }
        }

        /// <summary>
        /// Draw Player, enemy and coins
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            pauseButton.Draw(spriteBatch, gameTime);

            shipSprite.Draw(spriteBatch, gameTime);
            asteroidSprite.Draw(spriteBatch, gameTime);

            DrawGameStatus(spriteBatch);
            DrawGameFooter(spriteBatch);
        }
        /// <summary>
        /// Display the name of the game and the current score
        /// and status of the player at the top of the screen
        /// </summary>
        public void DrawGameStatus(SpriteBatch spriteBatch)
        {
            // TODO: Use the Sprite's score and energy

            int score = 0;
            int energy = 100;

            int topMargin = 4;
            int sideMargin = 50;

            Vector2 topLeft = new Vector2(sideMargin, topMargin);
            string status = $"Score = {score:##0}";

            spriteBatch.DrawString(arialFont, status, topLeft, Color.White);

            Vector2 gameSize = arialFont.MeasureString(App05Game.GameName);
            Vector2 topCentre = new Vector2((App05Game.Game_Width / 2 - gameSize.X / 2), topMargin);
            spriteBatch.DrawString(arialFont, App05Game.GameName, topCentre, Color.White);

            string healthText = $"Energy = {energy:##0}%";
            Vector2 healthSize = arialFont.MeasureString(healthText);
            Vector2 topRight = new Vector2(
                App05Game.Game_Width - (healthSize.X + sideMargin), topMargin);

            spriteBatch.DrawString(arialFont, healthText, topRight, Color.White);

        }

        /// <summary>
        /// Display identifying information such as the the App,
        /// the Module, the authors at the bottom of the screen
        /// </summary>
        public void DrawGameFooter(SpriteBatch spriteBatch)
        {
            int bottomMargin = 30;

            Vector2 namesSize = calibriFont.MeasureString(App05Game.AuthorNames);
            Vector2 appSize = calibriFont.MeasureString(App05Game.AppName);

            Vector2 bottomCentre = new Vector2(
                (App05Game.Game_Width - namesSize.X) / 2,
                App05Game.Game_Height - bottomMargin);

            Vector2 bottomLeft = new Vector2(
                bottomMargin, App05Game.Game_Height - bottomMargin);

            Vector2 bottomRight = new Vector2(
                App05Game.Game_Width - appSize.X - bottomMargin,
                App05Game.Game_Height - bottomMargin);

            spriteBatch.DrawString(calibriFont,
                App05Game.AuthorNames, bottomCentre, Color.Yellow);
            spriteBatch.DrawString(calibriFont,
                App05Game.ModuleName, bottomLeft, Color.Yellow);
            spriteBatch.DrawString(calibriFont,
                App05Game.AppName, bottomRight, Color.Yellow);
        }
    
    }
}
