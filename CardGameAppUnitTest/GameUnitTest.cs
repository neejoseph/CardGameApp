using CardGameApp.Interface;
using CardGameApp.Management;
using CardGameApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardGameAppUnitTest
{
    public class GameUnitTest
    {     
        [Fact]
        public void AssignCardUnitTest()
        {
            CardManagement cd = new CardManagement();
            List<int> cardslist = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Player player1 = new Player() { PlayerId = "Player1", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
            Player player2 = new Player() { PlayerId = "Player2", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
            cd.AssignCards(cardslist, ref player1, ref player2);
            Assert.Equal(player1.DrawPile.Count, player2.DrawPile.Count);
        }

        [Fact]
        public void ShuffledCardIntialCardsTest()
        {
            CardManagement cd = new CardManagement();
            List<int> cardslist = new List<int>();
            List<int> shuffledcards = cd.ShuffleCards();
            Assert.NotEqual(cardslist, shuffledcards);
        }

        [Fact]
        public void FindWinnerTest()
        {
            CardManagement cdmananagement = new CardManagement();
            List<PlayerDashboard> playerdashBoard = new List<PlayerDashboard>()
                                                        {
                                                            new PlayerDashboard() { PlayerId = "Player1",issuccess=true } ,
                                                            new PlayerDashboard() { PlayerId = "Player2",issuccess=true } ,
                                                            new PlayerDashboard() { PlayerId = "Player1",issuccess=true } ,
                                                            new PlayerDashboard() { PlayerId = "Player2",issuccess=true } ,
                                                             new PlayerDashboard() { PlayerId = "Player1",issuccess=true } ,
                                                              new PlayerDashboard() { PlayerId = "Player1",issuccess=true }
                                                        };
            string winner = cdmananagement.FindWinner(playerdashBoard);
            Assert.Equal("Player 1", winner);
        }
        [Fact]
        public void PlayCardGameTest()
        {
            CardManagement cdmananagement = new CardManagement();
            List<int> shuffledcards = new List<int>() {2,3,4,8,9,7,6,10,5,9,7,3 };
           
            Player player1 = new Player() { PlayerId = "Player1", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
            Player player2 = new Player() { PlayerId = "Player2", DrawPile = new Stack<int>(), DiscardPile = new Stack<int>() };
            cdmananagement.AssignCards(shuffledcards, ref player1, ref player2);

            List<PlayerDashboard> dashboard = cdmananagement.PlayGame( player1,  player2);
            string winner = cdmananagement.FindWinner(dashboard);
            Assert.Equal("Player 2", winner);
        }
    }
}
