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
using System.Media;
using System.Windows.Media;
using System.Windows.Controls;

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private const String bgname = "black";
        private const String fgColor = "black";
        private const int bgborder = 0;
        private const int fgborder = 1;
        private int score { get; set; } = 0;
        private int line { get; set; } = 0;
        private int level { get; set; } = 0;

        public ObservableCollection<Block> Blocks { get; set; }

        public ObservableCollection<Block> NextList { get; set; }
        public ObservableCollection<Block> HoldList { get; set; }


        public ICommand MainMenuCommand { get; private set; }
        public ICommand KeyD { get; private set; }
        public ICommand KeyA { get; private set; }
        public ICommand KeyLeft { get; private set; }
        public ICommand KeyRight { get; private set; }
        public ICommand KeyDown { get; private set; }
        public ICommand KeySpace { get; private set; }
        public ICommand KeyW { get; private set; }

        public ICommand NewGameCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ResumeCommand { get; private set; }


        private int _gameState { get; set; } = 0; //0 for start, 1 for end, 2 for stop

        private Tetramino currentTetramino { get; set; }
        private Tetramino recordTetramino { get; set; }

        private Tetramino shadowTetramino { get; set; }
        private Tetramino shadowRecord { get; set; }

        private Tetramino nextTetramino { get; set; }
        private bool initialize { get; set; }

        private Game game { get; set; }
        private Suite suite { get; set; }

        private bool newTetrinimo { get; set; } = true;

        private String gameOver { get; set; }
        private bool canMove { get; set; }

        private Queue<Tetramino> TetraminoQ;

        public CancellationTokenSource _tokenSource { get; set; }
        public CancellationToken cancelToken { get; set; }

        public volatile bool isPaused = false;
        public volatile bool isStopMusic = false;
        private MediaPlayer mediaPlayer { get; set; }
        private void NotifyOfPropertyChange(Func<MediaElement> p)
        {
            throw new NotImplementedException();
        }

        public GameViewModel(NavigationService mainMenuNavigationService, NavigationService newGameViewSerivce)
        {
            game = new Game();
            score = 0;
            line = 0;
            level = 0;
            gameOver = "";

            initialize = true;

            Blocks = new ObservableCollection<Block>();
            NextList = new ObservableCollection<Block>();
            HoldList = new ObservableCollection<Block>();

            InitializeGrid(22, 10, Blocks, true);
            InitializeGrid(4, 4, NextList, false);
            InitializeGrid(4, 4, HoldList, false);

            _tokenSource = new CancellationTokenSource();
            cancelToken = _tokenSource.Token;


            MainMenuCommand = new NavigateCommand(mainMenuNavigationService, NewGameGenerate);
            KeyA = new KeyCommand(RotateCW);
            KeyD = new KeyCommand(RotateCCW);
            KeyLeft = new KeyCommand(Left);
            KeyRight = new KeyCommand(Right);
            KeyDown = new KeyCommand(Down);
            KeySpace = new KeyCommand(HardDrop);
            //KeyW = new KeyCommand(Hold);

            NewGameCommand = new NavigateCommand(newGameViewSerivce, NewGameGenerate);
            ResumeCommand = new KeyCommand(Resume);

            StopCommand = new KeyCommand(Stop);


            TetraminoQ = new Queue<Tetramino>();
            TetraminoQ.Enqueue(new Tetramino());
            TetraminoQ.Enqueue(new Tetramino());
            TetraminoQ.Enqueue(new Tetramino());


            //Thread music = new Thread(PlayMusic);
            //music.Start();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(@"Resource/Tetris.mp3", UriKind.RelativeOrAbsolute));

            mediaPlayer.Play();

            mediaPlayer.MediaEnded += new EventHandler(MediaElement_MediaEnded);

            gameRun();
        }

     
        private void MediaElement_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromMilliseconds(2);
            mediaPlayer.Play();
        }

        private void NewGameGenerate()
        {
            //stop the async task -- gameloop 
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            mediaPlayer.Stop();
            mediaPlayer.Close();
        }

        private void Stop()
        {
            isPaused = true;

        }

        private void Resume()
        {
            isPaused = false;
        }

        private void gameRun()
        {


            gameLoop(cancelToken);

            //_tokenSource.Dispose();
        }

        private async Task gameLoop(CancellationToken token)
        {
            while (_gameState == 0)
            {


                if (newTetrinimo)
                {
                    await LineCancellation();

                    canMove = true;

                    currentTetramino = Clone.CloneObject(TetraminoQ.Dequeue()) as Tetramino;
                    nextTetramino = Clone.CloneObject(TetraminoQ.Peek()) as Tetramino;
                    TetraminoQ.Enqueue(new Tetramino());

                    nextTetramino = Tetramino.NextHoldTetraminTransfer(nextTetramino);

                    drawNextHoldTetramino(NextList, nextTetramino);


                    recordTetramino = Clone.CloneObject(currentTetramino) as Tetramino;
                    shadowTetramino = Clone.CloneObject(currentTetramino) as Tetramino;
                    shadowRecord = Clone.CloneObject(currentTetramino) as Tetramino;

                    if (initialize)
                    {
                        await Task.Delay(50);
                        initialize = false;
                    }


                    Blocks[currentTetramino.Block1.X * 10 + currentTetramino.Block1.Y] = new Block(currentTetramino.Color, currentTetramino.Block1.X * 30, currentTetramino.Block1.Y * 30, fgborder);
                    Blocks[currentTetramino.Block2.X * 10 + currentTetramino.Block2.Y] = new Block(currentTetramino.Color, currentTetramino.Block2.X * 30, currentTetramino.Block2.Y * 30, fgborder);
                    Blocks[currentTetramino.Block3.X * 10 + currentTetramino.Block3.Y] = new Block(currentTetramino.Color, currentTetramino.Block3.X * 30, currentTetramino.Block3.Y * 30, fgborder);
                    Blocks[currentTetramino.Block4.X * 10 + currentTetramino.Block4.Y] = new Block(currentTetramino.Color, currentTetramino.Block4.X * 30, currentTetramino.Block4.Y * 30, fgborder);


                    UpdateShadow();

                    newTetrinimo = false;
                    //OnPropertyChanged("Blocks");

                    for (int i = 0; i < 10; i++)
                    {
                        if (Blocks[i + 20].IsOccupied == true)
                        {
                            _gameState = 1;
                            gameOver = "Game Over!";
                            OnPropertyChanged("GameOver");
                        }
                    }
                }
                else
                {
                    Down();
                    await Task.Delay(300);

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



        private void Hold()
        {
            TetraminoQ.Enqueue(currentTetramino);
            Tetramino holdTetramino = Tetramino.NextHoldTetraminTransfer(currentTetramino);
            drawNextHoldTetramino(HoldList, holdTetramino);

            cleanHolded();
        }

        private void drawNextHoldTetramino(ObservableCollection<Block> o, Tetramino t)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    o[i * 4 + j] = new Block(fgColor, i * 30, j * 30, bgborder);
                }
            }
            o[t.Block1.X * 4 + t.Block1.Y] = new Block(t.Color, t.Block1.X * 30, t.Block1.Y * 30, fgborder);
            o[t.Block2.X * 4 + t.Block2.Y] = new Block(t.Color, t.Block2.X * 30, t.Block2.Y * 30, fgborder);
            o[t.Block3.X * 4 + t.Block3.Y] = new Block(t.Color, t.Block3.X * 30, t.Block3.Y * 30, fgborder);
            o[t.Block4.X * 4 + t.Block4.Y] = new Block(t.Color, t.Block4.X * 30, t.Block4.Y * 30, fgborder);

        }

        private void cleanHolded()
        {

        }
        private async Task LineCancellation()
        {
            await ScoreAndLineUpDate();
            OnPropertyChanged("Score");
            OnPropertyChanged("Line");
            OnPropertyChanged("Level");
            OnPropertyChanged("Blocks");

        }

        private async Task ScoreAndLineUpDate()
        {
            int firstCancel = 0;
            bool isFirstCancel = true;
            int count = 0;

            bool checkRow = true;
            for (int i = 21; i >= 2; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!Blocks[i * 10 + j].IsOccupied)
                    {
                        checkRow = false;
                        goto Outer;
                    }
                }

                if (checkRow)
                {
                    count++;
                    if (isFirstCancel)
                    {
                        firstCancel = i;
                        isFirstCancel = false;
                    }
                }

            Outer:
                checkRow = true;
                ;
            }

            line += count;

            switch (count)
            {
                case 1:
                    score += 20 * (level + 1);
                    break;
                case 2:
                    score += 50 * (level + 1);
                    break;
                case 3:
                    score += 120 * (level + 1);
                    break;
                case 4:
                    score += 150 * (level + 1);
                    break;
                default:
                    break;
            }

            level = line / 10;

            await clearLine(firstCancel, count);
        }

        private async Task clearLine(int firstCancel, int count)
        {

            int lastCancel = firstCancel - count;
            for (int i = firstCancel; i > lastCancel; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    Blocks[i * 10 + j] = new Block(fgColor, i * 30, j * 30, bgborder);

                }
            }

            for (int i = lastCancel; i > 2; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Blocks[i * 10 + j].IsOccupied)
                    {
                        Block t = Blocks[i * 10 + j];
                        Blocks[(i + count) * 10 + j] = new Block(t.Color, (i + count) * 30, j * 30, fgborder, true);
                        Blocks[i * 10 + j] = new Block(fgColor, i * 30, j * 30, bgborder, false);

                    }

                }
            }

        }

        private void Down()
        {
            if (isPaused)
            {
                return;
            }
            suite = new Suite(currentTetramino, Score, Line, Blocks);

            game.Down(suite);


            recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;

            if (!suite.CanLock)
            {
                newTetrinimo = true;
            }
        }

        private void HardDrop()
        {
            if (isPaused)
            {
                return;
            }
            suite = new Suite(currentTetramino, Score, Line, Blocks);
            Tetramino t = game.TetraminoMapping(suite);
            currentTetramino = Clone.CloneObject(t) as Tetramino;
            suite = new Suite(currentTetramino, Score, Line, Blocks);

            recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;

            canMove = false;
            newTetrinimo = true;
        }

        private void UpdateShadow()
        {
            shadowTetramino = Clone.CloneObject(currentTetramino) as Tetramino;
            shadowTetramino.Block1.IsOccupied = false;
            shadowTetramino.Block2.IsOccupied = false;
            shadowTetramino.Block3.IsOccupied = false;
            shadowTetramino.Block4.IsOccupied = false;

            suite = new Suite(shadowTetramino, Score, Line, Blocks);
            shadowTetramino = game.TetraminoMapping(suite);


            Tetramino.StyleTetramino(shadowTetramino, fgColor, shadowTetramino.Color);
            suite = new Suite(shadowTetramino, Score, Line, Blocks);
            shadowRecord = Clone.CloneObject(UpdateGrid(shadowRecord, suite,true)) as Tetramino;

        }

        private void Right()
        {
            if (isPaused)
            {
                return;
            }
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
            if (isPaused)
            {
                return;
            }
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
            if (isPaused)
            {
                return;
            }
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
            if (isPaused)
            {
                return;
            }
            if (currentTetramino.Type != 'O' && canMove)
            {
                suite = new Suite(currentTetramino, Score, Line, Blocks);
                game.RotateCW(suite);
                recordTetramino = Clone.CloneObject(UpdateGrid(recordTetramino, suite)) as Tetramino;
                UpdateShadow();

            }
        }

        private Tetramino UpdateGrid(Tetramino record, Suite s , bool isRecordShadow=false)
        {
            //change perivious position to background block
            if(!(isRecordShadow && Blocks[record.Block1.X * 10 + record.Block1.Y].Color != fgColor))
            {
                Blocks[record.Block1.X * 10 + record.Block1.Y] = new Block(record.Block1.X < 2 ? bgname : fgColor, record.Block1.X * 30, record.Block1.Y * 30, bgborder, false);
            }
            if (!(isRecordShadow && Blocks[record.Block2.X * 10 + record.Block2.Y].Color != fgColor))
            {
                Blocks[record.Block2.X * 10 + record.Block2.Y] = new Block(record.Block2.X < 2 ? bgname : fgColor, record.Block2.X * 30, record.Block2.Y * 30, bgborder, false);
            }
            if (!(isRecordShadow && Blocks[record.Block3.X * 10 + record.Block3.Y].Color != fgColor))
            {
                Blocks[record.Block3.X * 10 + record.Block3.Y] = new Block(record.Block3.X < 2 ? bgname : fgColor, record.Block3.X * 30, record.Block3.Y * 30, bgborder, false);
            }
            if (!(isRecordShadow && Blocks[record.Block4.X * 10 + record.Block4.Y].Color != fgColor))
            {
                Blocks[record.Block4.X * 10 + record.Block4.Y] = new Block(record.Block4.X < 2 ? bgname : fgColor, record.Block4.X * 30, record.Block4.Y * 30, bgborder, false);

            }

            //update the current position
            Blocks[s.Tetramino.Block1.X * 10 + s.Tetramino.Block1.Y] = new Block(s.Tetramino.Block1.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block1.X * 30, s.Tetramino.Block1.Y * 30, s.Tetramino.Block1.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block2.X * 10 + s.Tetramino.Block2.Y] = new Block(s.Tetramino.Block2.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block2.X * 30, s.Tetramino.Block2.Y * 30, s.Tetramino.Block2.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block3.X * 10 + s.Tetramino.Block3.Y] = new Block(s.Tetramino.Block3.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block3.X * 30, s.Tetramino.Block3.Y * 30, s.Tetramino.Block3.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);
            Blocks[s.Tetramino.Block4.X * 10 + s.Tetramino.Block4.Y] = new Block(s.Tetramino.Block4.X < 2 ? bgname : s.Tetramino.Color, s.Tetramino.Block4.X * 30, s.Tetramino.Block4.Y * 30, s.Tetramino.Block4.X < 2 ? bgborder : fgborder, !s.CanLock ? true : false, s.Tetramino.Block1.BorderColor);


            Tetramino res = Clone.CloneObject(suite.Tetramino) as Tetramino;
            return res;
        }

        private void InitializeGrid(int row, int col, ObservableCollection<Block> o, bool isGridField)
        {
            for (int i = 0; i < row; i++) //row 22
                for (int j = 0; j < col; j++)//col 10
                {
                    o.Add(new Block((i < 2 && isGridField) ? bgname : fgColor, i * 30, j * 30, bgborder));

                }
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

        public String GameOver { get { return gameOver; } set { gameOver = value; } }

    }

}