using CardGameApp.Management;
using System;
using System.Collections.Generic;

namespace CardGameApp
{
    class Program
    {
        /// <summary>
        /// Entry Point of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CardGame game = new CardGame(new CardManagement());
            game.StartGame();
            Console.ReadKey();
        }
    }
}
