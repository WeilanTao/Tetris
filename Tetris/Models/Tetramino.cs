using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tetris.Utils;

namespace Tetris.Models
{
    [Serializable]
    public class Tetramino : ICloneable
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

        private const int colorBorder = 1; //const?

        private char _type { get; set; }

        public char Type { get { return _type; } set { _type = value; } }


        public static void StyleTetramino(Tetramino t, String BlockColor, String BorderColor)
        {
            t.Color = BlockColor;

            t.Block1.Color = BlockColor;
            t.Block2.Color = BlockColor;
            t.Block3.Color = BlockColor;
            t.Block4.Color = BlockColor;

            t.Block1.BorderColor = BorderColor;
            t.Block2.BorderColor = BorderColor;
            t.Block3.BorderColor = BorderColor;
            t.Block4.BorderColor = BorderColor;


        }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }

        public static Tetramino NextHoldTetraminTransfer(Tetramino tetraino)
        {
            Tetramino t = Tetris.Utils.Clone.CloneObject(tetraino) as Tetramino;

            char Type = t.Type;

            if (Type == 'Z')
            {
                t.Block1.X = 1;
                t.Block1.Y = 1;
                t.Block2.X = 2;
                t.Block2.Y = 1;
                t.Block3.X = 2;
                t.Block3.Y = 2;
                t.Block4.X = 3;
                t.Block4.Y = 2;
            }
            else if (Type == 'S')
            {
                t.Block1.X = 1;
                t.Block1.Y = 2;
                t.Block2.X = 2;
                t.Block2.Y = 2;
                t.Block3.X = 2;
                t.Block3.Y = 1;
                t.Block4.X = 3;
                t.Block4.Y = 1;

            }
            else if (Type == 'T')
            {
                t.Block1.X = 2;
                t.Block1.Y = 1;
                t.Block2.X = 1;
                t.Block2.Y = 2;
                t.Block3.X = 2;
                t.Block3.Y = 2;
                t.Block4.X = 3;
                t.Block4.Y = 2;

            }
            else if (Type == 'L')
            {
                t.Block1.X = 2;
                t.Block1.Y = 0;
                t.Block2.X = 2;
                t.Block2.Y = 1;
                t.Block3.X = 2;
                t.Block3.Y = 2;
                t.Block4.X = 3;
                t.Block4.Y = 2;
            }
            else if (Type == 'I')
            {
                t.Block1.X = 2;
                t.Block1.Y = 0;
                t.Block2.X = 2;
                t.Block2.Y = 1;
                t.Block3.X = 2;
                t.Block3.Y = 2;
                t.Block4.X = 2;
                t.Block4.Y = 3;

            }
            else if (Type == 'J')
            {
                t.Block1.X = 2;
                t.Block1.Y = 0;
                t.Block2.X = 2;
                t.Block2.Y = 1;
                t.Block3.X = 2;
                t.Block3.Y = 2;
                t.Block4.X = 1;
                t.Block4.Y = 2;
            }
            else if (Type == 'O')
            {
                t.Block1.X = 1;
                t.Block1.Y = 1;
                t.Block2.X = 2;
                t.Block2.Y = 1;
                t.Block3.X = 1;
                t.Block3.Y = 2;
                t.Block4.X = 2;
                t.Block4.Y = 2;

            }

            return t;
        }

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
