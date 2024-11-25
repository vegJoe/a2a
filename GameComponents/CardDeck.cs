using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a.GameComponents
{
    internal class CardDeck
    {
        private List<string> _cards = new List<string>();

        public CardDeck() 
        {
            
        }

        public List<string> Cards { get { return _cards; } set { _cards = value; } }
    }
}
