using App05MonoGame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace App05MonoGame.Sprites
{
    /// <summary>
    /// This class contains at least one animation,
    /// although more can be added to the Dictionary
    /// It updates and draws the current animation.
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class AnimatedSprite : Sprite
    {

        public Dictionary<string, Animation> Animations { get; set; }

        public Animation Animation { get; set; }

        public override int Width
        {
            get { return Animation.FrameWidth; }
        }

        public override int Height
        {
            get { return Animation.FrameHeight; }
        }

        private Rectangle sourceRectangle;

        public AnimatedSprite() : base()
        {            
        }

        public void PlayAnimation(string key)
        {
            if(Animations != null && Animations.ContainsKey(key))
            {
                Animation = Animations[key];
                Animation.Start();
            }
        }

        public override void Update(GameTime gameTime)
        {
            //added a default frame for when the sprite is not active
            sourceRectangle = new Rectangle(0, 0, Width, Height);

            if (Animation != null && IsActive)
            {
                sourceRectangle = Animation.UpdateFrame(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // SourceRectangle should never be empty now as Animation.Update will always 
            //return a frame.
            if (Animation != null && IsVisible)// && sourceRectangle != Rectangle.Empty)            
            {
                spriteBatch.Draw //This will now draw either a new frame or the previous frame.
                        (Animation.FrameSet,
                         Position,
                         sourceRectangle,
                         Color.White, Rotation, Origin,
                         Scale, SpriteEffects.None, 1);
            }
            else  
                base.Draw(spriteBatch, gameTime); 
        }
    }
}
