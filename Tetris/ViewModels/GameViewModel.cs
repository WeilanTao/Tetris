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

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private int _vmscore;
        private int _vmline;

        public ICommand MainMenuCommand { get; }
        public ObservableCollection<Block> Blocks { get; set; }

        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            Game game = new Game();
            _vmscore = game.Score;
            _vmline = game.Line;

            Blocks = new ObservableCollection<Block>();


            initializeGrid();

            removeTest();
            Add();
            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
        }


        private void initializeGrid()
        {
            for (int i = 0; i < 10; i++) //row
                for (int j = 0; j < 20; j++)//col
                    Blocks.Add(new Block { Color = "darkblue", X = i * 30, Y = j * 30 , Border = 0 });

        }

        private void Add()
        {
            Blocks.Add(new Block{ Color = "wheat", X =  30, Y =  30, Border = 1 } );
            Blocks.Add(new Block { Color = "wheat", X = 30, Y = 60, Border = 1 });
            Blocks.Add(new Block { Color = "wheat", X = 30, Y = 90, Border = 1 });
            OnPropertyChanged("Blocks");
        }
        private void removeTest()
        {
            Blocks.RemoveAt(19);
            OnPropertyChanged("Blocks");
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
