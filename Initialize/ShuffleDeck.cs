using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a.Initialize
{
    internal static class ShuffleDeck
    {
        public static void GetShuffledDeck(List<string> deckToShuffle)
        {
            deckToShuffle.Sort((a, b) => Random.Shared.Next(-1, 2));
        }
    }
}
