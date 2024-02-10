using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens.Transitions;
using System.Diagnostics;

namespace Project_Yokai
{
    public class Main : Game
    {
        //General
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;

        //Player specific
        private Player player;

        //GameScreens
        private MainMenu mainMenu;
        private Intro introStart;
        private Forest1 forest1;
        private Hidden1 hidden1;
        private Hidden2 hidden2;
        private Shrine shrineStart;
        private Forest2 forest2;
        private Cave caveStart;



        //OptionPanel
        private ControlsPanel controlsPanel;

        //GameStates
        private GameState _currentGameState;


        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            player = new Player(new Vector2(100, 100));
            mainMenu = new MainMenu(this);
            introStart = new Intro(this);
            forest1 = new Forest1(this, player);
            hidden1 = new Hidden1(this, player);
            hidden2 = new Hidden2(this, player);
            shrineStart = new Shrine(this, player);
            forest2 = new Forest2(this, player);
            caveStart = new Cave(this, player);

            _currentGameState = GameState.MainMenu;

            mainMenu.PlayButtonClicked += MainMenu_PlayButtonClicked;
            introStart.NextButtonClicked += Intro_NextButtonClicked;
            forest1.hidden1Reached += Forest1_hidden1Reached;
            forest1.shrineReached += Forest1_shrineReached;
            hidden1.kunaiObtained += Hidden1_kunaiObtained;

        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            Texture2D kunaiTexture = Content.Load<Texture2D>("Assets/Sprites/kunai");
            Texture2D playerSpriteSheet = Content.Load<Texture2D>("Assets/Sprites/sprite sheet (MC)");
            player.LoadContent(playerSpriteSheet, kunaiTexture, Content);

            mainMenu.LoadContent();
            introStart.LoadContent();
            forest1.LoadContent();
            hidden1.LoadContent();
            shrineStart.LoadContent();


            _spriteBatch = new SpriteBatch(GraphicsDevice);

            controlsPanel = new ControlsPanel(GraphicsDevice, Content);
            _screenManager.LoadScreen(mainMenu);
        }

        //GameState changes
        private void MainMenu_PlayButtonClicked()
        {
            _currentGameState = GameState.Intro;
            _screenManager.LoadScreen(introStart, new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void Intro_NextButtonClicked()
        {
            _currentGameState = GameState.Forest1;
            _screenManager.LoadScreen(forest1, new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void Forest1_hidden1Reached()
        {

            _currentGameState = GameState.Hidden1;
            _screenManager.LoadScreen(hidden1, new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void Forest1_shrineReached()
        {
            Debug.WriteLine("Shrine loaded");
            _currentGameState = GameState.Shrine;
            _screenManager.LoadScreen(shrineStart, new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void Hidden1_kunaiObtained()
        {
            player.currentPlayerState = PlayerState.HasObtainedItem;

        }

        public GameState CurrentGameState
        {
            get { return _currentGameState; }
            set { _currentGameState = value; }
        }

        private bool _displayControlsPanel = false;
        private bool displayControlsPanel
        {
            get { return _displayControlsPanel; }
            set { _displayControlsPanel = value; }
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    mainMenu.HandleMouseInput(mouseState);
                    break;
                case GameState.Intro:
                    introStart.HandleMouseInput(mouseState);
                    break;
                case GameState.Forest1:
                    forest1.HandleMouseInput(mouseState);
                    break;
                case GameState.Hidden1:
                    hidden1.HandleMouseInput(mouseState);
                    break;
                case GameState.Shrine:
                    shrineStart.HandleMouseInput(mouseState);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
