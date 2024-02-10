using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Timers;
using Project_Yokai.Content.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Project_Yokai
{
    public class Forest1 : GameScreen
    {

        private AudioManager audioManager;
        private SpriteBatch _spriteBatch;
        public event Action hidden1Reached;
        public event Action shrineReached;
        bool hasReachedDestination = false;

        private Vector2 ScreenChangePosition { get; } = new Vector2(350, 8);
        private Vector2 ScreenChangePosition2 { get; } = new Vector2(1136, 606);

        private List<MovableStones> movableStones;
        private List<Obstacles> obstacles;
        private Obstacles resetStone;

        //Camera
        private Camera camera;
        private int mapWidth = 1200;
        private int mapHeight = 709;

        // General textures and buttons
        Texture2D backgroundSprite;
        private Player player;
        int frameWidth = 64;
        int frameHeight = 64;
        int totalFrames = 8;
        float frameTime = 0.1f;


        public Forest1(Game game, Player player) : base(game)
        {
            audioManager = new AudioManager();
            camera = new Camera();
            this.player = player;
            movableStones = new List<MovableStones>();
            obstacles = new List<Obstacles>();

        }

        public override void LoadContent()
        {
            Content.RootDirectory = "Content";
            backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/Map1");
            Texture2D playerSpriteSheet = Content.Load<Texture2D>("Assets/Sprites/sprite sheet (MC)");

            //Music
            audioManager.LoadContent(Content, "Music/Adrian von Ziegler - Yuki");
            audioManager.PlayBackgroundMusic();
            camera.Initialize(GraphicsDevice.Viewport, mapWidth, mapHeight);

            //Movable stones
            //Each stone has a 75 pixel distance from each other, Y = 150-525
            Texture2D stoneSprite = Content.Load<Texture2D>("Assets/Sprites/movable stone (1)");
            movableStones.Add(new MovableStones(new Vector2(330, 525), Vector2.Zero, stoneSprite));
            movableStones.Add(new MovableStones(new Vector2(400, 300), Vector2.Zero, stoneSprite));
            movableStones.Add(new MovableStones(new Vector2(330, 300), Vector2.Zero, stoneSprite));
            movableStones.Add(new MovableStones(new Vector2(330, 150), Vector2.Zero, stoneSprite));
            movableStones.Add(new MovableStones(new Vector2(100, 375), Vector2.Zero, stoneSprite));
            movableStones.Add(new MovableStones(new Vector2(100, 225), Vector2.Zero, stoneSprite));

            //Obstacles
            Texture2D obstacleSprite = Content.Load<Texture2D>("Assets/Sprites/stone");
            obstacles.Add(new Obstacles(new Vector2(100, 525), Vector2.Zero, obstacleSprite));

            Texture2D resetStoneSprite = Content.Load<Texture2D>("Assets/Sprites/reset post");
            resetStone = new Obstacles(new Vector2(380, 550), Vector2.Zero, resetStoneSprite);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.SetInitialPosition(new Vector2(35, 606));

        }
        public void HandleMouseInput(MouseState mouseState)
        {

            if (player.HasReachedDestination(ScreenChangePosition))
            {
                hidden1Destination();

            }

            if (player.HasReachedDestination(ScreenChangePosition2))
            {
                shrineDestination();
            }


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


        public void hidden1Destination()
        {
            if (!hasReachedDestination)
            {
                Debug.WriteLine("Loading next scene");
                hasReachedDestination = true;
                hidden1Reached?.Invoke();
            }
        }

        public void shrineDestination()
        {
            if (!hasReachedDestination)
            {
                Debug.WriteLine("Loading next scene");
                hasReachedDestination = true;
                shrineReached?.Invoke();
            }
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (var stone in movableStones)
            {
                if (player.Hitbox.Intersects(stone.Hitbox))
                {
                    Vector2 separationVector = CalculateSeparationVector(player.Hitbox, stone.Hitbox);
                    float scaleFactor = 0.5f;
                    if (Math.Abs(separationVector.X) < Math.Abs(separationVector.Y))
                    {
                        player.SetInitialPosition(new Vector2(player.Position.X, player.Position.Y + separationVector.Y));
                    }

                    else
                    {
                        player.SetInitialPosition(new Vector2(player.Position.X + separationVector.X, player.Position.Y));
                    }
                    stone.Move(separationVector * scaleFactor);

                }
            }
            foreach (var obstacle in obstacles)
            {
                if (player.Hitbox.Intersects(obstacle.Hitbox))
                {
                    Vector2 separationVectorPlayer = CalculateSeparationVector(player.Hitbox, obstacle.Hitbox);

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

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: camera.Transform);

            int scaledWidth = (int)(GraphicsDevice.Viewport.Width * 1.5f);
            int scaledHeight = (int)(GraphicsDevice.Viewport.Height * 1.5f);

            Rectangle destinationRectangle = new Rectangle(0, 0, scaledWidth, scaledHeight);
            _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);

            foreach (var stone in movableStones)
            {
                stone.Draw(_spriteBatch, scale: 0.3f);
            }
            foreach (var obstacle in obstacles)
            {
                obstacle.Draw(_spriteBatch, scale: 0.3f);
            }
            resetStone.Draw(_spriteBatch, scale: 0.4f);
            player.Draw(_spriteBatch, scale: 0.6f);

            _spriteBatch.End();


        }

    }
}
