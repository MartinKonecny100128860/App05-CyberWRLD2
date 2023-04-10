using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace App05MonoGame.Sprites
{
    public class Button : IUpdateableInterface, IDrawableInterface
    {

        #region Properties

        public Texture2D Image { get; }

        public float Scale { get; set; }

        public Vector2 Position { get; set; }

        public string Text { get; set; }

        public event EventHandler click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y,
                    (int)(Image.Width * Scale), 
                    (int)(Image.Height * Scale));
            }
        }
        #endregion

        #region Attributes

        private MouseState currentMouse;
        private MouseState previousMouse;

        private SpriteFont font;

        private bool isHovering;

        #endregion

        public Button(SpriteFont font, Texture2D image)
        {
            this.font = font;
            Image = image;
            Scale = 1.0f;

            PenColour = Color.Black;
        }

        #region Methods

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var colour = Color.White;

            if (isHovering) colour = Color.Gray;

            spriteBatch.Draw(Image, BoundingBox, colour);

            if(!string.IsNullOrEmpty(Text))
            {
                var x = (BoundingBox.X + (BoundingBox.Width / 2)) -
                    (font.MeasureString(Text).X / 2);

                var y = (BoundingBox.Y + (BoundingBox.Height / 2)) -
                    (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColour);
            }
        }

        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(
                currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = true;
            if(mouseRectangle.Intersects(BoundingBox))
            {
                isHovering = true;

                if(currentMouse.LeftButton == ButtonState.Released &&
                   previousMouse.LeftButton == ButtonState.Pressed)
                {
                    if(click != null)
                    {
                        click(this, new EventArgs());
                    }
                }
            }
        }


        public bool HasCollided(Sprite other)
        {
            return false;
        }

        #endregion
    }
}
