using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Diagnostics;
namespace Project_Yokai.Content.Code
{
    //Movable stones that will return to their initial position when the player resets the puzzle
    public class MovableStones
    {
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }
        public Vector2 InitialPosition { get; }
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width / 4, Sprite.Height / 4);
        public MovableStones(Vector2 position, Vector2 initialPosition, Texture2D sprite)
        {
            Position = position;
            InitialPosition = initialPosition;
            Sprite = sprite;
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public void Reset()
        {
            Position = InitialPosition;
        }
        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            // spriteBatch.DrawRectangle(Hitbox, Color.Red);

            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
