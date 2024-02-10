using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Project_Yokai.Content.Code
{
    //The player's logic. Goes from adding animations, managing playerstates, attacks etc
    public class Player
    {
        private Texture2D spriteSheet;
        private Texture2D kunaiTexture;
        private Vector2 position;
        private Vector2 velocity;
        public PlayerState currentPlayerState;
        public Vector2 Position => position;
        private Kunai currentKunai;
        private bool spacebarPressed = false;
        public Rectangle Hitbox => new Rectangle((int)position.X, (int)position.Y, 200 / 4, 400 / 4);

        private AnimationManager animations;
        public Player(Vector2 initialPosition)
        {
            this.velocity = Vector2.Zero;
            this.position = initialPosition;
            this.animations = new AnimationManager();
        }
        public void LoadContent(Texture2D spriteSheet, Texture2D kunaiTexture, ContentManager content)
        {
            this.spriteSheet = spriteSheet;
            this.kunaiTexture = kunaiTexture;
            InitializeAnimations();
        }

        public void SetInitialPosition(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        private void InitializeAnimations()
        {
            animations.AddAnimation(Vector2.UnitY, new Animation(spriteSheet, 4, 4, 0.1f, 1));
            animations.AddAnimation(-Vector2.UnitY, new Animation(spriteSheet, 4, 4, 0.1f, 2));
            animations.AddAnimation(Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 3));
            animations.AddAnimation(-Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 4));
            animations.AddAnimation(-Vector2.UnitY + Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 2));
            animations.AddAnimation(-Vector2.UnitY - Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 2));
            animations.AddAnimation(Vector2.UnitY + Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 1));
            animations.AddAnimation(Vector2.UnitY - Vector2.UnitX, new Animation(spriteSheet, 4, 4, 0.1f, 1));
        }

        public bool HasReachedDestination(Vector2 destination, float range = 5f)
        {
            return Math.Abs(position.X - destination.X) < range && Math.Abs(position.Y - destination.Y) < range;
        }


        public void Update(GameTime gameTime, bool useSpecialBounds = false)
        {
            InputManager.Update();

            velocity = Vector2.Zero;

            switch (currentPlayerState)
            {
                case PlayerState.Normal:
                    if (InputManager.Moving)
                    {
                        velocity = Vector2.Normalize(InputManager.Direction) * 100f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        animations.Update(InputManager.Direction, gameTime);

                    }

                    break;

                case PlayerState.HasObtainedItem:
                    if (InputManager.Moving)
                    {
                        velocity = Vector2.Normalize(InputManager.Direction) * 100f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        animations.Update(InputManager.Direction, gameTime);
                    }
                    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.Space) && !spacebarPressed)
                    {
                        Vector2 normalizedDirection = Vector2.Normalize(InputManager.Direction);
                        LaunchKunai(normalizedDirection);
                        spacebarPressed = true;
                    }

                    if (InputManager.CurrentKeyboardState.IsKeyUp(Keys.Space))
                    {
                        spacebarPressed = false;
                    }

                    if (currentKunai != null)
                    {
                        currentKunai.Update(gameTime);
                    }

                    break;

                default:
                    break;
            }

            if (useSpecialBounds)
            {
                float minX = 70;
                float minY = 150;
                float maxX = 680;
                float maxY = 390;

                position.X = MathHelper.Clamp(position.X, minX, maxX);
                position.Y = MathHelper.Clamp(position.Y, minY, maxY);
            }

            else
            {
                float minX = 5;
                float minY = 8;
                float maxX = 1136;
                float maxY = 608;

                position.X = MathHelper.Clamp(position.X, minX, maxX);
                position.Y = MathHelper.Clamp(position.Y, minY, maxY);
            }

            position += velocity;
        }
        private void LaunchKunai(Vector2 direction)
        {
            float kunaiSpeed = 5f;
            currentKunai = new Kunai(position, direction * kunaiSpeed, true);
            currentKunai.LoadContent(kunaiTexture);
        }

        public PlayerState CurrentPlayerState
        {
            get { return currentPlayerState; }
            set { currentPlayerState = value; }
        }
        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            animations.Draw(spriteBatch, position, scale);
            //spriteBatch.DrawRectangle(Hitbox, Color.Red);

            if (currentPlayerState == PlayerState.HasObtainedItem)
            {
                currentKunai.Draw(spriteBatch, scale: 0.2f);
            }

        }
    }
}
