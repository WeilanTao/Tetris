using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris.Models
{

    public class Game
    {
        private const int baseline = 21;

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

        public void HardDrop()
        {

        }

        public void Down(Suite suite)
        {
            if (IsStackCollision(suite.Tetramino, suite.Blocks))
            {
                suite.CanUpdate = false;
            }
            else
            {
                suite.Tetramino.Block1.X += 1;
                suite.Tetramino.Block2.X += 1;
                suite.Tetramino.Block3.X += 1;
                suite.Tetramino.Block4.X += 1;

                suite.CanUpdate = !IsStackCollision(suite.Tetramino, suite.Blocks);
            }

            if (!suite.CanUpdate)
            {
                suite.Tetramino.Block1.IsOccupied = true;
                suite.Tetramino.Block2.IsOccupied = true;
                suite.Tetramino.Block3.IsOccupied = true;
                suite.Tetramino.Block4.IsOccupied = true;

                #region update score and line
                //suite.Score++;
                //suite.Line++;
                #endregion
            }

        }

        public void Right(Suite suite) { 
        
        }

        public void Left() { }

        public void RotateLeft() { }

        public void RotateRight() { }

        private bool IsWallCollision()
        {

            return true;
        }

        private bool IsStackCollision(Tetramino tetramino, ObservableCollection<Block> blockslist)
        {
            if (tetramino.Block1.X >= baseline ||
                tetramino.Block2.X >= baseline ||
                tetramino.Block3.X >= baseline ||
                tetramino.Block4.X >= baseline ||
                blockslist[(tetramino.Block1.X + 1) * 10 + tetramino.Block1.Y].IsOccupied ||
                blockslist[(tetramino.Block2.X + 1) * 10 + tetramino.Block2.Y].IsOccupied ||
                blockslist[(tetramino.Block3.X + 1) * 10 + tetramino.Block3.Y].IsOccupied ||
                blockslist[(tetramino.Block4.X + 1) * 10 + tetramino.Block4.Y].IsOccupied
                )
            {
                // MessageBox.Show("Block1.X is " + tetramino.Block1.X + " tetramino.Block2.X is " + tetramino.Block2.X +
                //" tetramino.Block3.X is " + tetramino.Block3.X + " tetramino.Block4.X is" + tetramino.Block4.X +
                //" tetramino.Block1.IsOccupied is" + blockslist[tetramino.Block1.X + 1].IsOccupied +
                //" tetramino.Block2.IsOccupied is" + blockslist[tetramino.Block2.X + 1].IsOccupied +
                //" tetramino.Block3.IsOccupied is" + blockslist[tetramino.Block3.X + 1].IsOccupied +
                //" tetramino.Block4.IsOccupied is" + blockslist[tetramino.Block4.X + 1].IsOccupied);

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
