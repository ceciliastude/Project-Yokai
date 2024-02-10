using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using System;
using System.Diagnostics;

namespace Project_Yokai
{
    public class Intro : GameScreen
    {

        private AudioManager audioManager;
        private GameState currentGameState;
        private SpriteBatch _spriteBatch;
        SpriteFont font;
        SpriteFont title;
        public event Action NextButtonClicked;

        // General textures and buttons
        Texture2D backgroundSprite;
        Texture2D panel1;
        Texture2D nameBox;
        Texture2D nextButton;
        Texture2D nextButtonHover;
        Rectangle nextButtonRectangle;
        bool nextButtonHovered;
        bool buttonPressed = false;



        public Intro(Game game) : base(game)
        {
            audioManager = new AudioManager();
            currentGameState = GameState.Intro;

        }

        public override void LoadContent()
        {
            Content.RootDirectory = "Content";
            backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/intoBackground");
            panel1 = Content.Load<Texture2D>("Assets/UI/Panels Gray/Panel 2_");
            nameBox = Content.Load<Texture2D>("Assets/UI/Panels Green/Panel 1");
            nextButton = Content.Load<Texture2D>("Assets/UI/Standart Button V1/Standart Button Normal/Standart Button Normal 1");
            nextButtonHover = Content.Load<Texture2D>("Assets/UI/Standart Button V1/Standart Button Hover/Standart Button Hover 1");

            nextButtonRectangle = new Rectangle(650, 390, nextButton.Width, nextButton.Height);

            audioManager.LoadContent(Content, "Music/River - HarumachiMusic");
            audioManager.PlayBackgroundMusic();

            font = Content.Load<SpriteFont>("placeHolderFont");
            title = Content.Load<SpriteFont>("nameFont");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        public void HandleMouseInput(MouseState mouseState)
        {
            nextButtonHovered = nextButtonRectangle.Contains(mouseState.X, mouseState.Y);

            if (nextButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && !buttonPressed)
            {
                buttonPressed = true;
            }

            if (nextButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Released && buttonPressed)
            {
                HandleNextButtonClick();
                buttonPressed = false;
            }
        }



        private void HandleNextButtonClick()
        {
            Debug.WriteLine("Loading next scene");
            NextButtonClicked?.Invoke();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            Rectangle destinationRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);

            Rectangle panel1Destination = new Rectangle(50, 300, 700, 150);
            _spriteBatch.Draw(panel1, panel1Destination, Color.White);

            Rectangle nameBoxDestination = new Rectangle(80, 265, 160, 50); 
            _spriteBatch.Draw(nameBox, nameBoxDestination, Color.White);

            _spriteBatch.Draw(nextButtonHovered ? nextButtonHover : nextButton, nextButtonRectangle, Color.White);


            string textToDraw = "System";
            Vector2 textPosition = new Vector2(100, 282); 
            Color textColor = Color.Black; 
            _spriteBatch.DrawString(title, textToDraw, textPosition, textColor);

            string textToDraw2 = "Placeholder dialogue";
            Vector2 textPosition2 = new Vector2(100, 320);
            Color textColor2 = Color.Black;
            _spriteBatch.DrawString(font, textToDraw2, textPosition2, textColor2);

            _spriteBatch.End();


        }

    }
}
