using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    internal class Suite
    {
        private Tetramino _tetramino { get; set; }
        private int _score { get; set; } = 0;
        private int _line { get; set; } = 0;

        public Suite(Tetramino tetraminio, int score, int line)
        {
            _tetramino = tetraminio;
            _score = score;
            _line = line;
        }

        public int Score { get { return _score; } set { _score = value; } }

        public int Line { get { return _line; } set { _line = value; } }
        public Tetramino Tetramino { get { return _tetramino; } set { _tetramino = value; } }
    }
}
