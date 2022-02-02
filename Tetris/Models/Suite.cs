using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class Suite
    {
        private Tetramino _tetramino { get; set; }
        private int _score { get; set; } = 0;
        private int _line { get; set; } = 0;

        private bool _canUpdate { get; set; } = true;

        private ObservableCollection<Block> _blocks { get; set; }
        public Suite(Tetramino tetraminio, int score, int line, ObservableCollection<Block> blocks)
        {
            _tetramino = tetraminio;
            _score = score;
            _line = line;
            _blocks = blocks;
        }

        public bool CanUpdate { get { return _canUpdate; } set { _canUpdate = value; } }
        public int Score { get { return _score; } set { _score = value; } }

        public int Line { get { return _line; } set { _line = value; } }
        public Tetramino Tetramino { get { return _tetramino; } set { _tetramino = value; } }

        public ObservableCollection<Block> Blocks { get { return _blocks; } set { _blocks = value; } }
    }
}
