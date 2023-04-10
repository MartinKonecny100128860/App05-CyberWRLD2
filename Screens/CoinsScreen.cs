using App05MonoGame.Controllers;
using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace App05MonoGame.Screens
{
    public class CoinsScreen : IUpdateableInterface, IDrawableInterface
    {
        #region Attributes

        private App05Game game;
        private Texture2D backgroundImage;
        private Button pauseButton;

        // Arial large font and calibri small font

        private SpriteFont arialFont;
        private SpriteFont calibriFont;

        private AnimatedPlayer playerSprite;
        private AnimatedSprite enemySprite;
        private CoinsController coinsController;

        #endregion
        public CoinsScreen(App05Game game)
        {
            this.game = game;
            LoadContent();
        }

        public void LoadContent()
        {
            backgroundImage = game.Content.Load<Texture2D>(
                "backgrounds/green_background720p");

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

            SetupAnimatedPlayer();
            SetupEnemy();
            SetupCoins();
        }

        /// <summary>
        /// Create a controller for coins with one coin
        /// </summary>
        private void SetupCoins()
        {
            coinsController = new CoinsController(game);
        }

        /// <summary>
        /// This is a Sprite with four animations for the four
        /// directions, up, down, left and right
        /// </summary>
        private void SetupAnimatedPlayer()
        {
            Texture2D sheet4x3 = game.Content.Load<Texture2D>("Actors/rsc-sprite-sheet1");

            AnimationController contoller = new AnimationController(game.Graphics, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };
            contoller.CreateAnimationGroup(keys);

            playerSprite = new AnimatedPlayer()
            {
                CanWalk = true,
                Scale = 2.0f,

                Position = new Vector2(200, 200),
                Speed = 200,
                Direction = new Vector2(1, 0),

                Rotation = MathHelper.ToRadians(0),
                RotationSpeed = 0f
            };

            contoller.AppendAnimationsTo(playerSprite);
        }

        /// <summary>
        /// This is an enemy Sprite with four animations for the four
        /// directions, up, down, left and right.  Has no intelligence!
        /// </summary>
        private void SetupEnemy()
        {
            Texture2D sheet4x3 = game.Content.Load<Texture2D>("Actors/rsc-sprite-sheet3");

            AnimationController manager = new AnimationController(game.Graphics, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };

            manager.CreateAnimationGroup(keys);

            enemySprite = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 200),
                Direction = new Vector2(-1, 0),
                Speed = 50,

                Rotation = MathHelper.ToRadians(0),
            };

            manager.AppendAnimationsTo(enemySprite);
            enemySprite.PlayAnimation("Left");
        }

        private void PauseGame(object sender, System.EventArgs e)
        {
             game.Paused = !(game.Paused);

            if (game.Paused)
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
                playerSprite.Update(gameTime);
                enemySprite.Update(gameTime);

                if (playerSprite.HasCollided(enemySprite))
                {
                    playerSprite.IsActive = false;
                    playerSprite.IsAlive = false;
                    enemySprite.IsActive = false;
                }

                coinsController.Update(gameTime);
                coinsController.DetectCollision(playerSprite);
            }
        }

        /// <summary>
        /// Draw Player, enemy and coins
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            pauseButton.Draw(spriteBatch, gameTime);

            playerSprite.Draw(spriteBatch, gameTime);
            coinsController.Draw(spriteBatch, gameTime);
            enemySprite.Draw(spriteBatch, gameTime);

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
