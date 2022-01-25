using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tetris.ViewModels
{
    internal class MainMenuViewModel
    {
        public ICommand StartCommand { get; }
        public ICommand ContactAuthorCommand { get; }
        public ICommand ExitCommand { get; }

        public MainMenuViewModel()
        {

        }
    }
}
