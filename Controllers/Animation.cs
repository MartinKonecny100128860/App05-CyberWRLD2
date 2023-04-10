using App05MonoGame.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Controllers
{
    /// <summary>
    /// This class takes a sprite sheet with one row
    /// of many images and cycles through it frame
    /// by frame returning the current frame in
    /// the update method as a source Rectangle
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class Animation
    {
        public string Name { get; private set; }
        // One animation: one row of multiple images
        public Texture2D FrameSet { get; set; }
        public Texture2D FirstFrame { get; set; }

        public int CurrentFrame { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsLooping { get; set; }
        public int FramesPerSecond { get; set; }

        public readonly int NumberOfFrames;
        public readonly int FrameWidth;
        public readonly int FrameHeight;
        
        private float elapsedTime;
        private float maxFrameTime;

        public Animation(GraphicsDevice graphics, string name, Texture2D frameSet, int frames)
        {
            Name = name;
            FrameSet = frameSet;
            NumberOfFrames = frames;

            FramesPerSecond = 10;
            FrameHeight = FrameSet.Height;
            FrameWidth = FrameSet.Width / NumberOfFrames;

            FirstFrame = frameSet.CreateTexture(graphics,
                    new Rectangle(0, 0, FrameWidth, FrameHeight));

            IsLooping = true;

            Start();
        }

        public void Start()
        {
            CurrentFrame = NumberOfFrames - 1;
            IsPlaying = true;
            maxFrameTime = 1.0f / (float)FramesPerSecond;
            elapsedTime = 0;
        }

        public void Stop()
        {
            IsPlaying = false;
            maxFrameTime = 0;
            elapsedTime = 0;
        }


        public Rectangle UpdateFrame(GameTime gameTime)
        {            
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(IsPlaying && elapsedTime >= maxFrameTime)
            {
                if (CurrentFrame < NumberOfFrames - 1)
                    CurrentFrame++;

                else if(IsLooping)
                    CurrentFrame = 0;

                elapsedTime = 0;
                
                return new Rectangle((CurrentFrame) * FrameWidth, 0,
                    FrameWidth, FrameHeight);
            }
            // this will return the previous frame instead of an empty rectangle
            //// so we are always drawing an image.
            return new Rectangle((CurrentFrame) * FrameWidth, 0,
                    FrameWidth, FrameHeight);
        }        
    }
}
