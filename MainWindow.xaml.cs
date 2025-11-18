using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UcanObjeOyunu.Models;

namespace UcanObjeOyunu
{
    public partial class MainWindow : Window
    {
        private GameEngine? _gameEngine;
        private bool _isGameRunning;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameEngine = new GameEngine(GameCanvas.ActualWidth, GameCanvas.ActualHeight);
            _gameEngine.ScoreUpdated += OnScoreUpdated;
            _gameEngine.GameOver += OnGameOver;
            _gameEngine.CollisionDetected += OnCollisionDetected;
            _gameEngine.ObstacleCreated += OnObstacleCreated;
            _gameEngine.ObstacleRemoved += OnObstacleRemoved;

            // Set initial UI state
            GameOverText.Visibility = Visibility.Collapsed;
            StartGameText.Visibility = Visibility.Visible;
        }

        private void StartGame()
        {
            if (_isGameRunning) return;

            // UI thread'inde işlem yap
            Dispatcher.Invoke(() =>
            {
                // Clear the canvas
                GameCanvas.Children.Clear();
                GameOverText.Visibility = Visibility.Collapsed;
                StartGameText.Visibility = Visibility.Collapsed;

                // Initialize and start the game
                _gameEngine = new GameEngine(GameCanvas.ActualWidth, GameCanvas.ActualHeight);
                _gameEngine.ScoreUpdated += OnScoreUpdated;
                _gameEngine.GameOver += OnGameOver;
                _gameEngine.CollisionDetected += OnCollisionDetected;
                _gameEngine.ObstacleCreated += OnObstacleCreated;
                _gameEngine.ObstacleRemoved += OnObstacleRemoved;
                
                _gameEngine.StartGame();
                _isGameRunning = true;

                // Add player to canvas
                if (_gameEngine?.Player?.Visual != null)
                {
                    GameCanvas.Children.Add(_gameEngine.Player.Visual);
                    Canvas.SetLeft(_gameEngine.Player.Visual, _gameEngine.Player.X);
                    Canvas.SetTop(_gameEngine.Player.Visual, _gameEngine.Player.Y);
                }

                // Focus the window for keyboard input
                Focus();
            });
        }

        private void OnScoreUpdated(int score)
        {
            Dispatcher.Invoke(() =>
            {
                ScoreText.Text = score.ToString();
            });
        }

        private void OnGameOver()
        {
            Dispatcher.Invoke(() =>
            {
                _isGameRunning = false;
                GameOverText.Visibility = Visibility.Visible;
                StartGameText.Visibility = Visibility.Visible;
                StartButton.Content = "Yeniden Başlat";
            });
        }

        private void OnCollisionDetected()
        {
            // Flash the game canvas red to indicate collision
            Dispatcher.Invoke(() =>
            {
                var originalBackground = GameCanvas.Background;
                GameCanvas.Background = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
                
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += (s, e) =>
                {
                    GameCanvas.Background = originalBackground;
                    timer.Stop();
                };
                timer.Start();
            });
        }

        private void OnObstacleCreated(Obstacle obstacle)
        {
            if (obstacle?.Visual == null) return;

            Dispatcher.Invoke(() =>
            {
                GameCanvas.Children.Add(obstacle.Visual);
                Canvas.SetLeft(obstacle.Visual, obstacle.X);
                Canvas.SetTop(obstacle.Visual, obstacle.Y);
            });
        }

        private void OnObstacleRemoved(Obstacle obstacle)
        {
            if (obstacle?.Visual == null) return;

            Dispatcher.Invoke(() =>
            {
                GameCanvas.Children.Remove(obstacle.Visual);
            });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // B tuşu: zıplama
            if (e.Key == Key.B && _isGameRunning && _gameEngine != null)
            {
                _gameEngine.Jump();
                e.Handled = true;
            }
            // Enter: oyunu başlat / yeniden başlat
            else if (e.Key == Key.Enter)
            {
                if (!_isGameRunning)
                {
                    StartGame();
                }
                e.Handled = true;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            
            // Update game dimensions if game is running
            if (_gameEngine != null && _isGameRunning)
            {
                _gameEngine.StopGame();
                InitializeGame();
                StartGame();
            }
        }
    }
}