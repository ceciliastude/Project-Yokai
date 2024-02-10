using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using System;
using System.Diagnostics;

namespace Project_Yokai
{
    public class MainMenu : GameScreen
    {
        private AudioManager audioManager;
        private GameState currentGameState;
        private SpriteBatch _spriteBatch;

        public bool displayControlsPanel = false;
        bool optionsButtonWasPressed = false;
        bool playButtonWasPressed = false;
        public event Action PlayButtonClicked;

        // General textures and buttons
        Texture2D backgroundSprite;
        Texture2D buttons;
        Texture2D buttonHover;

        Rectangle button1Rectangle;
        Rectangle button2Rectangle;
        Rectangle button3Rectangle;

        SpriteFont font;
        SpriteFont title;

        bool button1Hovered;
        bool button2Hovered;
        bool button3Hovered;

        // Options textures
        ControlsPanel controlsPanel;

        public MainMenu(Game game) : base(game)
        {
            audioManager = new AudioManager();
            currentGameState = GameState.MainMenu;

        }
        public override void LoadContent()
        {
            Content.RootDirectory = "Content";
            backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/Dark_forest");
            buttons = Content.Load<Texture2D>("Assets/UI/Standart Button V1/Standart Button Normal/Standart Button Normal 5");
            buttonHover = Content.Load<Texture2D>("Assets/UI/Standart Button V1/Standart Button Hover/Standart Button Hover 5");
            audioManager.LoadContent(Content, "Music/Peaceful Garden - HarumachiMusic");
            audioManager.PlayBackgroundMusic();

            button1Rectangle = new Rectangle(300, 200, buttons.Width, buttons.Height);
            button2Rectangle = new Rectangle(300, 270, buttons.Width, buttons.Height);
            button3Rectangle = new Rectangle(300, 340, buttons.Width, buttons.Height);

            font = Content.Load<SpriteFont>("placeHolderFont");
            title = Content.Load<SpriteFont>("nameFont");

            controlsPanel = new ControlsPanel(GraphicsDevice, Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void HandleMouseInput(MouseState mouseState)
        {
            button1Hovered = button1Rectangle.Contains(mouseState.X, mouseState.Y);
            button2Hovered = button2Rectangle.Contains(mouseState.X, mouseState.Y);
            button3Hovered = button3Rectangle.Contains(mouseState.X, mouseState.Y);
            controlsPanel.Update(Mouse.GetState());

            if (button1Rectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && !playButtonWasPressed)
            {
                playButtonWasPressed = true;
            }

            if (button1Rectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Released && playButtonWasPressed)
            {
                Debug.WriteLine("Loading next scene");
                playButtonWasPressed = false;
                HandlePlayButtonClick();
            }

            if (button2Rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && !optionsButtonWasPressed)
                {
                    optionsButtonWasPressed = true;
                    HandleOptionsButtonClick();
                }
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                optionsButtonWasPressed = false;
            }

            if (button3Rectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Debug.WriteLine("Exiting...");
                Game.Exit();
            }
        }

        private void HandleOptionsButtonClick()
        {
            Debug.WriteLine("Displaying options");
            controlsPanel.ToggleOptionsMenu();
        }
        private void HandlePlayButtonClick()
        {
            Debug.WriteLine("Loading next scene");
            PlayButtonClicked?.Invoke();
        }
        public override void Update(GameTime gameTime)
        {
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            Rectangle destinationRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);
            _spriteBatch.Draw(button1Hovered ? buttonHover : buttons, new Vector2(300, 200), Color.White);
            _spriteBatch.Draw(button2Hovered ? buttonHover : buttons, new Vector2(300, 270), Color.White);
            _spriteBatch.Draw(button3Hovered ? buttonHover : buttons, new Vector2(300, 340), Color.White);

            string textToDraw = "Project Yokai";
            Vector2 textPosition = new Vector2(330, 100);
            Color textColor = Color.White;
            _spriteBatch.DrawString(title, textToDraw, textPosition, textColor);

            string textToDraw2 = "Play";
            Vector2 textPosition2 = new Vector2(382, 210);
            Color textColor2 = Color.White;
            _spriteBatch.DrawString(font, textToDraw2, textPosition2, textColor2);

            string textToDraw3 = "Controls";
            Vector2 textPosition3 = new Vector2(370, 280);
            Color textColor3 = Color.White;
            _spriteBatch.DrawString(font, textToDraw3, textPosition3, textColor3);

            string textToDraw4 = "Quit";
            Vector2 textPosition4 = new Vector2(382, 350);
            Color textColor4 = Color.White;
            _spriteBatch.DrawString(font, textToDraw4, textPosition4, textColor4);

            if (controlsPanel.DisplayOptionsMenu)
            {
                controlsPanel.Draw(_spriteBatch);
            }
            _spriteBatch.End();



        }
    }
}
