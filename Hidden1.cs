using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Project_Yokai.Content.Code;
using System;


public class Hidden1 : GameScreen
{
    private GameState currentGameState;
    private SpriteBatch _spriteBatch;

    private AudioManager audioManager;
    private const float InteractionDistance = 50.0f;

    //Events
    public event Action hidden1Exited;
    public event Action kunaiObtained;
    bool hasReachedExit = false;
    bool hasObtainedItem = false;

    // General textures and buttons
    Texture2D backgroundSprite;

    private Player player;
    private Kunai kunai;
    private KunaiItem kunaiItem;

    int frameWidth = 64;
    int frameHeight = 64;
    int totalFrames = 8;
    float frameTime = 0.1f;

    public Hidden1(Game game, Player player) : base(game)
    {
        currentGameState = GameState.Forest1;
        audioManager = new AudioManager();
        this.player = player;
    }

    public override void LoadContent()
    {
        Content.RootDirectory = "Content";
        backgroundSprite = Content.Load<Texture2D>("Assets/Backgrounds/Map2");
        Texture2D kunaiItemTexture = Content.Load<Texture2D>("Assets/Sprites/glowing kunai");
        audioManager.LoadSoundEffect(Content, "Sfx/success-bell");
        kunaiItem = new KunaiItem(kunaiItemTexture);

        kunaiItem.SetInitialPosition(new Vector2(372, 170));
        player.SetInitialPosition(new Vector2(372, 440));
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public void HandleMouseInput(MouseState mouseState)
    {
        Vector2 playerPosition = player.Position;

        Vector2 kunaiItemPosition = kunaiItem.Position;

        float distance = Vector2.Distance(playerPosition, kunaiItemPosition);

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Space) && distance < InteractionDistance && !kunaiItem.IsObtained)
        {
            KunaiObtained();

        }
    }

    public void KunaiObtained()
    {
        if (!hasObtainedItem)
        {
            kunaiItem.Interact();
            hasObtainedItem = true;
            audioManager.PlaySoundEffect();
            kunaiObtained?.Invoke();
        }

    }

    public override void Update(GameTime gameTime)
    {

        player.Update(gameTime, useSpecialBounds: true);
        kunaiItem.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        Rectangle destinationRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        _spriteBatch.Draw(backgroundSprite, destinationRectangle, Color.White);
        kunaiItem.Draw(_spriteBatch, scale: 0.2f);
        player.Draw(_spriteBatch, scale: 0.5f);
        _spriteBatch.End();
    }
}
