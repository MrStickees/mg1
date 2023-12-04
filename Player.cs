using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private Texture2D[] animationTextures;
    public Vector2 position;
    private Keys[] keys;
    private int currentFrame;
    private int totalFrames;
    private int millisecondsPerFrame;
    private int elapsedMilliseconds;
    private float speed;

    public Player(Texture2D[] animationTextures, Vector2 position, int totalFrames, int millisecondsPerFrame, Keys[] keys, float speed = 8)
    {
        this.animationTextures = animationTextures;
        this.position = position;
        this.totalFrames = totalFrames;
        this.millisecondsPerFrame = millisecondsPerFrame;
        this.keys = keys;
        this.speed = speed;
    }

    public void Update(GameTime gameTime, Maps map)
    {
        elapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedMilliseconds >= millisecondsPerFrame)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            elapsedMilliseconds = 0;
        }
        if (Keyboard.GetState().IsKeyUp(keys[0]) && Keyboard.GetState().IsKeyUp(keys[1]) && Keyboard.GetState().IsKeyUp(keys[2]) && Keyboard.GetState().IsKeyUp(keys[3]))
        {
            currentFrame = 0;
            elapsedMilliseconds = 0;
        }
        
        Move(map);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 offset)
    {
        int frameWidth = animationTextures[currentFrame].Width;
        int frameHeight = animationTextures[currentFrame].Height;

        float centerX = offset.X;
        float centerY = offset.Y;

        Rectangle sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)centerX, (int)centerY, 32, 32);

        spriteBatch.Draw(animationTextures[currentFrame], destinationRectangle, sourceRectangle, Color.White);
    }

    public void Move(Maps map)
    {
        if (Keyboard.GetState().IsKeyDown(keys[0]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / 32), (int)(position.Y / 32));
            int cellValue2 = map.GetCellValue((int)((position.X + 32) / 32), (int)(position.Y / 32));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.X % 32 == 0)
            {
                position.Y -= speed;
            }

            cellValue1 = map.GetCellValue((int)(position.X / 32), (int)(position.Y / 32));
            cellValue2 = map.GetCellValue((int)((position.X + 32) / 32), (int)(position.Y / 32));
            v1 = map.getSafe(cellValue1);
            v2 = map.getSafe(cellValue2);

            if (!v1 || !v2 && position.X % 32 != 0)
            {
                position.Y += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[1]))
        {
            int cellValue1 = map.GetCellValue((int)((position.X + 32) / 32), (int)(position.Y / 32));
            int cellValue2 = map.GetCellValue((int)((position.X + 32) / 32), (int)((position.Y + 32) / 32));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.Y % 32 == 0)
            {
                position.X += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[2]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / 32), (int)((position.Y + 32) / 32));
            int cellValue2 = map.GetCellValue((int)((position.X + 32) / 32), (int)((position.Y + 32) / 32));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.X % 32 == 0)
            {
                position.Y += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[3]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / 32), (int)(position.Y / 32));
            int cellValue2 = map.GetCellValue((int)(position.X / 32), (int)((position.Y + 32) / 32));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.Y % 32 == 0)
            {
                position.X -= speed;
            }

            cellValue1 = map.GetCellValue((int)(position.X / 32), (int)(position.Y / 32));
            cellValue2 = map.GetCellValue((int)(position.X / 32), (int)((position.Y + 32) / 32));

            v1 = map.getSafe(cellValue1);
            v2 = map.getSafe(cellValue2);

            if (!v1 || !v2 && position.Y % 32 != 0)
            {
                position.X += speed;
            }
        }
    }

}

