using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project_Yokai
{
    public class Shrine : GameScreen
    {

        private SpriteBatch _spriteBatch;
        private Lantern lantern;
        private Obstacles obstacles;
        private const float InteractionDistance = 80.0f;
        private AudioManager audioManager;

        //Camera
        private Camera camera;
        private int mapWidth = 1200;
        private int mapHeight = 709;

        // General textures and buttons
        Texture2D backgroundSprite;
        Player player;
        int frameWidth = 64;
        int frameHeight = 64;
        int totalFrames = 8;
        float frameTime = 0.1f;

        private List<Lantern> lanterns;

        public Shrine(Game game, Player player) : base(game)
        {
            camera = new Camera();
            this.player = player;
            lanterns = new List<Lantern>();
            audioManager = new AudioManager();

        }

        public override void LoadContent()
        {
            camera.Initialize(GraphicsDevice.Viewport, mapWidth, mapHeight);

            Content.RootDirectory = "Content";
            backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/Map4");
            Texture2D playerSpriteSheet = Content.Load<Texture2D>("Assets/Sprites/sprite sheet (MC)");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.SetInitialPosition(new Vector2(35, 35));

            Texture2D lanternSprite = Content.Load<Texture2D>("Assets/Sprites/lantern (unlit)");
            Texture2D lanternSpriteAnimated = Content.Load<Texture2D>("Assets/Sprites/lantern (lit)");
            Texture2D toriiGate = Content.Load<Texture2D>("Assets/Sprites/gate");
            obstacles = new Obstacles(new Vector2(585, 10), Vector2.Zero, toriiGate);
            lantern = new Lantern(Vector2.Zero, Vector2.Zero, lanternSprite, lanternSpriteAnimated);
            lanterns.Add(new Lantern(new Vector2(70, 52), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(10, 280), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(90, 450), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(10, 640), Vector2.Zero, lanternSprite, lanternSpriteAnimated));

            lanterns.Add(new Lantern(new Vector2(780, 550), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(600, 550), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(780, 400), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(600, 400), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(780, 250), Vector2.Zero, lanternSprite, lanternSpriteAnimated));
            lanterns.Add(new Lantern(new Vector2(600, 250), Vector2.Zero, lanternSprite, lanternSpriteAnimated));

            audioManager.LoadSoundEffect(Content, "SFX/wind-chime");

        }
        public void HandleMouseInput(MouseState mouseState)
        {
            Vector2 playerPosition = player.Position;

            foreach (var lantern in lanterns)
            {
                Vector2 lanternPosition = lantern.Position;
                float distance = Vector2.Distance(playerPosition, lanternPosition);

                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Space) && distance < InteractionDistance && !lantern.isLit)
                {
                    lantern.LightUp();
                    audioManager.PlaySoundEffect();

                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (var lantern in lanterns)
            {
                lantern.Update(gameTime);

                if (player.Hitbox.Intersects(lantern.Hitbox))
                {
                    Vector2 separationVectorPlayer = CalculateSeparationVector(player.Hitbox, lantern.Hitbox);

                    if (Math.Abs(separationVectorPlayer.X) < Math.Abs(separationVectorPlayer.Y))
                    {
                        player.SetInitialPosition(new Vector2(player.Position.X, player.Position.Y + separationVectorPlayer.Y));
                    }
                    else
                    {
                        player.SetInitialPosition(new Vector2(player.Position.X + separationVectorPlayer.X, player.Position.Y));
                    }
                }
            }

            camera.Follow(player.Position);
        }

        // Helper method to calculate a separation vector between two rectangles
        private Vector2 CalculateSeparationVector(Rectangle rect1, Rectangle rect2)
        {
            float overlapX = Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left);
            float overlapY = Math.Min(rect1.Bottom, rect2.Bottom) - Math.Max(rect1.Top, rect2.Top);

            float separationX = 0;
            float separationY = 0;

            bool fromLeft = rect1.Right > rect2.Left && rect1.Left < rect2.Left;
            bool fromRight = rect1.Left < rect2.Right && rect1.Right > rect2.Right;
            bool fromTop = rect1.Bottom > rect2.Top && rect1.Top < rect2.Top;
            bool fromBottom = rect1.Top < rect2.Bottom && rect1.Bottom > rect2.Bottom;

            if (Math.Abs(overlapX) < Math.Abs(overlapY))
            {
                separationX = fromLeft ? -overlapX : fromRight ? overlapX : 0;
            }
            else
            {
                separationY = fromTop ? -overlapY : fromBottom ? overlapY : 0;
            }

            return new Vector2(separationX, separationY);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: camera.Transform);

            int scaledWidth = (int)(GraphicsDevice.Viewport.Width * 1.5f);
            int scaledHeight = (int)(GraphicsDevice.Viewport.Height * 1.5f);

            Rectangle destinationRectangle = new Rectangle(0, 0, scaledWidth, scaledHeight);
            _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);
            player.Draw(_spriteBatch, scale: 0.6f);
            foreach (var lantern in lanterns)
            {
                lantern.Draw(_spriteBatch, scale: 0.4f);
            }
            obstacles.Draw(_spriteBatch, scale: 0.7f);

            _spriteBatch.End();


        }

    }
}
