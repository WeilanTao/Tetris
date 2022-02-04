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
        private int _border { get; set; }
        private String _color { get; set; }
        public Block Block1 { get { return _block1; } set { _block1 = value; } }
        public Block Block2 { get { return _block2; } set { _block2 = value; } }
        public Block Block3 { get { return _block3; } set { _block3 = value; } }
        public Block Block4 { get { return _block4; } set { _block4 = value; } }

       
        public String Color { get { return _color; } set { _color = value; } }

        private const int colorBorder  = 1; //const?

        private char _type { get; set; }

        public char Type { get { return _type; } set { _type = value; } }


        //public Tetramino ShallowCopy()
        //{
        //    return (Tetramino)this.MemberwiseClone();
        //}

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
                _block1 = new Block(Color, 0, 3, colorBorder);//row, col
                _block2 = new Block(Color, 0, 4, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block4 = new Block(Color, 1, 5, colorBorder);
                _type = 'Z';
            }
            else if (value == TetraminoEnum.S)
            {
                _color = "pink";
                _block1 = new Block(Color, 0, 5, colorBorder);
                _block2 = new Block(Color, 0, 4, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block4 = new Block(Color, 1, 3, colorBorder);
                _type = 'S';
            }
            else if (value == TetraminoEnum.T)
            {
                _color = "cyan";
                _block1 = new Block(Color, 1, 3, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block2 = new Block(Color, 1, 5, colorBorder);
                _block4 = new Block(Color, 0, 4, colorBorder);
                _type = 'T';
            }
            else if (value == TetraminoEnum.L)
            {
                _color = "orange";
                _block1 = new Block(Color, 0, 5, colorBorder);
                _block2 = new Block(Color, 1, 3, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block4 = new Block(Color, 1, 5, colorBorder);
                _type = 'L';
            }
            else if (value == TetraminoEnum.I)
            {
                _color = "red";
                _block1 = new Block(Color, 1, 3, colorBorder);
                _block2 = new Block(Color, 1, 4, colorBorder);
                _block3 = new Block(Color, 1, 5, colorBorder);
                _block4 = new Block(Color, 1, 6, colorBorder);
                _type = 'I';
            }
            else if (value == TetraminoEnum.J)
            {
                _color = "blue";
                _block1 = new Block(Color, 0, 3, colorBorder);
                _block2 = new Block(Color, 1, 3, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block4 = new Block(Color, 1, 5, colorBorder);
                _type = 'J';
            }
            else if (value == TetraminoEnum.O)
            {
                _color = "yellow";
                _block1 = new Block(Color, 0, 4, colorBorder);
                _block2 = new Block(Color, 0, 5, colorBorder);
                _block3 = new Block(Color, 1, 4, colorBorder);
                _block4 = new Block(Color, 1, 5, colorBorder);
                _type = 'O';
            }

        }

    }
}
