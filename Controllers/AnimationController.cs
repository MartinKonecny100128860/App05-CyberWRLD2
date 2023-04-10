using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using App05MonoGame.Helpers;
using System.Collections.Generic;
using App05MonoGame.Sprites;

namespace App05MonoGame.Controllers
{
    /// <summary>
    /// This class takes a sprite sheet which may have many
    /// rows and columns and breaks it up into separate
    /// animations one per row.
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class AnimationController
    {
        // Original SpriteSheet rows * cols or frames
        public Texture2D SpriteSheet { get; set; }

        private readonly int sheetWidth;

        private readonly int frameHeight;

        private readonly int frameWidth;

        private readonly int frameCount;

        private readonly int animationCount;

        private string firstKey;

        private readonly GraphicsDevice graphicsDevice;

        // A key image for the base sprite
       // public Texture2D FirstFrame { get; set; }

        // Each element is a row of image frames
        public Texture2D[] SpriteSheets { get; }

        public Dictionary<string, Animation> Animations { get; set; }

        /// <summary>
        /// Initialise all the attributes, create a new
        /// array of sheets and new dictionary of
        /// Animations.
        /// </summary>
        public AnimationController(GraphicsDevice graphics,
            Texture2D spriteSheet, int rows, int columns)
        {
            graphicsDevice = graphics;
            SpriteSheet = spriteSheet;

            frameHeight = spriteSheet.Height / rows;
            sheetWidth = SpriteSheet.Width;
            frameWidth = sheetWidth / columns;
            
            frameCount = columns;
            animationCount = rows;

            SpriteSheets = new Texture2D[animationCount];
            Animations = new Dictionary<string, Animation>();

            if(rows > 1)
                CreateSheets();
            else
            {
                SpriteSheets[0] = spriteSheet;
            }
        }

        /// <summary>
        /// Break up the orginal sprite sheet into rows
        /// </summary>
        private void CreateSheets()
        {
            for (int row = 0; row < animationCount; row++)
            {
                Texture2D Image = SpriteSheet.CreateTexture(
                    graphicsDevice, new Rectangle(0, row * frameHeight,
                                            sheetWidth, frameHeight));
                SpriteSheets[row] = Image;
            }

        }

        /// <summary>
        /// Create an animation based on the row (starts at 1)
        /// and add it to the dictionary based on its name as
        /// a key.
        /// </summary>
        public void CreateAnimation(string keyName, int row)
        {
            if (row > 0 && row <= animationCount)
            {
                Animation animation = new Animation
                    (
                        graphicsDevice,
                        keyName, 
                        SpriteSheets[row - 1], 
                        frameCount
                    );
                
                

                Animations.Add(keyName, animation);

                if (firstKey == null)
                    firstKey = keyName;
            }
        }

        /// <summary>
        /// From the array of keyNames create an animation
        /// for each of the keynames from the SpriteSheetRows
        /// </summary>
        public void CreateAnimationGroup(string[] keyNames)
        {
            if(keyNames.Length == animationCount)
            {
                int row = 0;
                foreach (string key in keyNames)
                {
                    row++;
                    CreateAnimation(key, row);
                    if (firstKey == null)
                        firstKey = key;
                }
            }
        }

        public void AppendAnimationsTo(AnimatedSprite sprite)
        {
            if (sprite.Animations == null)
                sprite.Animations = new Dictionary<string, Animation>();

            foreach(var animation in Animations)
            {
                sprite.Animations.Add(animation.Key, animation.Value);
            }

            if (Animations.ContainsKey("Right"))
            {
                sprite.PlayAnimation("Right");
            }
            else sprite.PlayAnimation(firstKey);
        }

    }
}
