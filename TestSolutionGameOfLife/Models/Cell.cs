using TestSolutionGameOfLife.ViewModels;

namespace TestSolutionGameOfLife.Models
{
    internal class Cell : ObservableBase
    {
        private CellStatus _status;

        public int Row { get; set; }
        public int Column { get; set; }
        public int AliveNeighbourCount { get; set; }
        public CellStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public Cell(int row, int column, CellStatus status)
        {
            Row = row;
            Column = column;
            Status = status;
        }
    }
}
