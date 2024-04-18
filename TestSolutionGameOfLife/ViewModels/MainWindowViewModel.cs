using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestSolutionGameOfLife.ViewModels
{
    internal class MainWindowViewModel
    {
        #region Properties
        public bool CanStart { get; private set; }
        public bool CanPause { get; private set; }
        public bool CanGenerate { get; private set; }
        #endregion

        #region Commands
        public ICommand LoadedCommand => new RelayCommand(LoadedWindow, () => true);
        public ICommand StartCommand => new RelayCommand(StartGame, () => CanStart);
        public ICommand PauseCommand => new RelayCommand(PauseGame, () => CanPause);
        public ICommand RandomGenerationCommand => new RelayCommand(GenerateGameField, () => CanGenerate);
        #endregion

        private void LoadedWindow()
        {
            throw new NotImplementedException();
        }

        private void StartGame()
        {
            throw new NotImplementedException();
        }

        private void PauseGame()
        {
            throw new NotImplementedException();
        }

        private void GenerateGameField()
        {
            throw new NotImplementedException();
        }
    }
}
