using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Project_Yokai.Content.Code
{
    //The game's "settings". Currently just a display of controls.
    public class ControlsPanel
    {
        private Texture2D controlsScreen;
        private Texture2D buttons;
        private Texture2D spaceBar;
        private Texture2D backButton;
        private Texture2D backButtonHover;
        SpriteFont font;
        SpriteFont title;
        Rectangle controlsScreenRectangle;
        Rectangle spaceBarRectangle;
        Rectangle backButtonRectangle;

        bool displayOptionsMenu = false;
        bool backButtonHovered;
        bool backButtonPressed = false;
        public ControlsPanel(GraphicsDevice graphicsDevice, ContentManager content)
        {
            LoadTextures(content);
            float scaleFactor = 0.5f;

            controlsScreenRectangle = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            spaceBarRectangle = new Rectangle(420, 310, (int)(spaceBar.Width * scaleFactor), (int)(spaceBar.Height * scaleFactor));
            backButtonRectangle = new Rectangle(20, 20, backButton.Width, backButton.Height);

        }

        private void LoadTextures(ContentManager content)
        {
            controlsScreen = content.Load<Texture2D>("Assets/UI/Panels Gray/Panel 4_");
            buttons = content.Load<Texture2D>("Assets/UI/Custom Big Buttons/Custom Buttons Normal/Custom Button Normal 3");
            spaceBar = content.Load<Texture2D>("Assets/UI/Panels Green/Panel 1");
            backButton = content.Load<Texture2D>("Assets/UI/Custom Big Buttons/Custom Buttons Normal/Custom Button Normal 2");
            backButtonHover = content.Load<Texture2D>("Assets/UI/Custom Big Buttons/Custom Buttons Hover/Custom Button Hover 2");
            font = content.Load<SpriteFont>("placeHolderFont");
            title = content.Load<SpriteFont>("nameFont");

        }
        public void Update(MouseState mouseState)
        {
            backButtonHovered = backButtonRectangle.Contains(mouseState.X, mouseState.Y);

            if (backButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed && !backButtonPressed)
            {
                backButtonPressed = true;
            }
            if (backButtonRectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Released && backButtonPressed)
            {
                displayOptionsMenu = !displayOptionsMenu;
                Debug.WriteLine("Exiting options...");
                backButtonPressed = false;

            }


        }
        public bool DisplayOptionsMenu
        {
            get { return displayOptionsMenu; }
        }

        public void ToggleOptionsMenu()
        {
            displayOptionsMenu = !displayOptionsMenu;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (displayOptionsMenu)
            {
                spriteBatch.Draw(controlsScreen, controlsScreenRectangle, Color.LightGreen);
                spriteBatch.Draw(buttons, new Vector2(160, 250), Color.White);
                spriteBatch.Draw(buttons, new Vector2(85, 325), Color.White);
                spriteBatch.Draw(buttons, new Vector2(160, 325), Color.White);
                spriteBatch.Draw(buttons, new Vector2(235, 325), Color.White);
                spriteBatch.Draw(spaceBar, spaceBarRectangle, Color.White);
                spriteBatch.Draw(backButtonHovered ? backButtonHover : backButton, backButtonRectangle, Color.White);

                string textToDraw = "Interact/Attack";
                Vector2 textPosition = new Vector2(450, 200);
                Color textColor = Color.White;
                spriteBatch.DrawString(title, textToDraw, textPosition, textColor);

                string textToDraw2 = "W";
                Vector2 textPosition2 = new Vector2(180, 270);
                Color textColor2 = Color.White;
                spriteBatch.DrawString(title, textToDraw2, textPosition2, textColor2);


                string textToDraw3 = "A";
                Vector2 textPosition3 = new Vector2(105, 345);
                Color textColor3 = Color.White;
                spriteBatch.DrawString(title, textToDraw3, textPosition3, textColor3);

                string textToDraw4 = "S";
                Vector2 textPosition4 = new Vector2(185, 345);
                Color textColor4 = Color.White;
                spriteBatch.DrawString(title, textToDraw4, textPosition4, textColor4);


                string textToDraw5 = "D";
                Vector2 textPosition5 = new Vector2(260, 345);
                Color textColor5 = Color.White;
                spriteBatch.DrawString(title, textToDraw5, textPosition5, textColor5);

                string textToDraw6 = "Spacebar";
                Vector2 textPosition6 = new Vector2(490, 340);
                Color textColor6 = Color.White;
                spriteBatch.DrawString(title, textToDraw6, textPosition6, textColor6);

                string textToDraw7 = "Move";
                Vector2 textPosition7 = new Vector2(162, 200);
                Color textColor7 = Color.White;
                spriteBatch.DrawString(title, textToDraw7, textPosition7, textColor7);

                string textToDraw8 = "Back";
                Vector2 textPosition8 = new Vector2(35, 35);
                Color textColor8 = Color.White;
                spriteBatch.DrawString(font, textToDraw8, textPosition8, textColor8);



            }

        }
    }
}
