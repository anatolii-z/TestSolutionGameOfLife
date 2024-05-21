using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using TestSolutionGameOfLife.Models;
using TestSolutionGameOfLife.ViewModels;

namespace TestSolutionGameOfLife
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainViewModel; 
        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainWindowViewModel();
            BuildGameField();
            DataContext = _mainViewModel;
        }

        public void BuildGameField()
        {
            for (int row = 0; row < _mainViewModel.GetGameFieldSize(); row++)
            {
                GameField.RowDefinitions.Add(new RowDefinition());
                for (int column = 0; column < _mainViewModel.GetGameFieldSize(); column++)
                {
                    if(row == 0)
                    {
                        GameField.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    var cell = _mainViewModel.CreateCell(row, column, CellStatus.Dead);
                    var rectangle = CreateRectangle(cell);
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, column);
                    GameField.Children.Add(rectangle);
                }
            }
        }

        private Rectangle CreateRectangle(Cell cell)
        {
            var rectangle = new Rectangle();
            var inputBinding = CreateInputBinding(cell);
            rectangle.InputBindings.Add(inputBinding);
            rectangle.DataContext = cell;
            rectangle.SetBinding(Rectangle.FillProperty, new Binding()
                {
                    Path = new PropertyPath("Status"),
                    Mode = BindingMode.TwoWay,
                    Converter = new ColorConverter()
                });
            return rectangle;
        }

        private InputBinding CreateInputBinding(Cell cell)
        {
            var inputBinding = new InputBinding(_mainViewModel.ChangeCellStatusCommand, new MouseGesture(MouseAction.LeftClick))
            {
                CommandParameter = $"{cell.Row},{cell.Column}"
            };
            return inputBinding;
        }
    }
}
