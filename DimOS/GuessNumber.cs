using System;
using System.Collections.Generic;
using System.Text;

namespace DimOS.App.Games
{
    public class GuessNumber
    {
        static Random _rand = new Random();
        public static void Start()
        {
            //Игра доделана!
            Console.Clear();
            Console.WriteLine(@"Welcome to ""Guess number"" game!"+Environment.NewLine+"Version 1.0");
            Console.WriteLine(
                "Choose difficulity level:\n" +
                "1 - 1 to 10\n" +
                "2 - 1 to 25\n" +
                "3 - 1 to 50\n" +
                "4 - 1 to 75\n" +
                "5 - 1 to 100\n" +
                "0 - exit");
            Console.Write(">");
            bool guessed = false;
            int random = 0;
            int difficulity = int.Parse(Console.ReadLine());

                 if (difficulity == 1)
                random = _rand.Next(1, 10);
            else if (difficulity == 2)
                random = _rand.Next(1, 25);
            else if (difficulity == 3)
                random = _rand.Next(1, 50);
            else if (difficulity == 4)
                random = _rand.Next(1, 75);
            else if (difficulity == 5)
                random = _rand.Next(1, 100);
            else if (difficulity == 0)
                Kernel.restartKernel();

            while (!guessed)
            {
                Console.Write(">");
                int readen = int.Parse(Console.ReadLine());
                if (readen == 0)
                    Kernel.restartKernel();
                if (readen < random)
                    Console.WriteLine("Wrong! The number is more than {0}", readen);
                else if (readen > random)
                    Console.WriteLine("Wrong! The number is less than {0}", readen);
                else if (readen == random)
                    guessed = true;
            }
            Console.WriteLine("Yay! You won! <3" + Environment.NewLine + "The number you typed was right!");
            Kernel.restartKernel();
        }
    }
}
