using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestSolutionGameOfLife.ViewModels
{
    internal class MainWindowViewModel
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand RandomGenerationCommand { get; set; }
        #endregion
    }
}
