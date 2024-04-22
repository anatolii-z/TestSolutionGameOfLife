using System;
using System.Windows.Input;

namespace TestSolutionGameOfLife.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Private field
        private bool _canStart;
        private bool _canPause;
        private bool _canRandomGeneration;
        #endregion

        #region Properties
        public bool CanStart
        {
            get { return _canStart; } 
            private set
            {
                _canStart = value;
                OnPropertyChanged(nameof(CanStart));
            }
        }
        public bool CanPause
        {
            get { return _canPause; }
            private set 
            {
                _canPause = value;
                OnPropertyChanged(nameof(CanPause));
            }
        }
        public bool CanGenerate
        {
            get { return _canRandomGeneration; }
            private set
            {
                _canRandomGeneration = value;
                OnPropertyChanged(nameof(CanGenerate));
            } 
        }
        #endregion

        #region Commands
        public ICommand LoadedCommand => new RelayCommand(LoadedWindow, () => true);
        public ICommand StartCommand => new RelayCommand(StartGame, () => CanStart);
        public ICommand PauseCommand => new RelayCommand(PauseGame, () => CanPause);
        public ICommand RandomGenerationCommand => new RelayCommand(GenerateGameField, () => CanGenerate);
        #endregion

        private void LoadedWindow()
        {
            CanStart = true;
            CanPause = false;
            CanGenerate = true;
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
