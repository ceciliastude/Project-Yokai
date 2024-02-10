using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project_Yokai.Content.Code
{
    //The game's input manager. Inserts logic for when the player walks in specific directions. 
    public static class InputManager
    {
        private static Vector2 direction;
        public static Vector2 Direction => direction;
        private static KeyboardState keyboardState;

        public static KeyboardState CurrentKeyboardState => keyboardState;

        public static bool Moving => direction != Vector2.Zero;

        public static void Update()
        {
            direction = Vector2.Zero;
            keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (keyboardState.IsKeyDown(Keys.A)) direction.X--;
                if (keyboardState.IsKeyDown(Keys.D)) direction.X++;
                if (keyboardState.IsKeyDown(Keys.W)) direction.Y--;
                if (keyboardState.IsKeyDown(Keys.S)) direction.Y++;
            }
        }
    }
}
