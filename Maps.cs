using System;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Maps
{
    private int rows;
    private int columns;
    public int[,] cells;
    public int[,] floor;
    private Texture2D[] Texture;
    private int[] valuesSafe = {2};

    private int size;

    public Maps(Texture2D[] Texture, int size = 32)
    {
        floor = new int[,]
        {
            {0,0,0,0,1,0,0,0,0,1,0,0,0,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        cells = new int[,]
        {
            {2,2,2,2,3,2,2,2,2,3,2,2,2,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2},

        };

        rows = cells.GetLength(1);
        columns = cells.GetLength(0);
        this.Texture = Texture;
        this.size = size;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 offset)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (floor[j, i] > 0) {
                    spriteBatch.Draw(Texture[floor[j, i]], new Rectangle((int)(i * size + offset.X), (int)(j * size + offset.Y), size, size), Color.White);
                }
            }
        }

        for (int i = 0; i < cells.GetLength(1); i++)
        {
            for (int j = 0; j < cells.GetLength(0); j++)
            {
                if (cells[j, i] > 0) {
                    spriteBatch.Draw(Texture[cells[j, i]], new Rectangle((int)(i * size + offset.X), (int)(j * size + offset.Y), size, size), Color.White);
                }
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
            if (value>0)
            {
                return false || isDoor(value);
            }
        }
        return true;
    }

    public bool isDoor(int value)
    {
        if (value == 3)
        {
            return true;
        }
        return false;
    }

    public void ResetMaps()
    {
        floor = new int[,]
        {
            {0,0,0,0,1,0,0,0,0,1,0,0,0,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        cells = new int[,]
        {
            {2,2,2,2,3,2,2,2,2,3,2,2,2,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2},

        };

        rows = cells.GetLength(1);
        columns = cells.GetLength(0);
    }
}