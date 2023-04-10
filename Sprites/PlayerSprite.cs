using App05MonoGame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace App05MonoGame.Sprites
{
    public enum DirectionControl
    {
        Rotational,
        FourDirections
    }

    /// <summary>
    /// This class contains at least one animation,
    /// although more can be added to the Dictionary
    /// It updates and draws the current animation.
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class PlayerSprite : Sprite
    {
        private readonly MovementController movement;

        public DirectionControl DirectionControl { get; set; }

        public PlayerSprite(): base()
        {
            DirectionControl = DirectionControl.Rotational;
            movement = new MovementController();
        }

        /// <summary>
        /// Constructor sets the main image and starting position of
        /// the Sprite as a Vector2
        /// </summary>
        public PlayerSprite(Texture2D image, int x, int y) : this()
        {
            Image = image;
            Position = new Vector2(x, y);
        }

        public void SetControl(DirectionControl control)
        {
            this.DirectionControl = control;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            IsActive = false;
            RotationSpeed = 0;

            if(DirectionControl == DirectionControl.FourDirections)
            {
                Vector2 newDirection = movement.ChangeDirection(keyState);

                if (newDirection != Vector2.Zero)
                {
                    Direction = newDirection;
                    IsActive = true;
                }
            }
            else if(DirectionControl == DirectionControl.Rotational)
            {
                Rotate(keyState);
            }

            base.Update(gameTime);

        }

        private void Rotate(KeyboardState keyState)
        {

            if (keyState.IsKeyDown(movement.InputKeys.TurnRight))
            {
                if (RotationSpeed == 0) RotationSpeed = 1.0f;
                Rotation += MathHelper.ToRadians(RotationSpeed);
                Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            }
            else if (keyState.IsKeyDown(movement.InputKeys.TurnLeft))
            {
                if (RotationSpeed == 0) RotationSpeed = 1.0f;
                Rotation -= MathHelper.ToRadians(RotationSpeed);
                Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            }

            if (keyState.IsKeyDown(movement.InputKeys.Forward))
            {
                IsActive = true;
            }
        }

    }
}
