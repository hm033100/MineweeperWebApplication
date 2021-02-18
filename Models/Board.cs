using MineweeperWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class Board
    {
        public int size { get; set; }
        public int difficulty { get; set; }
        public int liveBombs { get; set; }
        public Cell[,] theGrid { get; set; }


        public Board(int sizeSent)
        {
            size = sizeSent;

            theGrid = new Cell[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public void setupLiveNeighbors()
        {
            Random rand = new Random();
            switch (difficulty)
            {
                case 1:
                    for (int i = 0; i < 15; i++)
                    {
                        int j, k;
                        j = rand.Next(0, size);
                        k = rand.Next(0, size);
                        if (theGrid[j, k].IsLive == true)
                        {
                            i--;
                        }
                        else
                        {
                            theGrid[j, k].IsLive = true;
                            liveBombs++;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < 30; i++)
                    {
                        int j, k;
                        j = rand.Next(0, size);
                        k = rand.Next(0, size);
                        if (theGrid[j, k].IsLive == true)
                        {
                            i--;
                        }
                        else
                        {
                            theGrid[j, k].IsLive = true;
                            liveBombs++;
                        }
                    }
                    break;

                case 3:
                    for (int i = 0; i < 45; i++)
                    {
                        int j, k;
                        j = rand.Next(0, size);
                        k = rand.Next(0, size);
                        if (theGrid[j, k].IsLive == true)
                        {
                            i--;
                        }
                        else
                        {
                            theGrid[j, k].IsLive = true;
                            liveBombs++;
                        }
                    }
                    break;

            }
        }

        public void calculateLiveNeighbors()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (theGrid[i, j].IsLive == true)
                        theGrid[i, j].liveNeighbors = 9;
                    else
                    {
                        try
                        {
                            if (theGrid[i + 1, j].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i - 1, j].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i + 1, j + 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i + 1, j - 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i - 1, j + 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i - 1, j - 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i, j + 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                        try
                        {
                            if (theGrid[i, j - 1].IsLive == true)
                                theGrid[i, j].liveNeighbors++;
                        }
                        catch { }
                    }
                }
            }
        }

        public void floodFill(int r, int c)
        {
            int[] x = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] y = { 0, 0, 1, -1, -1, 1, 1, -1 };
            for (int i = 0; i < 8; i++)
            {
                if (isValid(r + x[i], c + y[i]))
                {
                    if (theGrid[r + x[i], c + y[i]].isVisible == false && theGrid[r + x[i], c + y[i]].liveNeighbors == 0)
                    {
                        theGrid[r + x[i], c + y[i]].isVisible = true;
                        floodFill(r + x[i], c + y[i]);
                    }
                    else
                    {
                        theGrid[r + x[i], c + y[i]].isVisible = true;
                    }
                }
            }

        }

        private bool isValid(int r, int c)
        {
            if (r >= 0 && c >= 0 && r < size && c < size)
                return true;

            return false;
        }

        public bool winCondition()
        {
            int covered = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!theGrid[i, j].isVisible)
                    {
                        covered++;
                    }
                }
            }
            if (covered == liveBombs)
            {
                return true;
            }

            return false;
        }

        public void resetLiveNeighbors()
        {
            this.liveBombs = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    theGrid[i, j].IsLive = false;
                    theGrid[i, j].isVisible = false;
                    theGrid[i, j].liveNeighbors = 0;
                }
            }
        }
    }
}
