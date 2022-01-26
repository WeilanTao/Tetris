using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tetris.Commands;
using Tetris.Services;
using Tetris.Stores;

namespace Tetris.ViewModels
{
    public class ContactAuthorViewModel:ViewModelBase
    {
        public ICommand MainMenuNavCommand { get; }

        public ContactAuthorViewModel(NavigationService mainMenuService)
        {
            MainMenuNavCommand = new NavigateCommand(mainMenuService);
        }
    }
}
