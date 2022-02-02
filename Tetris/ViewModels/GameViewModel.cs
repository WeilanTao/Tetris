using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using Tetris.Services;
using Tetris.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading;
using System.Collections;

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private int _score { get; set; } = 0;
        private int _line { get; set; } = 0;
        public ObservableCollection<Block> Blocks { get; set; }
        //public Stack<Block> TetriminoSet = new Stack<Block>();

        public ICommand MainMenuCommand { get; private set; }
        public ICommand KeyD { get; private set; }
        public ICommand KeyA { get; private set; }
        public ICommand KeyLeft { get; private set; }
        public ICommand KeyRight { get; private set; }
        public ICommand KeyDown { get; private set; }
        public ICommand KeySpace { get; private set; }

        private int _gameState { get; set; } = 0; //0 for start, 1 for end, 2 for stop

        private Tetramino currentTetramino { get; set; }

        private Game game { get; set; }

        private Suite suite { get; set; }

        int i = 0;
        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            game = new Game();
            _score = game.Score;
            _line = game.Line;

            Blocks = new ObservableCollection<Block>();


            initializeGrid();

            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
            KeyD = new KeyCommand(RotateRight);
            KeyA = new KeyCommand(RotateLeft);
            KeyLeft = new KeyCommand(Left);
            KeyRight = new KeyCommand(Right);
            KeyDown = new KeyCommand(Down);
            KeySpace = new KeyCommand(HardDrop);

            gameRun();
        }

        private async void gameRun()
        {
            await gameLoop();
            //Blocks.RemoveAt(20);
            OnPropertyChanged("Blocks");
        }

        private async Task gameLoop()
        {
            currentTetramino = new Tetramino();
            //Thread.Sleep(1000);
            Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, 1);
            Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, 1);
            Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, 1);
            Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, 1);

            OnPropertyChanged("Blocks");

            while (_gameState == 0)
            {

                //Down();

                await Task.Delay(2);
                OnPropertyChanged("Blocks");

                if (i == 10)
                {
                    _gameState = 1;

                }

                i++;

                #region get a new random tetramino
                #endregion
                #region tetramino keeps falling down 5s until check stack collision is dected

                #endregion

                #region update the ui

                #endregion
            }

        }

        //TODO: need to find a proper way to remove / add tetriminos onto the canvas 
        private void Down()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            game.Down(suite);
            //_vmscore = suite.Score;
            //_vmline = suite.Line;
            MessageBox.Show(suite.CanUpdate.ToString());
            if (suite.CanUpdate)
            {
                updateGrid();
            }
        }

        private void updateGrid()
        {
            //change perivious position to background block
            Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y - 10] = new Block("darkblue", (currentTetramino.Block1.X - 1) * 30, currentTetramino.Block1.Y * 30, 0);
            Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y - 10] = new Block("darkblue", (currentTetramino.Block2.X - 1) * 30, currentTetramino.Block2.Y * 30, 0);
            Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y - 10] = new Block("darkblue", (currentTetramino.Block3.X - 1) * 30, currentTetramino.Block3.Y * 30, 0);
            Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y - 10] = new Block("darkblue", (currentTetramino.Block4.X - 1) * 30, currentTetramino.Block4.Y * 30, 0);

            //update the current position
            Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, 1);
            Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, 1);
            Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, 1);
            Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, 1);

        }


        private void Right()
        {
            throw new NotImplementedException();
        }

        private void Left()
        {
            throw new NotImplementedException();
        }

        private void RotateLeft()
        {
            throw new NotImplementedException();
        }

        private void RotateRight()
        {
            throw new NotImplementedException();
        }
        private void HardDrop()
        {
            throw new NotImplementedException();
        }
        private void initializeGrid()
        {
            for (int i = 0; i < 22; i++) //row
                for (int j = 0; j < 10; j++)//col

                    Blocks.Add(new Block(i < 2 ? "lightblue" : "darkblue", i * 30, j * 30, 0));
        }


        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
            }
        }

        public int Line
        {
            get { return _line; }
            set
            {
                _line = value;
            }
        }

        public Suite Suite
        {
            get { return suite; }

            set { suite = value; }
        }



    }



}
