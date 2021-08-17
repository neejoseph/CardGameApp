using CardGameApp.Interface;
using CardGameApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameApp.Management
{
    public class CardManagement : ICardGame
    {
        static List<PlayerDashboard> playerDashboards = new List<PlayerDashboard>();
        Stack<int> playedCards = new Stack<int>();
        const int totalNumberofCards = 40;
        const int numberofPlayers = 2;
        List<PlayerDashboard> dashboard = new List<PlayerDashboard>();
     

        /// <summary>
        /// Shuffle Cards - It is used for shuffling the cards (intial set & discard pile)
        /// </summary>
        /// <param name="drawpilelist"></param>
        /// <returns> list of shuffled cards</returns>
        public List<int> ShuffleCards(List<int> drawpilelist = null)
        {
            try
            {
                Random rand = new Random();
                int count = 0;

                //Creating a list with 40 cards (intial addition of cards)
                if (drawpilelist == null)
                {
                    drawpilelist = new List<int>();

                    count = totalNumberofCards;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 1; j <= 10; j++)
                            drawpilelist.Add(j);
                    }
                }
                else
                    count = drawpilelist.Count; //DiscardPile count


                ///Shuffling of Cards
                for (int i = count - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);
                    if (j != i)
                    {
                        int curVal = drawpilelist[i];
                        drawpilelist[i] = drawpilelist[j];
                        drawpilelist[j] = curVal;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return drawpilelist;
        }

        /// <summary>
        /// Assign the initial set of first 40 cards to player1 and player2
        /// </summary>
        /// <param name="shufflelist"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void AssignCards(List<int> shufflelist, ref Player player1, ref Player player2)
        {
            try
            {
                for (int i = 0; i < shufflelist.Count; i = i + 2)
                {
                    player1.DrawPile.Push(shufflelist[i]);
                    player2.DrawPile.Push(shufflelist[i + 1]);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Display the cards assigned to each player
        /// </summary>
        /// <param name="drawpile"></param>
        public void DisplayCards(Stack<int> drawpile)
        {
            try
            {
                int[] arrayCards = drawpile.ToArray();
                string cards = string.Empty;
                for (int i = 0; i < arrayCards.Length; i++)
                {
                    cards = cards + arrayCards[i].ToString() + ",";
                }
                Console.WriteLine(cards.TrimEnd(','));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       
        /// <summary>
        /// Find the winner of the game
        /// </summary>
        /// <param name="playerdashBoard"></param>
        /// <returns></returns>
        public string FindWinner(List<PlayerDashboard> playerdashBoard)
        {
            string winner = string.Empty;
            try
            {
                if (playerdashBoard != null && playerdashBoard.Count > 0)
                {
                    int player1successcount = (from n in playerdashBoard
                                               where n.PlayerId == "Player1"
                                               select n).Count();
                    int player2successcount = (from n in playerdashBoard
                                               where n.PlayerId == "Player2"
                                               select n).Count();
                    if (player1successcount > player2successcount)
                        winner = "Player 1";
                    else if (player2successcount > player1successcount)
                        winner = "Player 2";
                    else if (player2successcount == player1successcount)
                        winner = "Player 1 & Player 2";

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return winner;
        }

        /// <summary>
        /// Starts playing the game
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        public List<PlayerDashboard> PlayGame(Player player1, Player player2)
        {
            try
            {
                int numberofCardsofEachPlayer = totalNumberofCards / numberofPlayers;

                int player1Cardno;
                int player2Cardno;
                int player1messagecard = numberofCardsofEachPlayer;
                int player2messagecard = numberofCardsofEachPlayer;
                for (int i = 0; i < numberofCardsofEachPlayer; i++)
                {
                    if (player1.DrawPile.Count== 0 || player2.DrawPile.Count == 0)
                        break;
                    player1Cardno =  player1.DrawPile.Pop();
                     player2Cardno = player2.DrawPile.Pop();
                    if (i == 0)
                    {
                        Console.WriteLine("Player 1 (" + player1messagecard + " cards) :" + player1Cardno);
                        Console.WriteLine("Player 2 (" + player2messagecard + " cards) :" + player2Cardno);
                    }
                    else
                    {
                        Console.WriteLine("Player 1 (" + (player1messagecard + i) + " cards) :" + player1Cardno);
                        Console.WriteLine("Player 2 (" + (player2messagecard - i) + " cards) :" + player2Cardno);
                    }
                    PlayCardGame(ref player1, ref player2, player1Cardno, player2Cardno);

                }
                Console.WriteLine("First set of the game is over,Going to play with DiscardPile ");
                DiscardPileEvaluation(ref player1, ref player2);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return dashboard;
        }

        /// <summary>
        /// If the previous round doesn't have any winner and assign those cards and current set of cards to the Drawpile of the winner of the current round.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playedCards"></param>
        private void AddPlayedCardstoDiscardPile(ref Player player, ref Stack<int> playedCards)
        {
            try
            {
                for (int i = 0; i < playedCards.Count; i++)
                {
                    player.DiscardPile.Push(playedCards.Pop());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign the shuffled discardpile of the player to the drawpile after one set  of cards were played.
        /// </summary>
        /// <param name="shufflelist"></param>
        /// <param name="player1"></param>
        private void AssignDiscardPileCards(List<int> shufflelist, ref Player player1)
        {
            try
            {
                for (int i = 0; i < shufflelist.Count; i++)
                {
                    player1.DrawPile.Push(shufflelist[i]);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add Cards to the discardpile of the winner in each round.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player1CardNo"></param>
        /// <param name="player2CardNo"></param>
        private void AddCardstoDiscardPile( ref Player player1,int player1CardNo,int player2CardNo)
        {
            try
            { 
            player1.DiscardPile.Push(player1CardNo);
            player1.DiscardPile.Push(player2CardNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Each round of the card game and selects the winner in each round
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="player1Cardno"></param>
        /// <param name="player2Cardno"></param>
        /// <returns></returns>
        private List<PlayerDashboard> PlayCardGame(ref Player player1, ref Player player2, int player1Cardno,int player2Cardno)
        {
            try
            { 
            if (player1Cardno > player2Cardno)
            {
                Console.WriteLine("Player 1 wins this round");
                dashboard.Add(new PlayerDashboard() { PlayerId = player1.PlayerId, issuccess = true });
                AddCardstoDiscardPile(ref player1, player1Cardno, player2Cardno);
                if (playedCards != null && playedCards.Count > 0)
                    AddPlayedCardstoDiscardPile(ref player1, ref playedCards);
            }
            else if (player1Cardno < player2Cardno)
            {
                Console.WriteLine("Player 2 wins this round");
                dashboard.Add(new PlayerDashboard() { PlayerId = player2.PlayerId, issuccess = true });
                AddCardstoDiscardPile(ref player2, player1Cardno, player2Cardno);
                if (playedCards != null && playedCards.Count > 0)
                    AddPlayedCardstoDiscardPile(ref player2, ref playedCards);
            }
            else if (player1Cardno == player2Cardno)
            {
                Console.WriteLine("No winner in this round");
                playedCards.Push(player1Cardno);
                playedCards.Push(player2Cardno);
            }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dashboard;
        }
        /// <summary>
        ///Playing the game with Discard pile
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private List<PlayerDashboard> DiscardPileEvaluation(ref Player player1, ref Player player2)
        {
            try
            { 
            int player1Cardno; int player2Cardno;
            if (player1.DiscardPile.Count > 0 && player2.DiscardPile.Count == 0)
                dashboard.Add(new PlayerDashboard() { PlayerId = player1.PlayerId, issuccess = true });
            else if (player2.DiscardPile.Count > 0 && player1.DiscardPile.Count == 0)
                dashboard.Add(new PlayerDashboard() { PlayerId = player2.PlayerId, issuccess = true });

            else if (player2.DiscardPile.Count > 0 && player1.DiscardPile.Count > 0)
            {
                int remainingMaxCards = player1.DiscardPile.Count > player2.DiscardPile.Count ? player1.DiscardPile.Count : player2.DiscardPile.Count;
                List<int> shuffledDiscardPile = ShuffleCards(player1.DiscardPile.ToList());
                AssignDiscardPileCards(shuffledDiscardPile, ref player1);
                shuffledDiscardPile = ShuffleCards(player2.DiscardPile.ToList());
                AssignDiscardPileCards(shuffledDiscardPile, ref player2);
                Console.WriteLine("Discard piles are shuffled and assigned back to the players");
                Console.WriteLine("Next set of cards for Player1");
                DisplayCards(player1.DrawPile);
                Console.WriteLine("Next set of cards for Player2");
                DisplayCards(player2.DrawPile);

                int player1messagecard = player1.DrawPile.Count;
                int player2messagecard = player2.DrawPile.Count;
                for (int i = 0; i < remainingMaxCards; i++)
                {
                    if (player1.DrawPile.Count == 0)
                    {
                        Console.WriteLine("There are no more cards in the drw pile of Player 1");
                        break;
                    }
                    if (player2.DrawPile.Count == 0)
                    {
                        Console.WriteLine("There are no more cards in the drw pile of Player 2");
                        break;
                    }
                   
                    player1Cardno = player1.DrawPile.Pop();
                    player2Cardno = player2.DrawPile.Pop();
                    if (i == 0)
                    {
                        Console.WriteLine("Player 1 (" + player1messagecard + " cards) :" + player1Cardno);
                        Console.WriteLine("Player 2 (" + player2messagecard + " cards) :" + player2Cardno);
                    }
                    else
                    {
                        Console.WriteLine("Player 1 (" + (player1messagecard + i) + " cards) :" + player1Cardno);
                        Console.WriteLine("Player 2 (" + (player2messagecard - i) + " cards) :" + player2Cardno);
                    }
                    PlayCardGame(ref player1, ref player2, player1Cardno, player2Cardno);
                }
              
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dashboard;
            //if (player1.DiscardPile.Count > 0 || player2.DiscardPile.Count > 0)
            //   DiscardPileEvaluation(ref player1, ref player2);
        }
    }
}
