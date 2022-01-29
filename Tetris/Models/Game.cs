﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class Game
    {
        private int _score = 0;
        private int _line = 0;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int Line
        {
            get { return _line; }
            set { _line = value; }
        }
    }
}
