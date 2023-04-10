using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace App05MonoGame.Screens
{
    public class StartScreen : IUpdateableInterface, IDrawableInterface
    {
        #region fields

        private App05Game game;
        private Texture2D backgroundImage;
        private SpriteFont arialFont;

        private Button coinsButton;
        private Button asteroidsButton;

        private List<string> instructions;

        #endregion
        public StartScreen(App05Game game)
        {
            this.game = game;
            LoadContent();
        }

        public void LoadContent()
        {
            backgroundImage = game.Content.Load<Texture2D>(
                "backgrounds/Space6000x4000");

            CreateInstructions();

            arialFont = game.Content.Load<SpriteFont>("fonts/arial");
            
            SetupCoinsButton();
            SetupAsteroidsButton();
                
        }

        private void SetupAsteroidsButton()
        {
            asteroidsButton = new Button(arialFont,
                game.Content.Load<Texture2D>("Controls/button-icon-png-200"))
            {
                Position = new Vector2(1100, 640),
                Text = "Asteroids",
                Scale = 0.8f
            };

            asteroidsButton.click += StartAsteroidsGame;
        }

        private void SetupCoinsButton()
        {
            coinsButton = new Button(arialFont,
                game.Content.Load<Texture2D>("Controls/button-icon-png-200"))
            {
                Position = new Vector2(1130, 580),
                Text = "Coins",
                Scale = 0.6f
            };

            coinsButton.click += StartCoinsGame;
        }

        /// <summary>
        /// A short summary explaining how to play the game
        /// </summary>
        private void CreateInstructions()
        {
            instructions = new List<string>();

            instructions.Add("The player can move around by ...");
            instructions.Add("Every time the player collides with a coin...");
            instructions.Add("A dog moves around ...");
            instructions.Add("Every time the dog collides with the player...");
            instructions.Add("Energy is lost...");
            instructions.Add("The game is won when...");
            instructions.Add("The game is lost when...");
        }

        private void StartCoinsGame(object sender, System.EventArgs e)
        {
            game.GameState = GameStates.PlayingLevel1;
        }

        private void StartAsteroidsGame(object sender, System.EventArgs e)
        {
            game.GameState = GameStates.PlayingLevel2;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            int y = 100;
            foreach(string line in instructions)
            {
                y += 40;
                int x = 200;
                spriteBatch.DrawString(arialFont, line,
                    new Vector2(x, y), Color.White);
            }

            coinsButton.Draw(spriteBatch, gameTime);
            asteroidsButton.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            coinsButton.Update(gameTime);
            asteroidsButton.Update(gameTime);
        }
    }
}
