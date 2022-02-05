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
        private const int fgborder = 1;
        private int score { get; set; } = 0;
        private int line { get; set; } = 0;
        private int level { get; set; } = 0;

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

        private Tetramino shadowTetramino { get; set; }
        private Tetramino shadowRecord { get; set; }


        private Game game { get; set; }
        private Suite suite { get; set; }

        private bool newTetrinimo { get; set; } = true;

        private bool canMove { get; set; }
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
                    ScoreAndLineUpDate();

                    canMove = true;
                    currentTetramino = new Tetramino();
                    recordTetramino = Clone.CloneObject(currentTetramino) as Tetramino;
                    shadowTetramino = Clone.CloneObject(currentTetramino) as Tetramino;
                    shadowRecord = Clone.CloneObject(currentTetramino) as Tetramino;

                    Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgborder);
                    Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgborder);
                    Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgborder);
                    Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgborder);

                    UpdateShadow();

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
                        Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgborder, true);
                        Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgborder, true);
                        Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgborder, true);
                        Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgborder, true);
                    }
                    else
                    {
                        //if after the keyright/keyleft/keyrotation, the tetraminio doesn't reach its button position...then we will go back to the down loop instead of creating a new tetramino
                        newTetrinimo = false;
                    }

                }

            }

        }

        private void ScoreAndLineUpDate()
        {

        }


        private void Down()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            game.Down(suite);

            recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;

            if (!suite.CanLock)
            {
                newTetrinimo = true;
            }
        }

        private void UpdateShadow()
        {
            shadowTetramino = Clone.CloneObject(currentTetramino) as Tetramino;

            suite = new Suite(shadowTetramino, Score, Line, Blocks);
            shadowTetramino = game.TetraminoMapping(suite);


            Tetramino.StyleTetramino(shadowTetramino, fgColor, shadowTetramino.Color);
            suite = new Suite(shadowTetramino, Score, Line, Blocks);
            shadowRecord = Clone.CloneObject(UpdateGrid(shadowRecord, suite)) as Tetramino;

        }


        private void Right()
        {
            if (canMove)
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.Right(suite);
                recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;
                UpdateShadow();
            }
        }

        private void Left()
        {
            if (canMove)
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.Left(suite);
                recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;
                UpdateShadow();

            }
        }

        private void RotateCCW()
        {
            if (currentTetramino.Type != 'O' && canMove)
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.RotateCCW(suite);
                recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;
                UpdateShadow();

            }
        }

        private void RotateCW()
        {
            if (currentTetramino.Type != 'O' && canMove)
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.RotateCW(suite);
                recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;
                UpdateShadow();

            }
        }

        private void HardDrop()
        {
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            Tetramino t = game.TetraminoMapping(suite);
            currentTetramino = Clone.CloneObject(t) as Tetramino;
            suite = new Suite(currentTetramino, Score, Line, Blocks);

            recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;

            canMove = false;
            newTetrinimo = true;
        }

        private Tetramino UpdateGrid(Tetramino record, Suite s)
        {
            //change perivious position to background block
            Blocks[record.Block1.X * 10 + record.Block1.Y] = new Block(record.Block1.X < 2 ? bgname : fgColor, record.Block1.X * 30, record.Block1.Y * 30, bgborder);
            Blocks[record.Block2.X * 10 + record.Block2.Y] = new Block(record.Block2.X < 2 ? bgname : fgColor, record.Block2.X * 30, record.Block2.Y * 30, bgborder);
            Blocks[record.Block3.X * 10 + record.Block3.Y] = new Block(record.Block3.X < 2 ? bgname : fgColor, record.Block3.X * 30, record.Block3.Y * 30, bgborder);
            Blocks[record.Block4.X * 10 + record.Block4.Y] = new Block(record.Block4.X < 2 ? bgname : fgColor, record.Block4.X * 30, record.Block4.Y * 30, bgborder);

            //update the current position
            Blocks[s.Tetramino.Block1.X * 10 + s.Tetramino.Block1.Y] = new Block(s.Tetramino.Block1.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block1.X * 30, s.Tetramino.Block1.Y * 30, s.Tetramino.Block1.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block2.X * 10 + s.Tetramino.Block2.Y] = new Block(s.Tetramino.Block2.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block2.X * 30, s.Tetramino.Block2.Y * 30, s.Tetramino.Block2.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block3.X * 10 + s.Tetramino.Block3.Y] = new Block(s.Tetramino.Block3.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block3.X * 30, s.Tetramino.Block3.Y * 30, s.Tetramino.Block3.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block4.X * 10 + s.Tetramino.Block4.Y] = new Block(s.Tetramino.Block4.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block4.X * 30, s.Tetramino.Block4.Y * 30, s.Tetramino.Block4.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);


            Tetramino res = Clone.CloneObject(suite.Tetramino) as Tetramino;
            return res;
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

        public int Level { get { return level; } set { this.level = value; } }

        public Suite Suite
        {
            get { return suite; }

            set { suite = value; }
        }

    }

}
