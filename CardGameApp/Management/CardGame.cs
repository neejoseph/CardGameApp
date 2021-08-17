using CardGameApp.Interface;
using CardGameApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameApp.Management
{
    public class CardGame
    {
         private ICardGame gamemanagement;

        ///Constructor dependency injection
        public CardGame(ICardGame _gamemanagement)
        {
            this.gamemanagement = _gamemanagement;
           
        }

        /// <summary>
        /// Entry Point of the Game
        /// </summary>
      public void StartGame()
        {
            try
            {
                List<int> shufflecards = gamemanagement.ShuffleCards();
                Player player1 = new Player() { PlayerId = "Player1", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
                Player player2 = new Player() { PlayerId = "Player2", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
                gamemanagement.AssignCards(shufflecards, ref player1, ref player2);
                Console.WriteLine("Assigned cards of Player1");
                gamemanagement.DisplayCards(player1.DrawPile);
                Console.WriteLine("Assigned cards of Player2");
                gamemanagement.DisplayCards(player2.DrawPile);
                List<PlayerDashboard> dashboards = gamemanagement.PlayGame(player1, player2);
                string winner = gamemanagement.FindWinner(dashboards);

                Console.WriteLine(winner + " wins the game!");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
