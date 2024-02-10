using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Yokai;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;

//Animation logic class
//Used to identify how many frames X & Y, how many rows and more specifics. 
public class Animation
{
    private Texture2D texture;
    private List<Rectangle> sourceRectangles;
    private int totalFrames;
    private int currentFrame;
    private float frameTime;
    private float timeElapsed;
    private bool isActive;

    public Animation(Texture2D texture, int framesX, int framesY, float frameTime, int row = 1)
    {
        this.texture = texture;
        this.frameTime = frameTime;
        this.timeElapsed = 0f;
        this.isActive = true;

        // Calculate frame dimensions
        int frameWidth = texture.Width / framesX;
        int frameHeight = texture.Height / framesY;

        // Initialize source rectangles
        sourceRectangles = new List<Rectangle>();
        for (int i = 0; i < framesX; i++)
        {
            sourceRectangles.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
        }

        totalFrames = sourceRectangles.Count;
        currentFrame = 0;
    }

    public void Stop()
    {
        isActive = false;
    }

    public void Start()
    {
        isActive = true;
    }

    public void Reset()
    {
        currentFrame = 0;
        timeElapsed = 0f;
    }

    public void Update(GameTime gameTime)
    {
        if (!isActive)
            return;

        timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        while (timeElapsed > frameTime)
        {
            timeElapsed -= frameTime;
            currentFrame = (currentFrame + 1) % totalFrames;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
    {
        spriteBatch.Draw(texture, position, sourceRectangles[currentFrame], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
