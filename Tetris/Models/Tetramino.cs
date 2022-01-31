using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris.Models
{

    public class Tetramino
    {
        private enum TetraminoEnum
        {
            Z,
            S,
            T,
            L,
            I,
            J,
            O
        }

        private Block _block1 { get; set; }
        private Block _block2 { get; set; }
        private Block _block3 { get; set; }
        private Block _block4 { get; set; }

        public Block Block1 { get { return _block1; } set { _block1 = value; } }
        public Block Block2 { get { return _block2; } set { _block2 = value; } }
        public Block Block3 { get { return _block3; } set { _block3 = value; } }
        public Block Block4 { get { return _block4; } set { _block4 = value; } }

        private String _color { get; set; }
        public String Color { get { return _color; } set { _color = value; } }

        private static int border { get; } = 1; //const?
        public Tetramino()
        {
            Random random = new Random();
            Type type = typeof(TetraminoEnum);

            Array values = type.GetEnumValues();

            int index = random.Next(values.Length);
            TetraminoEnum value = (TetraminoEnum)values.GetValue(index);


            if (value == TetraminoEnum.Z)
            {
                _color = "green";
                _block1 = new Block(Color, 0 * 30, 3 * 30, border);//row, col
                _block2 = new Block(Color, 0 * 30, 4 * 30, border);
                _block3 = new Block(Color, 1 * 30, 4 * 30, border);
                _block4 = new Block(Color, 1 * 30, 5 * 30, border);
            }
            else if (value == TetraminoEnum.S)
            {
                _color = "pink";
                _block1 = new Block(Color, 0 * 30, 5 * 30, border);
                _block2 = new Block(Color, 0 * 30, 4 * 30, border);
                _block3 = new Block(Color, 1 * 30, 4 * 30, border);
                _block4 = new Block(Color, 1 * 30, 3 * 30, border);

            }
            else if (value == TetraminoEnum.T)
            {
                _color = "cyan";
                _block1 = new Block(Color, 1 * 30, 3 * 30, border);
                _block2 = new Block(Color, 1 * 30, 4 * 30, border);
                _block3 = new Block(Color, 1 * 30, 5 * 30, border);
                _block4 = new Block(Color, 0 * 30, 4 * 30, border);
            }
            else if (value == TetraminoEnum.L)
            {
                _color = "orange";
                _block1 = new Block(Color, 0 * 30, 5 * 30, border);
                _block2 = new Block(Color, 1 * 30, 3 * 30, border);
                _block3 = new Block(Color, 1 * 30, 4 * 30, border);
                _block4 = new Block(Color, 1 * 30, 5 * 30, border);
            }
            else if (value == TetraminoEnum.I)
            {
                _color = "red";
                _block1 = new Block(Color, 1 * 30, 3 * 30, border);
                _block2 = new Block(Color, 1 * 30, 4 * 30, border);
                _block3 = new Block(Color, 1 * 30, 5 * 30, border);
                _block4 = new Block(Color, 1 * 30, 6 * 30, border);
            }
            else if (value == TetraminoEnum.J)
            {
                _color = "blue";
                _block1 = new Block(Color, 0 * 30, 3 * 30, border);
                _block2 = new Block(Color, 1 * 30, 3 * 30, border);
                _block3 = new Block(Color, 1 * 30, 4 * 30, border);
                _block4 = new Block(Color, 1 * 30, 5 * 30, border);
            }
            else if (value == TetraminoEnum.O)
            {
                _color = "yellow";
                _block1 = new Block(Color, 0 * 30, 4 * 30, border);
                _block2 = new Block(Color, 0 * 30, 5 * 30, border);
                _block3 = new Block(Color, 1 * 30, 4 * 30, border);
                _block4 = new Block(Color, 1 * 30, 5 * 30, border);
            }

        }

    }
}
