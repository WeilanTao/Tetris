using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tetris.Commands;
using Tetris.Services;
using Tetris.Stores;
using Tetris.Utils;

namespace Tetris.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand StartGameCommand { get; }
        public ICommand ContactAuthorCommand { get; }
        public ICommand ExitCommand { get; }


        public MainMenuViewModel(NavigationService contactNavigateService, NavigationService startGameNavigationService)
        {
            ContactAuthorCommand = new NavigateCommand(contactNavigateService);
            StartGameCommand = new NavigateCommand(startGameNavigationService);
            ExitCommand = new KeyCommand(CloseWind);
        }
       
        public void CloseWind()
        {
            if (CloseWindow.WinObject != null)
                CloseWindow.CloseParent();
        }
    }

   
}
