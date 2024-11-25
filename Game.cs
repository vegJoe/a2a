using a2a.GameComponents;
using a2a.Initialize;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace a2a
{
    public sealed class Game
    {
        private static Game? _game;
        private GameSetup _gameSetup;
        private string? _playerInput;
        private Dictionary<int, int> _displayedCardPlayerIndex;

        public static Game GetGame()
        {
            if (_game == null)
            {
                _game = new Game();
                _game.GameSetup();
            }
            return _game;
        }

        private void GameSetup()
        {
            _gameSetup = new GameSetup();
            GetShuffledDeck();
            DealCards();

            PlayGame();
        }

        private void GetShuffledDeck()
        {
            ShuffleDeck.GetShuffledDeck(_gameSetup.GreenDeck);
            ShuffleDeck.GetShuffledDeck(_gameSetup.RedDeck);
        }

        private void DealCards()
        {
            foreach(var player in _gameSetup.Players)
            {
                while(player.Cards.Count < 7)
                {
                    player.Cards.Add(_gameSetup.RedDeck[0]);
                    _gameSetup.RedDeck.RemoveAt(0);
                }

                //for (int i = 0; i < 7; i++)
                //{
                //    player.Cards.Add(_gameSetup.RedDeck[0]);
                //    _gameSetup.RedDeck.RemoveAt(0);
                //}
            }
        }

        private void PlayGame()
        {
            int greenDeckCardNumber = _gameSetup.GreenDeck.Count - 1;
            int judgeOrder = _gameSetup.Players.FindIndex(x => x.IsJudge == true);
            

            while(_gameSetup.RunGame)
            {
                GameUI.ShowCard(_gameSetup.GreenDeck[greenDeckCardNumber]);


                WaitForPlayerInput();


                _displayedCardPlayerIndex = GameUI.ShowCard(_gameSetup.PlayedRedCardsDict.PlayedRedCardsDict);
                
                JudgeVotesOnCard(judgeOrder);

                greenDeckCardNumber--;

                if(judgeOrder != _gameSetup.Players.Count - 1)
                {
                    judgeOrder++;
                }else
                {
                    judgeOrder = 0;
                }

                _gameSetup.PlayedRedCardsDict.ClearRedCardList();
                _displayedCardPlayerIndex.Clear();

                SetNewJudge(judgeOrder);

                DealCards();

                CheckScoreCount();
            }
        }

        private void CheckScoreCount()
        {
            int scoreLimit = 0;

            if(_gameSetup.Players.Count >= 8)
            {
                scoreLimit = 4;
            }else
            {
                winningConditions.TryGetValue(_gameSetup.Players.Count, out scoreLimit);
            }

            foreach (var player in _gameSetup.Players)
            {
                if(player.WinningHands == scoreLimit)
                {
                    GameUI.PlayerWon(player.GetType().Name);
                    Console.ReadLine();
                    _gameSetup.RunGame = false;
                }
            }
        }

        private void JudgeVotesOnCard(int judgeIndex)
        {
            Random rnd = new Random();
            int winningCard = 0;
            bool failedInput = false;

            if(_gameSetup.Players[judgeIndex] is BotPlayer)
            {
                //winningCard = _gameSetup.PlayedRedCardsDict.PlayedRedCardsDict.First().Key;
                winningCard = _gameSetup.PlayedRedCardsDict.PlayedRedCardsDict.ElementAt(rnd.Next(0, _gameSetup.PlayedRedCardsDict.PlayedRedCardsDict.Count)).Key;
            }
            else
            {
                GameUI.EnterWinningCardNumber();
                failedInput = int.TryParse(Console.ReadLine(), out winningCard);
                if (!failedInput || winningCard < 0 || winningCard > _gameSetup.PlayedRedCardsDict.PlayedRedCardsDict.Count)
                {
                    GameUI.ErrorMsgWrongInputNotRecognized();
                }
            }

            GameUI.WinningCard(_gameSetup.PlayedRedCardsDict.PlayedRedCardsDict[winningCard]);

            SetWinner(winningCard);
        }

        private void SetWinner(int winningCard)
        {
            int winner = 0;
            _displayedCardPlayerIndex.TryGetValue(winningCard, out winner);

            _gameSetup.Players[winner].WinningHands++;
        }

        private void WaitForPlayerInput()
        {
            int chosenCard = 0;

            GameUI.GameOptions();

            if (_gameSetup.Players[0].IsJudge == true)
            {
                _playerInput = "-1";
            }else
            {
                _playerInput = Console.ReadLine();
            }



            switch (_playerInput)
            {
                case "-1":
                    PopulateRedCardsList(-1);
                    break;
                case "1":
                    GameUI.ShowCard(_gameSetup.Players[0].Cards);
                    WaitForPlayerInput();
                    break;
                case "2":
                    GameUI.EnterCardNumber();
                    bool resp = int.TryParse(Console.ReadLine(), out chosenCard);
                    if(!resp || chosenCard < 0 || chosenCard > 6)
                    {
                        GameUI.ErrorMsgWrongInputNotRecognized();
                    } else
                    {
                        PopulateRedCardsList(chosenCard);
                    }
                    break;
                case "3":
                    GameUI.ShowScoreCount(_gameSetup.Players[0].WinningHands);
                    WaitForPlayerInput();
                    break;
                case "4":
                    _gameSetup.RunGame = false;
                    break;
                default:
                    WaitForPlayerInput();
                    break;
            }
        }

        private void PopulateRedCardsList(int cardNumber)
        {
            Random rnd  = new Random();

            for (int playerIndex = 0; playerIndex < _gameSetup.Players.Count; playerIndex++)
            {
                if (_gameSetup.Players[playerIndex] is BotPlayer && _gameSetup.Players[playerIndex].IsJudge == false)
                {
                    SendCardAndDelete(playerIndex, rnd.Next(0, 6));
                }
                else if (_gameSetup.Players[playerIndex] is HumanPlayer && _gameSetup.Players[playerIndex].IsJudge == false)
                {
                    SendCardAndDelete(playerIndex, cardNumber);
                }
            }

            //_gameSetup.PlayedRedCardsList.PlayedRedCards = _gameSetup.PlayedRedCardsList.Cards.OrderBy(x => Random.Shared.Next()).ToList();
        }

        private void SendCardAndDelete(int playerIndex, int cardNumber)
        {
            _gameSetup.PlayedRedCardsDict.PopulateRedPlayedCardsList(playerIndex, _gameSetup.Players[playerIndex].Cards[cardNumber]);
            _gameSetup.Players[playerIndex].Cards.RemoveAt(cardNumber);
        }

        private void SetNewJudge(int playerNumberToBeJudge)
        {
            foreach (var player in _gameSetup.Players)
            {
                player.IsJudge = false;
            }

            _gameSetup.Players[playerNumberToBeJudge].IsJudge = true;
        }

        private Dictionary<int, int> winningConditions = new Dictionary<int, int>
        {
            { 4, 8 },
            { 5, 7 },
            { 6, 6 },
            { 7, 5 }
        };
    }
}
