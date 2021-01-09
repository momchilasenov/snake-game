using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake
{
    struct Position
    {

        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int right = 0;
            int left = 1;
            int down = 2;
            int up = 3;

            int consolewidth = 60;
            int consoleheight = 30;

            Console.WindowWidth = consolewidth;
            Console.WindowHeight = consoleheight-1;

            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            Console.CursorVisible = false;

            Position[] directions = new Position[]
            {
                new Position(0,1), //right
                new Position(0,-1), //left
                new Position(1,0), //down
                new Position(-1,0), //up

            };

            int sleepTime = 100;
            int direction = right; //var for the currentDirection based on the indexes in the directions array

            Random randomNumbersGenerator = new Random();

            Position food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));

            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("@");

            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i <= 6; i++)
            {
                //snakeBody
                snakeElements.Enqueue(new Position(0, i));
            }

            foreach (Position position in snakeElements)
            {
                //print the snake
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("*");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right)
                        {
                            direction = left;
                        }
                    }
                    else if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left)
                        {
                            direction = right;
                        }
                    }
                    else if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down)
                        {
                            direction = up;
                        }
                    }
                    else if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up)
                        {
                            direction = down;
                        }
                    }
                }

                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);

                if (snakeNewHead.col < 0)
                {
                    snakeNewHead.col = Console.WindowWidth - 1;
                }
                if (snakeNewHead.row < 0)
                {
                    snakeNewHead.row = Console.WindowHeight - 1;
                }
                if (snakeNewHead.row >= Console.WindowHeight)
                {
                    snakeNewHead.row = 0;
                }
                if (snakeNewHead.col >= Console.WindowWidth)
                {
                    snakeNewHead.col = 0;
                }

                if (snakeElements.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Game over!");
                    int userPoints = (snakeElements.Count - 6) * 100;
                    Console.WriteLine($"Total points: {userPoints}");
                    return;
                }

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("*");

                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feed the snake
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food));

                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("@");

                    if (sleepTime > 30)
                    {
                        sleepTime -= 5;
                    }
                }
                else
                {
                    // moving the snake
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.WriteLine(" ");
                }

                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("@");

                Thread.Sleep(sleepTime);
            }

        }
    }
}

