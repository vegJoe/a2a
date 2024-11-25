namespace a2a.Initialize
{
    internal class ReadDeckTxt
    {
        private StreamReader _cardTxt;
        private string _directory;
        private string _rootDir;
        private string _cardFolder = "\\cards";
        private string _greenCardPath = "\\greenApples.txt";
        private string _redCardsPath = "\\redApples.txt";
        private string _deckOne;
        private string _deckTwo;

        public void ReadCardsTxt()
        {
            _directory = AppDomain.CurrentDomain.BaseDirectory;
            _rootDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(_directory).FullName).FullName).FullName).FullName;

            _cardTxt = new StreamReader(_rootDir + _cardFolder + _greenCardPath);
            _deckTwo = _cardTxt.ReadToEnd();

            _cardTxt = new StreamReader(_rootDir + _cardFolder + _redCardsPath);
            _deckOne = _cardTxt.ReadToEnd();
        }

        public void ReadCardsTxt(string filePathDeckOne, string filePathDeckTwo)
        {
            _cardTxt = new StreamReader(filePathDeckOne);
            _deckTwo = _cardTxt.ReadToEnd();

            _cardTxt = new StreamReader(filePathDeckTwo);
            _deckTwo = _cardTxt.ReadToEnd();
        }

        public string DeckOne { get { return _deckOne; } }
        public string DeckTwo { get { return _deckTwo; } }
    }
}