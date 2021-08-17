using CardGameApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameApp.Interface
{
    public interface ICardGame
    {
        List<int> ShuffleCards(List<int> drawpilelist = null);
        void AssignCards(List<int> shufflelist, ref Player player1, ref Player player2);

        void DisplayCards(Stack<int> drawpile);
        List<PlayerDashboard> PlayGame(Player player1, Player player2);
        string FindWinner(List<PlayerDashboard> playerdashBoard);
    }
}
