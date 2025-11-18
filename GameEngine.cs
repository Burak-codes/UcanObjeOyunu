using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using UcanObjeOyunu.Models;

namespace UcanObjeOyunu
{
    public class GameEngine
    {
        public event Action<int>? ScoreUpdated;
        public event Action? GameOver;
        public event Action? CollisionDetected;
        public event Action<Obstacle>? ObstacleCreated;
        public event Action<Obstacle>? ObstacleRemoved;

        private readonly DispatcherTimer _gameLoop;
        private readonly Random _random;
        private readonly double _gameHeight;
        private readonly double _gameWidth;
        private int _score;
        private int _obstacleSpawnCounter;
        private const int ObstacleSpawnInterval = 120; // frames

        public Player Player { get; private set; } = null!;
        public List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();
        public bool IsGameRunning { get; private set; }

        public GameEngine(double gameWidth, double gameHeight)
        {
            _gameWidth = gameWidth;
            _gameHeight = gameHeight;
            _random = new Random();
            _gameLoop = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };
            _gameLoop.Tick += GameLoop_Tick;
        }

        public void StartGame()
        {
            if (IsGameRunning) return;

            Player = new Player();
            Obstacles.Clear();
            _score = 0;
            _obstacleSpawnCounter = 0;
            IsGameRunning = true;
            ScoreUpdated?.Invoke(_score);
            _gameLoop.Start();
        }

        public void StopGame()
        {
            _gameLoop.Stop();
            IsGameRunning = false;
        }

        public void Jump()
        {
            if (IsGameRunning)
            {
                Player.Jump();
            }
        }

        private void GameLoop_Tick(object? sender, EventArgs e)
        {
            if (!IsGameRunning) return;

            // Update game objects
            if (Player != null)
            {
                Player.Update();

                // Spawn new obstacles
                if (_obstacleSpawnCounter >= ObstacleSpawnInterval)
                {
                    var obstacle = new Obstacle(_gameHeight, _random);
                    Obstacles.Add(obstacle);
                    ObstacleCreated?.Invoke(obstacle);
                    _obstacleSpawnCounter = 0;
                }
                _obstacleSpawnCounter++;

                // Update obstacles
                foreach (var obstacle in Obstacles.ToList())
                {
                    if (obstacle == null) continue;
                    obstacle.Update();

                // Remove obstacles that are off-screen
                if (obstacle.X + obstacle.Width < 0)
                {
                    Obstacles.Remove(obstacle);
                    ObstacleRemoved?.Invoke(obstacle);
                    continue;
                }

                    // Check for scoring
                    if (Player != null && !obstacle.IsScored && obstacle.X + obstacle.Width < Player.X)
                    {
                        obstacle.IsScored = true;
                        _score++;
                        ScoreUpdated?.Invoke(_score);
                    }

                    // Check for collisions
                    if (Player != null && Player.IntersectsWith(obstacle))
                    {
                        CollisionDetected?.Invoke();
                        GameOver?.Invoke();
                        StopGame();
                        return;
                    }
            }

            }
            
            // Check for game over (falling out of bounds)
            if (Player != null && Player.Y > _gameHeight)
            {
                GameOver?.Invoke();
                StopGame();
            }
        }
    }
}
