using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tetris.Commands;
using Tetris.Stores;

namespace Tetris.ViewModels
{
    public class ContactAuthorViewModel:ViewModelBase
    {
        public ICommand MainMenuNavCommand { get; }

        public ContactAuthorViewModel(NavigationStore navigationStore, Func<MainMenuViewModel> createMainMenuViewModel)
        {
            MainMenuNavCommand = new NavigateCommand(navigationStore, createMainMenuViewModel);
        }
    }
}
