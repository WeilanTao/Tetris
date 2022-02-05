using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    [Serializable]
    public class Block
    {

        private String _color { get; set; }

        private int _border { get; set; }
        private int _x { get; set; }
        private int _y { get; set; }

        private bool _isOccupied { get; set; }

        public Block()
        {

        }

        public Block(String Color, int x, int y, int Border, bool isOccupied = false)
        {
            _color = Color;
            _x = x;
            _y = y;
            _border = Border;
            _isOccupied = isOccupied;
        }

        public String Color { get { return _color; } set { _color = value; } }
        public int Border { get { return _border; } set { _border = value; } }
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }
        public bool IsOccupied { get { return _isOccupied; } set { _isOccupied = value; } }


    }

}
