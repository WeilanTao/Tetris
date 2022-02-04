using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tetris.Utils;

namespace Tetris.Models
{

    public class Game
    {
        private const int bottomline = 21;
        private const int leftbase = 0;
        private const int rightbase = 9;

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


        public Tetramino HardDrop(Suite suite)
        {
            Tetramino t = new Tetramino();
            t.Block1.X = suite.Tetramino.Block1.X;
            t.Block2.X = suite.Tetramino.Block2.X;
            t.Block3.X = suite.Tetramino.Block3.X;
            t.Block4.X = suite.Tetramino.Block4.X;
            t.Block1.Y = suite.Tetramino.Block1.Y;
            t.Block2.Y = suite.Tetramino.Block2.Y;
            t.Block3.Y = suite.Tetramino.Block3.Y;
            t.Block4.Y = suite.Tetramino.Block4.Y;
            t.Color = suite.Tetramino.Color;
            t.Type = suite.Tetramino.Type;

            while (!IsStackCollision(t, suite.Blocks))
            {
                t.Block1.X += 1;
                t.Block2.X += 1;
                t.Block3.X += 1;
                t.Block4.X += 1;
            }

            return t;
        }

        public void Down(Suite suite)
        {
            if (IsStackCollision(suite.Tetramino, suite.Blocks))
            {
                suite.CanLock = false;
            }
            else
            {
                suite.Tetramino.Block1.X += 1;
                suite.Tetramino.Block2.X += 1;
                suite.Tetramino.Block3.X += 1;
                suite.Tetramino.Block4.X += 1;

                suite.CanLock = !IsStackCollision(suite.Tetramino, suite.Blocks);
            }

        }

        public void Right(Suite suite)
        {
            //we only get the position of the destination; so the occupancy of the currentTetramino won't affect the check of the occupation collision
            suite.Tetramino.Block1.Y += 1;
            suite.Tetramino.Block2.Y += 1;
            suite.Tetramino.Block3.Y += 1;
            suite.Tetramino.Block4.Y += 1;


            if (IsWallCollision(suite.Tetramino, suite.Blocks))
            {
                suite.Tetramino.Block1.Y -= 1;
                suite.Tetramino.Block2.Y -= 1;
                suite.Tetramino.Block3.Y -= 1;
                suite.Tetramino.Block4.Y -= 1;
            }

        }

        public void Left(Suite suite)
        {
            suite.Tetramino.Block1.Y -= 1;
            suite.Tetramino.Block2.Y -= 1;
            suite.Tetramino.Block3.Y -= 1;
            suite.Tetramino.Block4.Y -= 1;

            if (IsWallCollision(suite.Tetramino, suite.Blocks))
            {
                suite.Tetramino.Block1.Y += 1;
                suite.Tetramino.Block2.Y += 1;
                suite.Tetramino.Block3.Y += 1;
                suite.Tetramino.Block4.Y += 1;
            }

        }

        public void RotateCCW(Suite suite) {
            VectorUtil.Transfer(suite.Tetramino,false);

            if(IsStackCollision(suite.Tetramino,suite.Blocks) || IsWallCollision(suite.Tetramino, suite.Blocks))
            {
                VectorUtil.Transfer(suite.Tetramino, true);
            }

        }

        public void RotateCW(Suite suite) {
            VectorUtil.Transfer(suite.Tetramino, true);
            if (IsStackCollision(suite.Tetramino, suite.Blocks) || IsWallCollision(suite.Tetramino, suite.Blocks))
            {
                VectorUtil.Transfer(suite.Tetramino, false);
            }
        }

        private bool IsWallCollision(Tetramino tetramino, ObservableCollection<Block> blockslist)
        {

            if (tetramino.Block1.Y < leftbase ||
                tetramino.Block2.Y < leftbase ||
                tetramino.Block3.Y < leftbase ||
                tetramino.Block4.Y < leftbase ||
                tetramino.Block1.Y > rightbase ||
                tetramino.Block2.Y > rightbase ||
                tetramino.Block3.Y > rightbase ||
                tetramino.Block4.Y > rightbase

                || blockslist[(tetramino.Block1.X) * 10 + tetramino.Block1.Y].IsOccupied
                || blockslist[(tetramino.Block2.X) * 10 + tetramino.Block2.Y].IsOccupied
                || blockslist[(tetramino.Block3.X) * 10 + tetramino.Block3.Y].IsOccupied
                || blockslist[(tetramino.Block4.X) * 10 + tetramino.Block4.Y].IsOccupied
                )
            {
                return true;
            }
            return false;
        }

        public bool IsStackCollision(Tetramino tetramino, ObservableCollection<Block> blockslist)
        {
            if (tetramino.Block1.X >= bottomline ||
                tetramino.Block2.X >= bottomline ||
                tetramino.Block3.X >= bottomline ||
                tetramino.Block4.X >= bottomline ||
                blockslist[(tetramino.Block1.X + 1) * 10 + tetramino.Block1.Y].IsOccupied ||
                blockslist[(tetramino.Block2.X + 1) * 10 + tetramino.Block2.Y].IsOccupied ||
                blockslist[(tetramino.Block3.X + 1) * 10 + tetramino.Block3.Y].IsOccupied ||
                blockslist[(tetramino.Block4.X + 1) * 10 + tetramino.Block4.Y].IsOccupied
                )
            {
                return true;
            }


            return false;
        }

        private int LineRemoveCheck(int score, int line)
        {
            return 0;
        }
    }
}
