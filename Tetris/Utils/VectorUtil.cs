using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.Utils
{
    /// <summary>
    /// Rotation ruls: Check wall-collison after the rotation;
    /// if there is wall-roattion, move the tetris left/right;
    /// if there is stack collision, the rotation is not allowed, the tetris will be reverted back to its position before the rotation
    /// </summary>
    /// 

    public class VectorUtil
    {
        private static int[,] CWMatrix = { { 0, 1 }, { -1, 0 } };
        private static int[,] CCWMatrix = { { 0, -1 }, { 1, 0 } };

        private const int rowLen = 4;
        private const int colLen = 2;

        //private int[,] offset { get; set; }

        public static void Transfer(Tetramino tetramino, bool isCW)
        {
            //  Convert the Tetramino to Matrix
            int[,] Matrix = TetraminoToMatrix(tetramino);

            int[] offset = new int[2] { tetramino.Block3.X, tetramino.Block3.Y };
            //Do the calculation
            DoTransfer(Matrix, offset, isCW);

            //Convert the Matrix back to Tetramino
            MatrixToTetramino(Matrix, tetramino);
        }
        private static void DoTransfer(int[,] Matrix, int[] Offset, bool isCW)
        {
            //substract the offset
            for (int i = 0; i < rowLen; i++)
            {
                Matrix[i, 0] -= Offset[0];
                Matrix[i, 1] -= Offset[1];
            }

            //do the multiplication
            int[,] resMatrix = new int[4, 2];

            resMatrix[0, 0] = Matrix[0, 0] * (isCW ? CWMatrix[0, 0] : CCWMatrix[0, 0]) + Matrix[0, 1] * (isCW ? CWMatrix[1, 0] : CCWMatrix[1, 0]);
            resMatrix[1, 0] = Matrix[1, 0] * (isCW ? CWMatrix[0, 0] : CCWMatrix[0, 0]) + Matrix[1, 1] * (isCW ? CWMatrix[1, 0] : CCWMatrix[1, 0]);
            resMatrix[2, 0] = Matrix[2, 0] * (isCW ? CWMatrix[0, 0] : CCWMatrix[0, 0]) + Matrix[2, 1] * (isCW ? CWMatrix[1, 0] : CCWMatrix[1, 0]);
            resMatrix[3, 0] = Matrix[3, 0] * (isCW ? CWMatrix[0, 0] : CCWMatrix[0, 0]) + Matrix[3, 1] * (isCW ? CWMatrix[1, 0] : CCWMatrix[1, 0]);

            resMatrix[0, 1] = Matrix[0, 0] * (isCW ? CWMatrix[0, 1] : CCWMatrix[0, 1]) + Matrix[0, 1] * (isCW ? CWMatrix[1, 1] : CCWMatrix[1, 1]);
            resMatrix[1, 1] = Matrix[1, 0] * (isCW ? CWMatrix[0, 1] : CCWMatrix[0, 1]) + Matrix[1, 1] * (isCW ? CWMatrix[1, 1] : CCWMatrix[1, 1]);
            resMatrix[2, 1] = Matrix[2, 0] * (isCW ? CWMatrix[0, 1] : CCWMatrix[0, 1]) + Matrix[2, 1] * (isCW ? CWMatrix[1, 1] : CCWMatrix[1, 1]);
            resMatrix[3, 1] = Matrix[3, 0] * (isCW ? CWMatrix[0, 1] : CCWMatrix[0, 1]) + Matrix[3, 1] * (isCW ? CWMatrix[1, 1] : CCWMatrix[1, 1]);


            //add the offset
            for (int i = 0; i < rowLen; i++)
            {
                resMatrix[i, 0] += Offset[0];
                resMatrix[i, 1] += Offset[1];

                Matrix[i, 0] = resMatrix[i, 0];
                Matrix[i, 1] = resMatrix[i, 1];
            }


        }



        private static int[,] TetraminoToMatrix(Tetramino tetramino)
        {
            int[,] Matrix = new int[4, 2];

            Matrix[0, 0] = tetramino.Block1.X;
            Matrix[0, 1] = tetramino.Block1.Y;

            Matrix[1, 0] = tetramino.Block2.X;
            Matrix[1, 1] = tetramino.Block2.Y;

            Matrix[2, 0] = tetramino.Block3.X;
            Matrix[2, 1] = tetramino.Block3.Y;

            Matrix[3, 0] = tetramino.Block4.X;
            Matrix[3, 1] = tetramino.Block4.Y;

            return Matrix;

        }

        private static void MatrixToTetramino(int[,] Matrix, Tetramino tetramino)
        {
            tetramino.Block1.X = Matrix[0, 0];
            tetramino.Block1.Y = Matrix[0, 1];
            tetramino.Block2.X = Matrix[1, 0];
            tetramino.Block2.Y = Matrix[1, 1];
            tetramino.Block3.X = Matrix[2, 0];
            tetramino.Block3.Y = Matrix[2, 1];
            tetramino.Block4.X = Matrix[3, 0];
            tetramino.Block4.Y = Matrix[3, 1];

        }

    }
}
