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
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore= navigationStore;
            _createViewModel= createViewModel;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel =_createViewModel();
        }
    }
}
