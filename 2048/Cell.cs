using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Cell
    {
        private int number;
        
        public Cell(int number)
        {
            this.number = number;
        }

        public int GetNumber()
        {
            return this.number;
        }

        public void IncreaseCellNumber()
        {
            if(this.number > 0)
            {
                this.number *= 2;
            }

            else
            {
                this.number = 2;
            }
            
        }

        public void SetCellNumber(int newNum)
        {
            this.number = newNum;
        }

        public void ResetNumber()
        {
            this.number = 0;
        }

        public void PrintCell()
        {
            // Print max 4 digits, if there are less fill with gray blanks 
            if(this.number == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write("    ");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            else
            {
                Console.Write(this.number);
                int spacesLeft = 4 - this.number.ToString().Length;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                for(int i = 0; i < spacesLeft; i++)
                {
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.Write(" ");
        }
        
    }
}
