using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            // If the user input made a movement 
            Board board = new Board();
            int score = 0;
            board.PrintBoard();
            Console.WriteLine("Score: " + score);
            while (true) {
                char nextMove = ReadDirectionInput();
                var movement = board.Move(nextMove);

                if(movement.Item1)
                {
                    Console.Clear();
                    score += movement.Item2;
                    board.SetRandomCell();
                    board.PrintBoard();
                    Console.WriteLine("Score: " + score);
                }

                if (IsWin(board))
                {
                    Console.WriteLine("You Won!");
                    break;
                }

                else if (board.GetEmptyCellsCount() == 0)
                {
                    if (IsLoss(board))
                    {
                        Console.WriteLine("You Lost!");
                        break;
                    }
                }

                if (nextMove == 'Q')
                {
                    break;
                }
            }            
        }

        public static char ReadDirectionInput()
        {
            char direction = 'N';
            bool keyIsValid = false;
            while (!keyIsValid)
            {
                Console.Write("\nEnter key: ");
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = 'U';
                        keyIsValid = true;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = 'D';
                        keyIsValid = true;
                        break;

                   case ConsoleKey.RightArrow:
                   case ConsoleKey.D:
                        direction = 'R';
                        keyIsValid = true;
                        break;

                   case ConsoleKey.LeftArrow:
                   case ConsoleKey.A:
                        direction = 'L';
                        keyIsValid = true;
                        break;

                   case ConsoleKey.Q:
                   case ConsoleKey.Escape:
                       direction = 'Q';
                       keyIsValid = true;
                       break;

                   default:
                        Console.WriteLine("\nInput Isn't valid");
                        break;
                }
            }
            
            Console.WriteLine(direction);

            return direction;
        }

        public static bool IsLoss(Board board)
        {
            Cell[,] board_arr = board.GetBoard();
            bool loss = true;
            for(int i = 0; i < board_arr.GetLength(0) - 1; i++)
            {
                for(int j = 0; j < board_arr.GetLength(1) - 1; j++)
                {
                    if(board_arr[i, j].GetNumber() == board_arr[i, j + 1].GetNumber() || board_arr[i, j].GetNumber() == board_arr[i + 1, j].GetNumber())
                    {
                        loss = false;
                        break;
                    }
                }
            }
            return loss;
        }

        public static bool IsWin(Board board)
        {
            Cell[,] board_arr = board.GetBoard();
            for (int i = 0; i < board_arr.GetLength(0); i++)
            {
                for(int j = 0; j < board_arr.GetLength(1); j++)
                {
                    if(board_arr[i, j].GetNumber() == 2048)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
