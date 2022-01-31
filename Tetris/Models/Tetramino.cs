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

        //private Block _block1 { get; set; }
        //private Block _block2 { get; set; }
        //private Block _block3 { get; set; }
        //private Block _block4 { get; set; }

        private static int border {get;} = 1; //const?
        public Tetramino()
        {
            Random random = new Random();
            Type type = typeof(TetraminoEnum);

            Array values = type.GetEnumValues();

            int index = random.Next(values.Length);
            TetraminoEnum value = (TetraminoEnum)values.GetValue(index);

            if(value == TetraminoEnum.Z)
            {
       
            }
            else if(value == TetraminoEnum.S)
            {

            }else if(value== TetraminoEnum.T)
            {

            }else if(value == TetraminoEnum.L)
            {

            }else if(value == TetraminoEnum.I)
            {

            }
            else if (value == TetraminoEnum.J)
            {

            }
            else if (value == TetraminoEnum.O)
            {

            }

        }

    }
}
