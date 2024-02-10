using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace Project_Yokai.Content.Code
{
    //Lanterns that are unlit at first. When the player interacts with them they display their lit up animation
    public class Lantern
    {
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D SpriteSheet { get; set; }

        public Vector2 InitialPosition { get; }
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width / 3, Sprite.Height / 3);

        private Animation animation;
        public bool isLit = false;
        public Lantern(Vector2 position, Vector2 initialPosition, Texture2D sprite, Texture2D spriteSheet)
        {
            Position = position;
            SpriteSheet = spriteSheet;
            Sprite = sprite;
        }

        public void LightUp()
        {
            if (!isLit)
            {
                isLit = true;
                animation = new Animation(SpriteSheet, 4, 1, 0.2f);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (isLit)
            {
                animation.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            //spriteBatch.DrawRectangle(Hitbox, Color.Red);

            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            if (isLit)
            {
                animation.Draw(spriteBatch, Position, scale);

            }
        }
    }
}
