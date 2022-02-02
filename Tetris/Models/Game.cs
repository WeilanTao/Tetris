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
        private const int baseline = 22;

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

        internal Tetramino GenerateRandom()
        {
            return null;
        }

        public void HardDrop()
        {

        }

        public void Down(Suite suite)
        {

            suite.Tetramino.Block1.X += 1;
            suite.Tetramino.Block2.X += 1;
            suite.Tetramino.Block3.X += 1;
            suite.Tetramino.Block4.X += 1;

            suite.CanUpdate = IsStackCollision(suite.Tetramino, suite.Blocks);

            if (!suite.CanUpdate)
            {
                suite.Tetramino.Block1.X -= 1;
                suite.Tetramino.Block2.X -= 1;
                suite.Tetramino.Block3.X -= 1;
                suite.Tetramino.Block4.X -= 1;
                #region update score and line
                //suite.Score++;
                //suite.Line++;
                #endregion
            }

        }

        public void Right() { }

        public void Left() { }

        public void RotateLeft() { }

        public void RotateRight() { }

        public bool IsWallCollision()
        {

            return true;
        }

        public bool IsStackCollision(Tetramino tetramino, ObservableCollection<Block> blockslist)
        {
            if (tetramino.Block1.X < baseline &&
                tetramino.Block2.X < baseline &&
                tetramino.Block3.X < baseline &&
                tetramino.Block4.X < baseline&&
                !blockslist[tetramino.Block1.X + 1].IsOccupied &&
                !blockslist[tetramino.Block2.X + 1].IsOccupied &&
                !blockslist[tetramino.Block3.X + 1].IsOccupied &&
                !blockslist[tetramino.Block4.X + 1].IsOccupied
                )
            {
                return true;
            }

            MessageBox.Show("Block1.X is "+ tetramino.Block1.X + " tetramino.Block2.X is "+ tetramino.Block3.X+
                " tetramino.Block3.X is "+ tetramino.Block1.X + " tetramino.Block4.X is" + tetramino.Block1.X +
                " tetramino.Block1.IsOccupied is" + blockslist[tetramino.Block1.X + 1].IsOccupied+
                " tetramino.Block2.IsOccupied is" + blockslist[tetramino.Block2.X + 1].IsOccupied +
                " tetramino.Block3.IsOccupied is" + blockslist[tetramino.Block3.X + 1].IsOccupied +
                " tetramino.Block4.IsOccupied is" + blockslist[tetramino.Block4.X + 1].IsOccupied );
            return false;
        }

        private int ScoreUpdate()
        {
            return 0;
        }

        private int LineUpdate()
        {
            return 0;
        }
    }
}
