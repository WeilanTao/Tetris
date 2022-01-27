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
            Blocks.Add(new Block { Color = "red", X = "0", Y = "0" });
            Blocks.Add(new Block { Color = "green", X = "30", Y = "30" });
            Blocks.Add(new Block { Color = "yellow", X = "60", Y = "60" });



            MainMenuCommand = new NavigateCommand(mainMenuNavigationService);
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
        public String X { get; set; }
        public String Y { get; set; }
    }


}
