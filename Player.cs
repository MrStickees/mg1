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
    public Vector2 positionSpawn;
    private Keys[] keys;
    private int currentFrame;
    private int totalFrames;
    private int millisecondsPerFrame;
    private int elapsedMilliseconds;
    private int speed;
    public bool isLoose = false;
    private int score = 0;

    private int size;

    public Player(Texture2D[] animationTextures, Vector2 position, int totalFrames, int millisecondsPerFrame, Keys[] keys, int size = 32)
    {
        this.animationTextures = animationTextures;
        this.positionSpawn = position;
        this.position = position;
        this.totalFrames = totalFrames;
        this.millisecondsPerFrame = millisecondsPerFrame;
        this.keys = keys;
        this.speed = size/16;
        this.size = size;
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
        Rectangle destinationRectangle = new Rectangle((int)centerX, (int)centerY, size, size);

        spriteBatch.Draw(animationTextures[currentFrame], destinationRectangle, sourceRectangle, Color.White);
    }

    public void Reset(Maps map, int score = 0)
    {
        position = positionSpawn;
        isLoose = false;
        this.score = score;
        map.ResetMaps();
    }

    public void isCollideDoor(Maps map)
    {
        bool randomBool = new Random().Next(0, 3) < 2;

        
        if (randomBool)
        {
            score++;
            Reset(map, score);
            Console.WriteLine("Win");
            Console.WriteLine("Score : " + score);
        }
        else
        {
            Console.WriteLine("Loose");
            Reset(map);
        }
    }

    public void Move(Maps map)
    {
        if (isLoose) return;
        if (Keyboard.GetState().IsKeyDown(keys[0]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / size), (int)(position.Y / size));
            int cellValue2 = map.GetCellValue((int)((position.X + size) / size), (int)(position.Y / size));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.X % size == 0)
            {
                position.Y -= speed;
            }

            cellValue1 = map.GetCellValue((int)(position.X / size), (int)(position.Y / size));
            cellValue2 = map.GetCellValue((int)((position.X + size) / size), (int)(position.Y / size));
            v1 = map.getSafe(cellValue1);
            v2 = map.getSafe(cellValue2);

            if (!v1 || !v2 && position.X % size != 0)
            {
                position.Y += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[1]))
        {
            int cellValue1 = map.GetCellValue((int)((position.X + size) / size), (int)(position.Y / size));
            int cellValue2 = map.GetCellValue((int)((position.X + size) / size), (int)((position.Y + size) / size));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.Y % size == 0)
            {
                position.X += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[2]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / size), (int)((position.Y + size) / size));
            int cellValue2 = map.GetCellValue((int)((position.X + size) / size), (int)((position.Y + size) / size));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.X % size == 0)
            {
                position.Y += speed;
            }
        }

        if (Keyboard.GetState().IsKeyDown(keys[3]))
        {
            int cellValue1 = map.GetCellValue((int)(position.X / size), (int)(position.Y / size));
            int cellValue2 = map.GetCellValue((int)(position.X / size), (int)((position.Y + size) / size));
            bool v1 = map.getSafe(cellValue1);
            bool v2 = map.getSafe(cellValue2);

            if (v1 && v2 || v1 && position.Y % size == 0)
            {
                position.X -= speed;
            }

            cellValue1 = map.GetCellValue((int)(position.X / size), (int)(position.Y / size));
            cellValue2 = map.GetCellValue((int)(position.X / size), (int)((position.Y + size) / size));

            v1 = map.getSafe(cellValue1);
            v2 = map.getSafe(cellValue2);

            if (!v1 || !v2 && position.Y % size != 0)
            {
                position.X += speed;
            }
        }

        if (Keyboard.GetState().GetPressedKeyCount() > 0)
        {
            int value = map.GetCellValue((int)(position.X / size), (int)(position.Y / size));
            bool isDoor = map.isDoor(value);

            if (isDoor)
            {
                isCollideDoor(map);
            }
            
        }
    }
}

