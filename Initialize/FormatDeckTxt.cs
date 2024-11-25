using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a.Initialize
{
    internal class FormatDeckTxt
    {
        public List<string> FormatDeck(string stringDeck)
        {
            List<string> cards = new List<string>();
            string[] cardsSplitArray = stringDeck.Split('\n');

            PushCardArrayToList(cardsSplitArray, cards);
            return cards;
        }

        private void PushCardArrayToList(string[] allCards, List<string> deckTarget)
        {
            foreach (string card in allCards)
            {
                deckTarget.Add(card);
            }
        }
    }
}
