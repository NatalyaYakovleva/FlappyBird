using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlappyBird
{
    class Flappy
    {
        int Width { set; get; }

        int Height { set; get; }
        Board board;
        Bird bird;
        Wall wall1;
        Wall wall2;
        Wall wall3;
        int score;

        public Flappy(int width, int height)
        {
            Width = width;
            Height = height;
        }

        void Setup()
        {
            board = new Board(Width, Height);
            bird = new Bird(Height, Height / 2);
            wall1 = new Wall(35, Width, Height);
            wall2 = new Wall(60, Width, Height);
            wall3 = new Wall(85, Width, Height);
            score = 0;
            board.Write();
            Console.SetCursorPosition((Width / 2) - 4, Height + 2);
            Console.Write("Score: ");
            bird.Write();
            wall1.Move();
            wall2.Move();
            wall3.Move();
        }

        public void Run()
        {
            while (true)
            { 
                Console.Clear();
                Setup();
                
                Console.ReadKey(true);
                while (bird.Y < Height && bird.Y > 1)
                {
                    if(((bird.X >= wall1.X - 2 && bird.X <= wall1.X + 2) && (bird.Y <= wall1.Y - 1 || bird.Y >= wall1.Y + 2))
                        || ((bird.X >= wall2.X - 2 && bird.X <= wall2.X + 2) && (bird.Y <= wall2.Y - 1 || bird.Y >= wall2.Y + 2))
                        || ((bird.X >= wall3.X - 2 && bird.X <= wall3.X + 2) && (bird.Y <= wall3.Y - 1 || bird.Y >= wall3.Y + 2)))
                    {
                        Console.Beep();
                        break;
                    }
                    if(wall1.X == bird.X || wall2.X == bird.X || wall3.X == bird.X)
                    {
                        score++;
                    }
                    bird.Logic();
                    wall1.Move();
                    wall2.Move();
                    wall3.Move();
                    Console.SetCursorPosition((Width / 2) + 3, Height + 2);
                    Console.Write(score);
                    Thread.Sleep(300);
                }
            }
        }
    }

    class Wall
    {
        public int X { set; get; }

        public int Y { set; get; }
        Random random;
        int boardWidth;
        int boardHeight;

        public Wall(int x, int boardWidth, int boardHeight)
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            random = new Random();
            X = x;
            Y = random.Next(3, boardHeight - 2);
            Thread.Sleep(10);
        }

        public void Move()
        {
            Write("\0");
            X--;
            Write();
            if(X - 2 <= 0)
            {
                X = 75;
                Y = random.Next(3, boardHeight - 2);
            }
        }

        void Write()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if(X - 2 >= 1 && X + 2 <= boardWidth - 1)
            {
                for (int i = 1; i < Y - 2; i++)
                {
                    Console.SetCursorPosition(X - 2, i);
                    Console.Write("│");
                    Console.SetCursorPosition(X + 2, i);
                    Console.Write("│");
                }
                for (int i = boardHeight; i > Y + 2; i--)
                {
                    Console.SetCursorPosition(X - 2, i);
                    Console.Write("│");
                    Console.SetCursorPosition(X + 2, i);
                    Console.Write("│");
                }
                for (int i = X - 1; i < X + 2; i++)
                {
                    Console.SetCursorPosition(i, Y + 2);
                    Console.Write("─");
                    Console.SetCursorPosition(i, Y - 2);
                    Console.Write("─");
                }
                Console.SetCursorPosition(X + 2, Y - 2);
                Console.Write("┘");
                Console.SetCursorPosition(X - 2, Y - 2);
                Console.Write("└");
                Console.SetCursorPosition(X + 2, Y + 2);
                Console.Write("┐");
                Console.SetCursorPosition(X - 2, Y + 2);
                Console.Write("┌");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        void Write(string str)
        {
            if (X - 2 >= 1 && X + 2 <= boardWidth - 1)
            {
                for (int i = 1; i < Y - 2; i++)
                {
                    Console.SetCursorPosition(X - 2, i);
                    Console.Write(str);
                    Console.SetCursorPosition(X + 2, i);
                    Console.Write(str);
                }
                for (int i = boardHeight; i > Y + 2; i--)
                {
                    Console.SetCursorPosition(X - 2, i);
                    Console.Write(str);
                    Console.SetCursorPosition(X + 2, i);
                    Console.Write(str);
                }
                for (int i = X - 1; i < X + 2; i++)
                {
                    Console.SetCursorPosition(i, Y + 2);
                    Console.Write(str);
                    Console.SetCursorPosition(i, Y - 2);
                    Console.Write(str);
                }
                Console.SetCursorPosition(X + 2, Y - 2);
                Console.Write(str);
                Console.SetCursorPosition(X - 2, Y - 2);
                Console.Write(str);
                Console.SetCursorPosition(X + 2, Y + 2);
                Console.Write(str);
                Console.SetCursorPosition(X - 2, Y + 2);
                Console.Write(str);
            }
        }
    }
    class Bird
    {
        public int X { set; get; }

        public int Y { set; get; }

        ConsoleKeyInfo keyInfo;
        ConsoleKey consoleKey;

        public Bird(int x, int y)
        {
            X = x;
            Y = y;
            keyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();
        }

        void Input()
        {
            if(Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
            }
        }

        public void Logic()
        {
            Input();
            if (consoleKey == ConsoleKey.UpArrow)
            {
                Up();
            }
            else
            {
                if (consoleKey == ConsoleKey.DownArrow)
                {
                    Down();
                }
            }
            consoleKey = ConsoleKey.A;
        }

        void Up()
        {
            Write("\0");
            Y--;
            Write();
        }

        void Down()
        {
            Write("\0");
            Y++;
            Write();
        }

        

        public void Write()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y - 1);
            Console.Write("▄");
            Console.SetCursorPosition(X - 1, Y - 1);
            Console.Write("▄");
            Console.SetCursorPosition(X - 1, Y);
            Console.Write("▀");
            Console.SetCursorPosition(X, Y);
            Console.Write("▀");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Write(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y - 1);
            Console.Write(str);
            Console.SetCursorPosition(X - 1, Y - 1);
            Console.Write(str);
            Console.SetCursorPosition(X - 1, Y);
            Console.Write(str);
            Console.SetCursorPosition(X, Y);
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Board
    {
        public int Height { set; get; }

        public int Width { set; get; }


        public Board()
        {
            Height = 20;
            Width = 80;
        }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Write()
        {
            for (int i = 1; i <= Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("─");
            }
            for (int i = 1; i <= Width; i++)
            {
                Console.SetCursorPosition(i, Height + 1);
                Console.Write("─");
            }
            for (int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
            }
            for(int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(Width + 1, i);
                Console.Write("│");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            Console.SetCursorPosition(Width + 1, 0);
            Console.Write("┐");
            Console.SetCursorPosition(0, Height + 1);
            Console.Write("└");
            Console.SetCursorPosition(Width + 1, Height + 1);
            Console.Write("┘");

        }
    }
}
