using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Stores;
using Tetris.ViewModels;

namespace Tetris.Commands
{
    public class NavigateCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public NavigateCommand(NavigationStore navigationStore)
        {
            _navigationStore= navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new ContactAuthorViewModel();
        }
    }
}
