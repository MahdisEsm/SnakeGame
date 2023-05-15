using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {
        public static void ClearScreen(int width,int height)
        {
            StringBuilder blank = new StringBuilder();
            
            for (int i = 0; i < width; i++)
            {
                blank.Append(" ");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(blank);
            }
        }

        static void Main(string[] args)
        {
            String tryPlaying=null;
            do
            {
                Console.Clear();
                Console.WindowHeight = 32;
                Console.WindowWidth = 64;
                int windowWidth = Console.WindowWidth;
                int windowHeight = Console.WindowHeight;

                List<int> snakeBodyX = new List<int>();
                List<int> snakeBodyY = new List<int>();
                int headX = windowWidth / 2;
                int headY = windowHeight / 2;
                int score = 3;
                int gameover = 0;
                Random randomFood = new Random();
                int randomFoodX = randomFood.Next(2, windowWidth - 1);
                int randomFoodY = randomFood.Next(2, windowHeight - 1);
                Console.CursorVisible = false;
                string move = "Right";
                DateTime startTime;
                DateTime keyTime;
                Console.SetCursorPosition(0, windowHeight / 2);
                string welcome = "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒" +
                    "▒▒▒▒▒▒▒▒▒▒▒▒▒Welcome to Snake Game";
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var item in welcome)
                {
                    Console.Write(item);
                    System.Threading.Thread.Sleep(70);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Are You Ready?Y/N");

                string response=Console.ReadLine();
                if (response == "Y" || response == "y")
                {
                    goto start;
                }
                else break;

                start:
                while (true)
                {
                    ClearScreen(windowWidth, windowHeight);

                    if (headX == windowWidth)
                    {
                        headX = 0;
                    }
                    else if (headX == 0)
                    {
                        headX = windowWidth;
                    }

                    if (headY == 0)
                    {
                        headY = windowHeight;
                    }
                    else if (headY == windowHeight)
                    {
                        headY = 0;
                    }

                    if (randomFoodX == headX && randomFoodY == headY)
                    {
                        score++;
                        randomFoodX = randomFood.Next(1, windowWidth - 2);
                        randomFoodY = randomFood.Next(1, windowHeight - 2);
                    }

                    for (int i = 0; i < snakeBodyX.Count(); i++)
                    {
                        Console.SetCursorPosition(snakeBodyX[i], snakeBodyY[i]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("o");
                        if (snakeBodyX[i] == headX && snakeBodyY[i] == headY)
                        {
                            gameover = 1;
                        }
                    }
                    if (gameover == 1)
                    {
                        break;
                    }


                    Console.SetCursorPosition(headX, headY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("0");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(randomFoodX, randomFoodY);
                    Console.Write("Φ");
                    startTime = DateTime.Now;
                    string keyPress = "No";


                    while (true)
                    {
                        keyTime = DateTime.Now;
                        if (keyTime.Subtract(startTime).TotalMilliseconds > 200) { break; }

                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo userKey = Console.ReadKey(true);

                            if (userKey.Key.Equals(ConsoleKey.UpArrow) && move != "Down" && keyPress == "No")
                            {
                                move = "Up";
                                keyPress = "Yes";
                            }
                            if (userKey.Key.Equals(ConsoleKey.DownArrow) && move != "Up" && keyPress == "No")
                            {
                                move = "Down";
                                keyPress = "Yes";
                            }
                            if (userKey.Key.Equals(ConsoleKey.LeftArrow) && move != "Right" && keyPress == "No")
                            {
                                move = "Left";
                                keyPress = "Yes";
                            }
                            if (userKey.Key.Equals(ConsoleKey.RightArrow) && move != "Left" && keyPress == "No")
                            {
                                move = "Right";
                                keyPress = "Yes";
                            }

                        }
                    }
                    snakeBodyX.Add(headX);
                    snakeBodyY.Add(headY);

                    switch (move)
                    {
                        case "Up":
                            headY--;
                            break;
                        case "Down":
                            headY++;
                            break;
                        case "Right":
                            headX++;
                            break;
                        case "Left":
                            headX--;
                            break;

                        default:
                            break;
                    }

                    if (snakeBodyX.Count() > score)
                    {
                        snakeBodyX.RemoveAt(0);
                        snakeBodyY.RemoveAt(0);
                    }

                }
                ClearScreen(windowWidth, windowHeight);
                Console.SetCursorPosition(windowWidth / 2, windowHeight / 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game over, Score: " + score);
                Console.SetCursorPosition(windowWidth / 2, (windowHeight / 2) + 1);
                Console.WriteLine("Do you want to play again?Y/N");
                tryPlaying = Console.ReadLine();

            } while (tryPlaying == "y" || tryPlaying == "Y");



            Console.ReadLine();


        }
    }
}
