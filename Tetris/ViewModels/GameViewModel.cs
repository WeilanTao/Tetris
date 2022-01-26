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

namespace Tetris.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private int _vmscore;
        private int _vmline;

        public ICommand MainMenuCommand { get; }

        public GameViewModel(NavigationService mainMenuNavigationService)
        {
            Game game = new Game();
            _vmscore = game.Score;
            _vmline = game.Line;
            MainMenuCommand =new NavigateCommand(mainMenuNavigationService);
        }


        public String Score
        {
            get { return _vmscore.ToString();}
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
