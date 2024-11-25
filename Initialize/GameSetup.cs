using a2a.GameComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a.Initialize
{
    internal class GameSetup
    {
        private ReadDeckTxt _ReadDeckTxt = new ReadDeckTxt();
        private FormatDeckTxt _formatDeckTxt = new FormatDeckTxt();
        private CardDeck _greenDeck = new CardDeck();
        private CardDeck _redDeck = new CardDeck();
        private List<IPlayer> _players = new List<IPlayer>();
        private PlayedRedCards _playedRedCards = new PlayedRedCards();
        private int _numOfPlayers = 0;
        private bool _runGame = false;

        public GameSetup()
        {
            _ReadDeckTxt.ReadCardsTxt();
            _greenDeck.Cards = _formatDeckTxt.FormatDeck(_ReadDeckTxt.DeckOne);
            _redDeck.Cards = _formatDeckTxt.FormatDeck(_ReadDeckTxt.DeckTwo);

            SetPlayerCount();

            _runGame = true;
        }

        private void SetPlayerCount()
        {
            GameUI.AskNumOfUsers();
            if(!int.TryParse(Console.ReadLine(), out _numOfPlayers))
            {
                GameUI.ErrorMsgWrongInputNotRecognized();
            }

            if (_numOfPlayers >= 4)
            {
                _players.Add(new HumanPlayer());

                for (int i = 1; i < _numOfPlayers; i++) 
                {
                    _players.Add(new BotPlayer());
                }

                RandomizeJudge();

            }else
            {
                SetPlayerCount();
            }
        }

        private void RandomizeJudge()
        {
            Random rnd = new Random();
            _players[rnd.Next(0, _players.Count)].IsJudge = true;
        }


        public List<string> GreenDeck { get { return _greenDeck.Cards; } }
        public List<string> RedDeck { get { return _redDeck.Cards; } }
        public List<IPlayer> Players { get { return _players; } }
        public bool RunGame { get { return _runGame; } set { _runGame = value; } }
        public PlayedRedCards PlayedRedCardsDict { get { return _playedRedCards; } }
    }
}
