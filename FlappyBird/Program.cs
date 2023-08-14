using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Flappy Bird C#";
            Console.CursorVisible = false;
            Flappy flappy = new Flappy(75, 20);
            flappy.Run();
            Console.ReadKey();
        }
    }
}
