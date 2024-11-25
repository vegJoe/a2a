using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a2a.GameComponents
{
    internal abstract class IPlayer
    {
        private List<string> _cards = new List<string>();
        private int _winningHands = 0;

        private bool _isJudge;

        public List<string> Cards { get { return _cards; } set { _cards = value; } }

        public bool IsJudge { get { return _isJudge; } set { _isJudge = value; } }

        public int WinningHands { get { return _winningHands; } set { _winningHands = value; } }
    }
}
