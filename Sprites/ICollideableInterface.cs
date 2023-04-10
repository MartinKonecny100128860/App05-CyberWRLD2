namespace App05MonoGame.Sprites
{
    /// <summary>
    /// This method checks for collison between the sprite
    /// and any other set of collidable objects and takes
    /// appropriate action
    /// </summary>
    public interface ICollideableInterface
    {
        public void DetectCollision(Sprite sprite);
    }
}
