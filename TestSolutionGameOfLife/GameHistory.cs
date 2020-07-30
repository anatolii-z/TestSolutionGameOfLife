using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSolutionGameOfLife
{
    public class GameHistory
    {
        public int[,] GameStartField { get; set; }
        public int[,] GameStopField { get; set; }

        public GameHistory(int[,] start, int[,] stop)
        {
            GameStartField = start;
            GameStopField = stop;
        }
    }
}
