using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Project_Yokai
{
    public class Forest2 : GameScreen
    {

        private AudioManager audioManager;
        private GameState currentGameState;
        private SpriteBatch _spriteBatch;


        // General textures and buttons
        Texture2D backgroundSprite;
        Player player;
        int frameWidth = 64;
        int frameHeight = 64;
        int totalFrames = 8;
        float frameTime = 0.1f;


        public Forest2(Game game, Player player) : base(game)
        {
            audioManager = new AudioManager();
            currentGameState = GameState.Forest1;
            this.player = player;

        }

        public override void LoadContent()
        {
            Content.RootDirectory = "Content";
            backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/Map5");
            Texture2D playerSpriteSheet = Content.Load<Texture2D>("Assets/Sprites/sprite sheet (MC)");
            //player = new Player(playerSpriteSheet, new Vector2(100, 100));

            audioManager.LoadContent(Content, "Music/Adrian von Ziegler - Yuki");
            audioManager.PlayBackgroundMusic();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        public void HandleMouseInput(MouseState mouseState)
        {

        }
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            Rectangle destinationRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);
            player.Draw(_spriteBatch, scale: 0.5f);

            _spriteBatch.End();


        }

    }
}
