using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project_Yokai.Content.Code
//The manager for the game's animations
{
    public class AnimationManager
    {
        private readonly Dictionary<object, Animation> anims = new();
        private object lastKey;

        public void AddAnimation(object key, Animation animation)
        {
            anims.Add(key, animation);
            lastKey ??= key;
        }

        public void Update(object key, GameTime gameTime)
        {
            if (anims.TryGetValue(key, out Animation value))
            {
                value.Start();
                anims[key].Update(gameTime);
                lastKey = key;
            }
            else
            {
                anims[lastKey].Stop();
                anims[lastKey].Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            anims[lastKey].Draw(spriteBatch, position, scale);
        }
    }
}
