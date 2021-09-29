using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Board
    {
        private Cell[,] board;
        private int emptyCellsCounter;

        public Board()
        {
            this.board = new Cell[4, 4];
            this.emptyCellsCounter = this.board.GetLength(0) * this.board.GetLength(1);
            // Create an empty board, then set two random indexes and fill them
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    this.board[i, j] = new Cell(0);
                }
            }

            for (int z = 0; z < 2; z++)
            {
                SetRandomCell();
            }
        }

        public Cell[,] GetBoard()
        {
            return this.board;
        }

        public int GetEmptyCellsCount()
        {
            return emptyCellsCounter;
        }
        public void SetRandomCell()
        {
            // If there are at least 1 empty cell, try to find random empty cell index for a new cell to become not empty
            Random rnd = new Random();
            if (this.emptyCellsCounter > 0)
            {
                while(true)
                {
                    int x = rnd.Next(0, 4);
                    int y = rnd.Next(0, 4);
                    if(this.board[x, y].GetNumber() == 0)
                    {
                        this.board[x, y].IncreaseCellNumber();
                        this.emptyCellsCounter--;
                        break;
                    }
                }
            }
            
        }

        /*
         * MoveBoard Algorithm:
         * Loop over every cell in current row/col.
         * If the movement is from the last index to the first one (Up and Left) in the relative dimension start in the second index,
         * and in each third loop iteration move the cell as closest as possible to the other side of the row/col,
         * without double additions of cells in the same movement, and remove spaces.
         * If a movement happened change moved to true and if an addition happened add to score.
         * Else, start in the second to last index, to the first index, and do the same.
         */
        public Tuple<bool, int> MoveBoard(char direction)
        {
            bool moved = false;
            int score = 0;
            bool[] joined = new bool[4] { false, false, false, false };

            switch (direction)
            {
                case 'U':
                    for (int j = 0; j < this.board.GetLength(1); j++)
                    {
                        joined = new bool[4] { false, false, false, false };
                        for (int i = 1; i < this.board.GetLength(0); i++)
                        {
                            if (this.board[i, j].GetNumber() != 0)
                            {
                                for (int z = i; z > 0; z--)
                                {

                                    if (this.board[z, j].GetNumber() == this.board[z - 1, j].GetNumber() && !joined[z] && !joined[z - 1])
                                    {
                                        this.board[z, j].ResetNumber();
                                        this.board[z - 1, j].IncreaseCellNumber();
                                        this.emptyCellsCounter++;
                                        score += this.board[z - 1, j].GetNumber();
                                        moved = true;
                                        joined[z] = false;
                                        joined[z - 1] = true;
                                    }

                                    else if (this.board[z - 1, j].GetNumber() == 0)
                                    {
                                        this.board[z - 1, j].SetCellNumber(this.board[z, j].GetNumber());
                                        this.board[z, j].ResetNumber();
                                        moved = true;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 'D':
                    for (int j = 0; j < this.board.GetLength(1); j++)
                    {
                        joined = new bool[4] { false, false, false, false };
                        for (int i = this.board.GetLength(0) - 2; i > -1; i--)
                        {
                            if (this.board[i, j].GetNumber() != 0)
                            {
                                for (int z = i; z < this.board.GetLength(1) - 1; z++)
                                    if (this.board[z, j].GetNumber() == this.board[z + 1, j].GetNumber() && !joined[z] && !joined[z + 1])
                                    {
                                        this.board[z, j].ResetNumber();
                                        this.board[z + 1, j].IncreaseCellNumber();
                                        this.emptyCellsCounter++;
                                        score += this.board[z + 1, j].GetNumber();
                                        moved = true;
                                        joined[z] = false;
                                        joined[z + 1] = true;
                                    }

                                    else if (this.board[z + 1, j].GetNumber() == 0)
                                    {
                                        this.board[z + 1, j].SetCellNumber(this.board[z, j].GetNumber());
                                        this.board[z, j].ResetNumber();
                                        moved = true;
                                    }
                            }
                        }
                    }
                    break;

                case 'L':
                    for(int i = 0; i < this.board.GetLength(0); i++)
                    {
                        joined = new bool[4] { false, false, false, false };
                        for (int j = 1; j < this.board.GetLength(1); j++)
                        {
                            if (this.board[i, j].GetNumber() != 0)
                            {
                                for (int z = j; z > 0; z--)
                                {
                                    if (this.board[i, z].GetNumber() == this.board[i, z - 1].GetNumber() && !joined[z] && !joined[z - 1])
                                    {
                                        this.board[i, z].ResetNumber();
                                        this.board[i, z - 1].IncreaseCellNumber();
                                        this.emptyCellsCounter++;
                                        score += this.board[i, z - 1].GetNumber();
                                        moved = true;
                                        joined[z] = false;
                                        joined[z - 1] = true;
                                    }

                                    else if (this.board[i, z - 1].GetNumber() == 0)
                                    {
                                        this.board[i, z - 1].SetCellNumber(this.board[i, z].GetNumber());
                                        this.board[i, z].ResetNumber();
                                        moved = true;
                                    }
                                }
                            }
                        }
                    }
                break;

                case 'R':
                    for(int i = 0; i < this.board.GetLength(0); i++)
                    {
                        joined = new bool[4] { false, false, false, false };
                        for (int j = this.board.GetLength(1) - 2; j > -1; j--)
                        {
                            if (this.board[i, j].GetNumber() != 0)
                            {
                                for(int z = j; z < this.board.GetLength(1) - 1; z++)
                                {
                                    if (this.board[i, z].GetNumber() == this.board[i, z + 1].GetNumber() && !joined[z] && !joined[z + 1])
                                    {

                                        this.board[i, z].ResetNumber();
                                        this.board[i, z + 1].IncreaseCellNumber();
                                        this.emptyCellsCounter++;
                                        score += this.board[i, z + 1].GetNumber();
                                        moved = true;
                                        joined[z] = false;
                                        joined[z + 1] = true;
                                    }

                                    else if (this.board[i, z + 1].GetNumber() == 0)
                                    {
                                        this.board[i, z + 1].SetCellNumber(this.board[i, z].GetNumber());
                                        this.board[i, z].ResetNumber();
                                        moved = true;
                                    }
                                }     
                            }
                        }
                    }
                break;
            }
            return Tuple.Create(moved, score);
        }

        public void PrintBoard()
        {
            for(int i = 0; i < this.board.GetLength(0); i++)
            {
                for(int j = 0; j < this.board.GetLength(1); j++)
                {
                    board[i, j].PrintCell();
                }
                Console.WriteLine("\n");
            }
        }
    }
}
