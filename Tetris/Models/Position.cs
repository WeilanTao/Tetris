using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class Position
    {
        private int _x { get; set; }
        private int _y { get; set; }

        public Position(int _x, int _y)
        {
            this._x = _x;
            this._y = _y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
