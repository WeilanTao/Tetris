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
    public class MainMenuViewModel: ViewModelBase
    {
        public ICommand StartCommand { get; }
        public ICommand ContactAuthorCommand { get; }
        public ICommand ExitCommand { get; }

       
        public MainMenuViewModel(NavigationStore navigationStore, Func<ContactAuthorViewModel> createContactAuthorViewModel)
        {
            ContactAuthorCommand = new NavigateCommand(navigationStore, createContactAuthorViewModel);

        }
    }
}
