using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TestSolutionGameOfLife
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        //размер поля
        private const int gameWidth = 80;
        private const int gameHeight = 80;
        private const double spacing = 1.0;

        private readonly Brush Alive = Brushes.Black;
        private readonly Brush Dead = Brushes.LightGray;
         
        //Массив клеток живых и мертвых для отрисовки Canvas
        private Rectangle[,] matrix = new Rectangle[gameWidth, gameHeight];

        private readonly DispatcherTimer timer = new DispatcherTimer();
        private Random random = new Random();
        private DateTime startTime = new DateTime();
        private DateTime stopTime = new DateTime();
        private int[,] startGameField = new int[gameWidth, gameHeight];
        private int[,] stopGameField = new int[gameWidth, gameHeight];
        private int[,] previosMatrix = new int[gameWidth, gameHeight];
        private bool step = false;

        //проверка входит ли клетка в игровое поле
        private bool validCoordinate(int x, int y)
        {
            return (x >= 0 && x < gameWidth) && (y >= 0 && y < gameHeight);
        }

        //Шаг игры
        private void Next()
        {
            //массив содержащий количество живых соседей клетки
            int[,] neighbors = GetNeighbors();

            EqualGameField(neighbors);
            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    bool isAlive = matrix[x, y].Fill == Alive;
                    int countONPixels = neighbors[x, y];

                    //проверка живых соседей и изменение состояния клетки
                    if (isAlive && countONPixels <= 1)
                    {
                        matrix[x, y].Fill = Dead;
                    }
                    else if (isAlive && countONPixels >= 4)
                    {
                        matrix[x, y].Fill = Dead;
                    }
                    else if (!isAlive && countONPixels == 3)
                    {
                        matrix[x, y].Fill = Alive;
                    }
                }
            }
            if (step)
            {
                step = false;
            }
            else
            {
                previosMatrix = (int[,])neighbors.Clone();
                step = true;
            }
        }

        //Сравнение текущего игрового поля с игровым полем один шаг назад
        private void EqualGameField(int[,] neighbors)
        {
            bool isEqual = true;
            for (int i = 0; i < previosMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < previosMatrix.GetLength(1); j++)
                {
                    if (previosMatrix[i, j] != neighbors[i, j])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            if (isEqual == true)
                StopGame();
        }

        private int[,] GetNeighbors()
        {
            int[,] _matrix = new int[gameWidth, gameHeight];

            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    int count = 0;
                    //проверка на живых соседей
                    for (int i = 0; i < 8; i++)
                    {
                        int xNeighbor;
                        int yNeighbor;
                        switch (i)
                        {
                            default:
                            // Top left
                            case 0:
                                xNeighbor = x - 1;
                                yNeighbor = y - 1;
                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Top middle
                            case 1:
                                xNeighbor = x;
                                yNeighbor = y - 1;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Top right
                            case 2:
                                xNeighbor = x + 1;
                                yNeighbor = y - 1;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Middle left
                            case 3:
                                xNeighbor = x - 1;
                                yNeighbor = y;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Middle right
                            case 4:
                                xNeighbor = x + 1;
                                yNeighbor = y;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Bottom left
                            case 5:
                                xNeighbor = x - 1;
                                yNeighbor = y + 1;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Bottom middle
                            case 6:
                                xNeighbor = x;
                                yNeighbor = y + 1;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                            // Bottom right
                            case 7:
                                xNeighbor = x + 1;
                                yNeighbor = y + 1;

                                if (validCoordinate(xNeighbor, yNeighbor) && matrix[xNeighbor, yNeighbor].Fill == Alive)
                                    count++;
                                break;
                        }
                        _matrix[x, y] = count;
                    }
                }
            }
            return _matrix;
        }

        //построение игрового поля
        private void BuildCanvas(bool isRandom)
        {
            Brush state = Dead;
            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    if (isRandom)
                    {
                        int index = random.Next(0, 2);
                        state = index == 1 ? Alive : Dead;
                    }
                    Rectangle rectangle = new Rectangle
                    {
                        Width = gameCanvas.ActualWidth / gameWidth - spacing,
                        Height = gameCanvas.ActualHeight / gameHeight - spacing,
                        Fill = state
                    };
                    gameCanvas.Children.Add(rectangle);

                    Canvas.SetLeft(rectangle, x * gameCanvas.ActualWidth / gameWidth);
                    Canvas.SetTop(rectangle, y * gameCanvas.ActualHeight / gameHeight);

                    matrix[x, y] = rectangle;

                    rectangle.MouseDown += RectangleMouseEnter;
                    rectangle.MouseEnter += RectangleMouseEnter;
                }
            }
        }

        private void RectangleMouseEnter(object sender, MouseEventArgs e)
        {
            bool leftButton = e.LeftButton == MouseButtonState.Pressed;
            if (!leftButton)
                return;
            Rectangle rectangle = (Rectangle)sender;
            rectangle.Fill = rectangle.Fill == Alive ? Dead : Alive;
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e) => BuildCanvas(false);

        private void GameTick(object sender, EventArgs e) => Next();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += GameTick;
        }

        private void RandomGeneration_Click(object sender, RoutedEventArgs e)
        {
            gameCanvas.Children.Clear();
            BuildCanvas(true);
        }

        //сохранение истории игры в базе данных
        private void SaveGameHistory()
        {
            string jsonGameHistory = JsonConvert.SerializeObject(new GameHistory(startGameField, stopGameField));
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("save_historyGame", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@start", SqlDbType.DateTime).Value = startTime;
                    command.Parameters.Add("@stop", SqlDbType.DateTime).Value = stopTime;
                    command.Parameters.Add("@jsonHistory", SqlDbType.NVarChar).Value = jsonGameHistory;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }
        }

        #region Start Pause Stop
        private void StopGame()
        {
            stopTime = DateTime.Now;
            timer.Stop();
            stopGameField = GetNeighbors();
            SaveGameHistory();
        }

        private void PauseMenuItem_Click(object sender, RoutedEventArgs e) => timer.Stop();

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            startGameField = GetNeighbors();
            startTime = DateTime.Now;
            timer.Start();
        }
        #endregion
    }
}
