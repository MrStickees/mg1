using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Maps
{
    private int rows;
    private int columns;
    private int[,] cells;
    private Texture2D[] Texture;
    private int[] valuesSafe = { -1, 1, 3 };


    public Maps(Texture2D[] Texture)
    {
        cells = new int[,]
        {
            {1,1,1,1},
            {1,0,2,1},
            {1,0,0,1},
            {1,1,1,1}
        };
        rows = cells.GetLength(1);
        columns = cells.GetLength(0);
        this.Texture = Texture;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 offset)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                spriteBatch.Draw(Texture[cells[j, i]], new Rectangle((int)(i * 32 + offset.X), (int)(j * 32 + offset.Y), 32, 32), Color.White);
            }
        }
    }

    public int GetCellValue(int row, int column)
    {
        if (row < 0 || row == rows || column < 0 || column == columns)
        {
            return -1;
        }
        return cells[column, row];
    }

    public bool getSafe(int value)
    {
        for (int i = 0; i < valuesSafe.Length; i++)
        {
            if (value == valuesSafe[i])
            {
                return false;
            }
        }
        return true;
    }
}