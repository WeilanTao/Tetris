using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Services;
using Tetris.Stores;
using Tetris.ViewModels;

namespace Tetris.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigateService;

        private Action _execute;

        public NavigateCommand(NavigationService _navigateService, Action execte = null)
        {
            this._navigateService = _navigateService;
            this._execute = execte;
        }

        public override void Execute(object? parameter)
        {
            if (_execute != null)
                _execute.Invoke();

            _navigateService.Navigate();
        }
    }
}
