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

        int i = 0;
        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            Game game = new Game();
            _vmscore = game.Score;
            _vmline = game.Line;

            Blocks = new ObservableCollection<Block>();


            initializeGrid();

            //removeTest();
            //Add();


            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
            KeyD = new KeyCommand(RotateRight);
            KeyA = new KeyCommand(RotateLeft);
            KeyLeft = new KeyCommand(Left);
            KeyRight = new KeyCommand(Right);
            KeyDown = new KeyCommand(Down);
            KeySpace = new KeyCommand(HardDrop);

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
            for (int i = 0; i < 10; i++) //row
                for (int j = 0; j < 20; j++)//col
                    Blocks.Add(new Block { Color = "darkblue", X = i * 30, Y = j * 30, Border = 0 });

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

    public class Block
    {

        public String Color { get; set; }

        public int Border { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }


}
