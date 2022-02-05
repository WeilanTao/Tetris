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
using Tetris.Utils;

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private const string bgname = "lightblue";
        private const String fgColor = "darkblue";
        private const int bgborder = 0;
        private const int fgorder = 1;
        private int score { get; set; } = 0;
        private int line { get; set; } = 0;
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
        private Tetramino recordTetramino { get; set; }
        //private Tetramino shadowTetramino { get; set; }

        private Game game { get; set; }
        private Suite suite { get; set; }

        private bool newTetrinimo { get; set; } = true;

        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            game = new Game();
            score = game.Score;
            line = game.Line;

            Blocks = new ObservableCollection<Block>();


            InitializeGrid();

            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
            KeyA = new KeyCommand(RotateCW);
            KeyD = new KeyCommand(RotateCCW);
            KeyLeft = new KeyCommand(Left);
            KeyRight = new KeyCommand(Right);
            KeyDown = new KeyCommand(Down);
            KeySpace = new KeyCommand(HardDrop);

            gameRun();
        }

        private async void gameRun()
        {
            await gameLoop();
        }

        private async Task gameLoop()
        {
            while (_gameState == 0)
            {
                if (newTetrinimo)
                {
                    currentTetramino = new Tetramino();
                    recordTetramino = CloneUtil.CloneObject(currentTetramino) as Tetramino;

                    Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgorder);
                    Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgorder);
                    Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgorder);
                    Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgorder);



                    newTetrinimo = false;
                    OnPropertyChanged("Blocks");

                    for (int i = 0; i < 10; i++)
                    {
                        if (Blocks[i + 20].IsOccupied == true)
                            _gameState = 1;
                    }
                }
                else
                {
                    Down();
                    await Task.Delay(1000);
                    //if keyright/keyleft/keyrotation is after the keydown...
                    if (game.IsStackCollision(currentTetramino, Blocks))
                    {
                        //if after the keyright/keyleft/keyrotation, the tetraminio reaches its button position...
                        Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgorder, true);
                        Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgorder, true);
                        Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgorder, true);
                        Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgorder, true);
                    }
                    else
                    {
                        //if after the keyright/keyleft/keyrotation, the tetraminio doesn't reach its button position...then we will go back to the down loop instead of creating a new tetramino
                        newTetrinimo = false;
                    }

                }

            }

        }

        private void Down()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            game.Down(suite);

            UpdateGrid();
            if (!suite.CanLock)
            {
                newTetrinimo = true;
            }
        }

        private void Right()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            game.Right(suite);
            UpdateGrid();
        }
        private void Left()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            game.Left(suite);
            UpdateGrid();
        }

        private void RotateCCW()
        {
            if (currentTetramino.Type != 'O')
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.RotateCCW(suite);
                UpdateGrid();
            }

        }

        private void RotateCW()
        {
            if (currentTetramino.Type != 'O')
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.RotateCW(suite);
                UpdateGrid();
            }
        }

        //private void UpDateShadow()
        //{

        //}

        private void HardDrop()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            Tetramino t = game.HardDrop(suite);
            currentTetramino = CloneUtil.CloneObject(t) as Tetramino;
            UpdateGrid();
            newTetrinimo = true;
        }

        private void UpdateGrid()
        {
            //change perivious position to background block
            Blocks[recordTetramino.Block1.X * 10 + recordTetramino.Block1.Y] = new Block(recordTetramino.Block1.X < 2 ? bgname : fgColor, recordTetramino.Block1.X * 30, recordTetramino.Block1.Y * 30, bgborder);
            Blocks[recordTetramino.Block2.X * 10 + recordTetramino.Block2.Y] = new Block(recordTetramino.Block2.X < 2 ? bgname : fgColor, recordTetramino.Block2.X * 30, recordTetramino.Block2.Y * 30, bgborder);
            Blocks[recordTetramino.Block3.X * 10 + recordTetramino.Block3.Y] = new Block(recordTetramino.Block3.X < 2 ? bgname : fgColor, recordTetramino.Block3.X * 30, recordTetramino.Block3.Y * 30, bgborder);
            Blocks[recordTetramino.Block4.X * 10 + recordTetramino.Block4.Y] = new Block(recordTetramino.Block4.X < 2 ? bgname : fgColor, recordTetramino.Block4.X * 30, recordTetramino.Block4.Y * 30, bgborder);

            //update the current position
            Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgorder, !suite.CanLock ? true : false);
            Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgorder, !suite.CanLock ? true : false);
            Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgorder, !suite.CanLock ? true : false);
            Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgorder, !suite.CanLock ? true : false);

            recordTetramino = CloneUtil.CloneObject(currentTetramino) as Tetramino;
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < 22; i++) //row
                for (int j = 0; j < 10; j++)//col

                    Blocks.Add(new Block(i < 2 ? bgname : fgColor, i * 30, j * 30, bgborder));
        }


        public int Score
        {
            get { return score; }
            set
            {
                score = value;
            }
        }

        public int Line
        {
            get { return line; }
            set
            {
                line = value;
            }
        }

        public Suite Suite
        {
            get { return suite; }

            set { suite = value; }
        }

    }

}
