using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System;

namespace Project_Yokai.Content.Code
{
    //The player's attack/weapon. Can be used to destroy stones
    public class Kunai
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;

        public bool IsActive { get; private set; }

        public Kunai(Vector2 startPosition, Vector2 direction, bool isActive)
        {
            this.position = startPosition;
            this.velocity = direction * 200f;
            IsActive = isActive;
        }

        public void LoadContent(Texture2D kunaiTexture)
        {
            texture = kunaiTexture;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


            }
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {

            float rotation = (float)Math.Atan2(velocity.X, velocity.Y);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);

        }
    }

}
