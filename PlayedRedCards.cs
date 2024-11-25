using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a
{
    internal class PlayedRedCards
    {
        private List<KeyValuePair<string, int>> _cards = new List<KeyValuePair<string, int>>();
        private Dictionary<int, string> _playedRedCards = new Dictionary<int, string>();

        public void PopulateRedPlayedCardsList(int playerIndex,string card)
        {
            //_cards.Add(card);
            _playedRedCards.Add(playerIndex, card);
        }

        public void ClearRedCardList()
        {
            _playedRedCards.Clear();
        }

        public Dictionary<int, string> PlayedRedCardsDict { get { return _playedRedCards; } set { _playedRedCards = value; } }
    }
}
