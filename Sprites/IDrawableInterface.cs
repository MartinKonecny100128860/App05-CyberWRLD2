using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Sprites
{
    /// <summary>
    /// This method will use the spriteBatch to draw any
    /// game object that is visible
    /// </summary>
    public interface IDrawableInterface
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
