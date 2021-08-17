using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameApp.Model
{
    /// <summary>
    /// Player Model
    /// </summary>
    public class Player
    {
        public string PlayerId { get; set; }
        public Stack<int> DrawPile { get; set; }
        public bool  isSuccess { get; set; }
        public Stack<int> DiscardPile { get; set; }
    }
}
