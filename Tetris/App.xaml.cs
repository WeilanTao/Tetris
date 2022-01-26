using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            _navigationStore.CurrentViewModel = CreateManMenuViweModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);

            
        }

        private ContactAuthorViewModel CreateContactAuthorViewModel()
        {
            return new ContactAuthorViewModel(_navigationStore, CreateManMenuViweModel);

        }

        private MainMenuViewModel CreateManMenuViweModel()
        {
            return new MainMenuViewModel(_navigationStore, CreateContactAuthorViewModel);
        }
    }
}
