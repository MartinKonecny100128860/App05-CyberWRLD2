using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Helpers
{
    public static class TextHelper
    {
        private static SpriteFont Font;
        private static SpriteBatch SpriteBatch;

        public static void LoadFont(
            ContentManager content, SpriteBatch spriteBatch)
        {
            Font = content.Load<SpriteFont>("Arial");
            SpriteBatch = spriteBatch;
        }

        public static void DrawString(string text, Vector2 position)
        {
            SpriteBatch.DrawString(Font, text,
                new Vector2(position.X, position.Y - 20),
                Color.White);
        }
    }
}
