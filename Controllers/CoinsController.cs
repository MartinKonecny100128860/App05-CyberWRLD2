using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace App05MonoGame.Controllers
{
    /// <summary>
    /// Could be used for three different coloured coins
    /// </summary>
    public enum CoinColours
    {
        copper = 100,
        Silver = 200,
        Gold = 500
    }

    /// <summary>
    /// This class creates a list of coins which
    /// can be updated and drawn and checked for
    /// collisions with the player sprite
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class CoinsController : IUpdateableInterface, 
        IDrawableInterface, ICollideableInterface
    {
        private App05Game game;

        private Texture2D copperCoinSheet;

        private readonly List<AnimatedSprite> Coins;        

        /// <summary>
        /// Create a new list of coins with one copper coin
        /// </summary>
        public CoinsController(App05Game game)
        {
            this.game = game;
            Coins = new List<AnimatedSprite>();

            copperCoinSheet = game.Content.Load<Texture2D>("Actors/coin_copper");
            
            CreateCoin();
        }

        /// <summary>
        /// Create an animated sprite of a copper coin
        /// which could be collected by the player for a score
        /// </summary>
        public void CreateCoin()
        {
            SoundController.PlaySoundEffect(Sounds.Coins);

            Animation animation = new Animation(
                game.Graphics, "coin", copperCoinSheet, 8);

            AnimatedSprite coin = new AnimatedSprite()
            {
                Animation = animation,
                Image = animation.FirstFrame,
                Scale = 2.0f,
                Position = new Vector2(600, 100),
                Speed = 0,
            };

            Coins.Add(coin);
        }

        /// <summary>
        /// If the sprite collides with a coin the coin becomes
        /// invisible and inactive.  A sound is played
        /// </summary>
        public void DetectCollision(Sprite sprite)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                if (coin.HasCollided(sprite) && coin.IsAlive)
                {
                    SoundController.PlaySoundEffect(Sounds.Coins);

                    coin.IsActive = false;
                    coin.IsAlive = false;
                    coin.IsVisible = false;
                }
            }           
        }

        public void Update(GameTime gameTime)
        {
            // TODO: create more coins every so often??
            // or recyle collected coins

            foreach(AnimatedSprite coin in Coins)
            {
                coin.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }
        }
    }
}
