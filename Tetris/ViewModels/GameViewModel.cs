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

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private int _vmscore;
        private int _vmline;
        public ObservableCollection<Block> Blocks { get; set; }

        public ICommand MainMenuCommand { get; private set; }
        public ICommand KeyD { get; private set; }
        public ICommand KeyA { get; private set; }
        public ICommand KeyLeft { get; private set; }
        public ICommand KeyRight { get; private set; }
        public ICommand KeyDown { get; private set; }
        public ICommand KeySpace { get; private set; }

        private int _gameState { get; set; } = 0; //0 for start, 1 for end, 2 for stop

        private Tetramino currentTetramino { get; set; }


        int i = 0;
        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            Game game = new Game();
            _vmscore = game.Score;
            _vmline = game.Line;

            Blocks = new ObservableCollection<Block>();


            initializeGrid();

            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
            KeyD = new KeyCommand(RotateRight);
            KeyA = new KeyCommand(RotateLeft);
            KeyLeft = new KeyCommand(Left);
            KeyRight = new KeyCommand(Right);
            KeyDown = new KeyCommand(Down);
            KeySpace = new KeyCommand(HardDrop);

            Thread thread = new Thread(gameLoop);
            //thread.IsBackground = true;
            thread.Start();

            //gameLoop();
        }


        private void gameLoop()
        {
            //Thread.Sleep(1000);
            currentTetramino = new Tetramino();
            Blocks.Add(currentTetramino.Block1);
            Blocks.Add(currentTetramino.Block2);
            Blocks.Add(currentTetramino.Block3);
            Blocks.Add(currentTetramino.Block4);

            OnPropertyChanged("Blocks");

            while (_gameState == 0)
            {
                #region get a new random tetramino

                #endregion

                #region tetramino keeps falling down 5s until check stack collision is dected

                #endregion

                #region update the ui

                #endregion
            }

        }


        private void HardDrop()
        {
            throw new NotImplementedException();
        }

        private void Down()
        {
            throw new NotImplementedException();
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

        private void initializeGrid()
        {
            for (int i = 0; i < 22; i++) //row
                for (int j = 0; j < 10; j++)//col

                    Blocks.Add(new Block(i < 2 ? "lightblue" : "darkblue", i * 30, j * 30, 0));
        }


        public String Score
        {
            get { return _vmscore.ToString(); }
            //set { _vmscore = value;
            //}
        }

        public String Line
        {
            get { return _vmline.ToString(); }
            //set
            //{
            //    _vmline = value;
            //}
        }


    }



}
