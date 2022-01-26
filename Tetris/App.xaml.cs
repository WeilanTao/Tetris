using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tetris.Services;
using Tetris.Stores;
using Tetris.ViewModels;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;


        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateMainMenuViweModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);

            
        }

        private ContactAuthorViewModel CreateContactAuthorViewModel()
        {
            return new ContactAuthorViewModel(new NavigationService(_navigationStore, CreateMainMenuViweModel));

        }

        private MainMenuViewModel CreateMainMenuViweModel()
        {
            return new MainMenuViewModel(new NavigationService(_navigationStore, CreateContactAuthorViewModel));
        }
    }
}
