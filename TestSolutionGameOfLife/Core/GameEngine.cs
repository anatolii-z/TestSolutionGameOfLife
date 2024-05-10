using System;
using System.Windows.Threading;

namespace TestSolutionGameOfLife.Core
{
    public class GameEngine
    {
        private readonly DispatcherTimer _timer;
        public static int GameFieldSize => 50;

        public GameEngine()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _timer.Tick += GameTick;
        }

        private void GameTick(object sender, EventArgs e) => NextGeneration();

        internal void Start()
        {
            _timer.Start();
        }

        private void NextGeneration()
        {

        }
    }
}
