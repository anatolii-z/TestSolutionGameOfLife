using System;
using System.Linq;
using System.Windows.Threading;
using TestSolutionGameOfLife.Models;

namespace TestSolutionGameOfLife.Core
{
    public class GameEngine
    {
        private readonly DispatcherTimer _timer;

        private readonly Cell[,] _cellMatrix;
        public static int GameFieldSize => 50;

        public GameEngine()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _timer.Tick += GameTick;
            _cellMatrix = new Cell[GameFieldSize, GameFieldSize];
        }

        private void GameTick(object sender, EventArgs e) => CreateNextGeneration();

        internal void Start()
        {
            _timer.Start();
        }

        internal void Pause()
        {
            _timer.Stop();
        }

        internal void RandomGenerateCellStatus()
        {
            var random = new Random();
            for(var i = 0; i < GameFieldSize; i++)
            {
                for(var j = 0; j < GameFieldSize; j++)
                {
                    _cellMatrix[i, j].Status = (CellStatus)random.Next(2);
                }
            }
        }

        private void CreateNextGeneration()
        {
            CheckNeighbors();
            SetCellStatus();
        }

        internal Cell CreateCell(int row, int column, CellStatus status)
        {
            var cell = new Cell(row, column, status);
            _cellMatrix[row, column] = cell;
            return cell;
        }

        /// <summary>
        /// Changing cell status based on user input
        /// </summary>
        /// <param name="coordinates">String with coordinates</param>
        internal void ChangeCellStatus(string coordinates)
        {
            var t = coordinates.Split(',').Select(x => int.Parse(x)).ToList();
            _cellMatrix[t[0], t[1]].Status = _cellMatrix[t[0], t[1]].Status == CellStatus.Alive ? 
                CellStatus.Dead : CellStatus.Alive;
        }

        /// <summary>
        /// Counting living cells around each cell
        /// </summary>
        private void CheckNeighbors()
        {
            for(var i = 0; i < GameFieldSize; i++)
            {
                for(var j = 0; j < GameFieldSize; j++)
                {
                    _cellMatrix[i, j].AliveNeighbourCount = 0;

                    if(CheckCoordinate(i - 1, j - 1) && _cellMatrix[i - 1, j - 1].Status == CellStatus.Alive)
                        _cellMatrix[i,j].AliveNeighbourCount++;
                    if (CheckCoordinate(i, j - 1) && _cellMatrix[i, j - 1].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i + 1, j - 1) && _cellMatrix[i + 1, j - 1].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i - 1, j) && _cellMatrix[i - 1, j].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i + 1, j) && _cellMatrix[i + 1, j].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i - 1, j + 1) && _cellMatrix[i - 1, j + 1].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i, j + 1) && _cellMatrix[i, j + 1].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                    if (CheckCoordinate(i + 1, j + 1) && _cellMatrix[i + 1, j + 1].Status == CellStatus.Alive)
                        _cellMatrix[i, j].AliveNeighbourCount++;
                }
            }
        }

        /// <summary>
        /// Check coordinates
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <returns></returns>
        private bool CheckCoordinate(int x, int y) => (x >= 0 && x < GameFieldSize) && (y >= 0 && y < GameFieldSize);

        /// <summary>
        /// Set cell status based on living neighbors and its status
        /// </summary>
        private void SetCellStatus()
        {
            for(var i = 0; i < GameFieldSize; i++)
            {
                for(var j = 0; j < GameFieldSize; j++)
                {
                    var cell = _cellMatrix[i, j];

                    if (cell.Status == CellStatus.Alive && cell.AliveNeighbourCount <= 1)
                    {
                        cell.Status = CellStatus.Dead;
                    }
                    else if (cell.Status == CellStatus.Alive && cell.AliveNeighbourCount >= 4)
                    {
                        cell.Status = CellStatus.Dead;
                    }
                    else if (cell.Status == CellStatus.Dead && cell.AliveNeighbourCount == 3)
                    {
                        cell.Status = CellStatus.Alive;
                    }
                }
            }
        }
    }
}
