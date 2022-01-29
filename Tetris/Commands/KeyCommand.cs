using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Commands;

namespace Tetris.Services
{
    internal class KeyCommand : CommandBase
    {
        private Action _execute;
        public KeyCommand(Action execute)
        {
            _execute = execute;
        }
        public override void Execute(object? parameter)
        {
            _execute.Invoke();
        }
    }
}
