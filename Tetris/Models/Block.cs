using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class Block
    {

        private String _color { get; set; }

        private int _border { get; set; }
        private int _x { get; set; }
        private int _y { get; set; }

        public Block(String Color, int x, int y, int Border)
        {
            _color = Color;
            _x = x;
            _y = y;
            _border = Border;

        }

        public String Color { get { return _color; } set { _color = value; } }
        public int Border { get { return _border; } set { _border = value; } }
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

    }

}
