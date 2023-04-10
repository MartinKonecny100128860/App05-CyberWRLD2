using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Helpers
{
    public static class Texture2DExtension
    {
        /// <summary>
        /// Creates a new texture from an area of the texture.
        /// </summary>
        /// <param name="graphics">The current GraphicsDevice</param>
        /// <param name="sourceRectangle">The dimension you want to have</param>
        /// <returns>The partial Texture.</returns>
        public static Texture2D CreateTexture(this Texture2D source, 
                                              GraphicsDevice graphics, 
                                              Rectangle sourceRectangle)
        {
            Texture2D texture = new Texture2D(graphics, sourceRectangle.Width, sourceRectangle.Height);

            int count = sourceRectangle.Width * sourceRectangle.Height;
            Color[] data = new Color[count];

            source.GetData(0, sourceRectangle, data, 0, count);
            texture.SetData(data);

            return texture;
        }

    }
}
