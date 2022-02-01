using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    public class Game
    {
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

        public void HardDrop() { 
        
        }
         
        public  void Down( Suite suite) {
            
            suite.Tetramino.Block1.X+=30;
            suite.Tetramino.Block2.X+=30;
            suite.Tetramino.Block3.X+=30;
            suite.Tetramino.Block4.X+=30;
            suite.Score++;

            #region check collision
            #endregion

            //return suite;

        }

        public void Right() { }

        public void Left() { } 

        public void RotateLeft() { }

        public void RotateRight() { }   

        public void CheckWallCollision() { 
        }

        public void CheckStackCollision() { }

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
