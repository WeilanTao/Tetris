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

        public NavigateCommand(NavigationService _navigateService)
        {
            this._navigateService = _navigateService;
        }

        public override void Execute(object? parameter)
        {
            _navigateService.Navigate();
        }
    }
}
