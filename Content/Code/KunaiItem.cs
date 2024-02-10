using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Project_Yokai.Content.Code;

namespace Project_Yokai.Content.Code
{
    //The physical kunai item that will dissapear and change the player state when interacted with
    public class KunaiItem
    {
        private static Texture2D texture;
        private SoundEffect kunaiObtainedSound;
        private Vector2 position;

        public Vector2 Position => position;
        private Animation animation;
        private bool isVisible = true;

        public bool IsObtained { get; private set; }

        public KunaiItem(Texture2D texture)
        {
            animation = new Animation(texture, 3, 1, 0.1f);
        }


        public void SetInitialPosition(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        public void Interact()
        {
            IsObtained = true;
            Debug.WriteLine("Kunai aquired!");

        }

        public void Update(GameTime gameTime)
        {
            if (!IsObtained)
            {
                animation.Update(gameTime);
            }
            else
            {
                isVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            if (isVisible)
            {
                animation.Draw(spriteBatch, position, scale);
            }
        }
    }
}
