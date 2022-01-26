using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Stores;

namespace Tetris.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentModelChanged;

        }

        private void OnCurrentModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
