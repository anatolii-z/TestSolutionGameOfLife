using System;
using System.Linq;
using System.Windows.Input;
using TestSolutionGameOfLife.Models;

namespace TestSolutionGameOfLife.ViewModels
{
    internal class MainWindowViewModel : ObservableBase
    {
        #region Private field
        private bool _canStart;
        private bool _canPause;
        private bool _canRandomGeneration;

        private readonly Cell[,] _cellMatrix;
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

        public int GameFieldSize => 50;
        #endregion

        public MainWindowViewModel()
        {
            _cellMatrix = new Cell[GameFieldSize, GameFieldSize];
        }

        #region Commands
        public ICommand LoadedCommand => new RelayCommand(_ => LoadedWindow(), () => true);
        public ICommand StartCommand => new RelayCommand(_ => StartGame(), () => CanStart);
        public ICommand PauseCommand => new RelayCommand(_ => PauseGame(), () => CanPause);
        public ICommand RandomGenerationCommand => new RelayCommand(_ => GenerateGameField(), () => CanGenerate);
        public ICommand ChangeCellStatusCommand => new RelayCommand((x) => ChangeCellStatus(x), () => true);
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

        private void ChangeCellStatus(string coordinate)
        {
            var t = coordinate.Split(',').Select(x=> int.Parse(x)).ToList();
            _cellMatrix[t[0], t[1]].Status = _cellMatrix[t[0], t[1]].Status == CellStatus.Alive ? CellStatus.Dead : CellStatus.Alive;
        }

        public Cell CreateCell(int row, int column, CellStatus status)
        {
            var cell = new Cell(row, column, status);
            _cellMatrix[row, column] = cell;
            return cell;
        }
    }
}
