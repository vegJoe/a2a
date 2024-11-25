using a2a.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a
{
    internal static class GameUI
    {
        public static void AskNumOfUsers()
        {
            Console.Write("Enter number of players (Not less than 4): ");
        }

        public static void ErrorMsgWrongInputNotRecognized()
        {
            Console.WriteLine("Could not process input. Not a number or out of range?.");
        }

        public static void ShowCard(string card)
        {
            Console.WriteLine($"\nGreen card on table: {card}");
        }

        public static void PlayersCard(string card, string player)
        {
            Console.WriteLine($"{player} choose card {card}");
        }

        public static Dictionary<int, int> ShowCard(Dictionary<int, string> cards)
        {
            int i = 0;
            Dictionary<int, int> displayedNumPlayerIndex = new Dictionary<int, int>();

            foreach(var card in cards)
            {
                Console.WriteLine($"{i}. {card.Value}");
                displayedNumPlayerIndex.Add(i, card.Key);
                i++;
            }

            return displayedNumPlayerIndex;
        }

        public static void ShowCard(List<string> cards)
        {
            int i = 0;
            foreach (var card in cards)
            {
                Console.WriteLine($"{i}. {card}");
                i++;
            }
        }

        public static void EnterCardNumber()
        {
            Console.Write("Enter card number: ");
        }

        public static void EnterWinningCardNumber()
        {
            Console.Write("Enter the winning card number: ");
        }
        public static void GameOptions()
        {
            Console.WriteLine("\nOPTIONS:");
            Console.WriteLine("1. Show cards");
            Console.WriteLine("2. Choose card");
            Console.WriteLine("3. Show player score");
            Console.WriteLine("4. Quit Game\n");
        }

        internal static void PlayerWon(string player)
        {
            Console.WriteLine($"Player {player} won!!");
            Console.WriteLine("Press enter to quit...");
        }

        internal static void ShowScoreCount(int playerScoreCount)
        {
            Console.WriteLine($"Player score is: {playerScoreCount}");
        }

        internal static void WinningCard(string winningCard)
        {
            Console.WriteLine($"Card that won {winningCard}");
        }
    }
}
