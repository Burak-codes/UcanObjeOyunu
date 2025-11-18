using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UcanObjeOyunu.Models
{
    public class Obstacle : GameObject
    {
        private const double ObstacleSpeed = 3;
        private readonly string[] _topics = 
        { 
            "Inheritance", 
            "Polymorphism", 
            "Encapsulation", 
            "Abstraction",
            "Interfaces",
            "UML Diagrams",
            "Design Patterns",
            "SOLID Principles"
        };

        public string Topic { get; private set; }
        public bool IsScored { get; set; }

        public Obstacle(double gameHeight, Random random)
        {
            Width = 60;
            Height = random.Next(100, (int)(gameHeight * 0.6));
            X = 800; // Start off-screen
            Y = random.Next(50, (int)(gameHeight - Height - 50));
            Topic = _topics[random.Next(_topics.Length)];

            Visual = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(100, 200),
                    (byte)random.Next(100, 200),
                    (byte)random.Next(100, 200))),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            var textBlock = new System.Windows.Controls.TextBlock
            {
                Text = Topic,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                TextAlignment = System.Windows.TextAlignment.Center,
                Width = Width - 10,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold
            };

            var viewbox = new System.Windows.Controls.Viewbox
            {
                Child = textBlock,
                Width = Width - 10,
                Height = Height - 10
            };

            var grid = new System.Windows.Controls.Grid();
            grid.Children.Add(Visual);
            grid.Children.Add(viewbox);

            // Store the grid as the visual element
            Visual = grid;
        }

        public override void Update()
        {
            X -= ObstacleSpeed;
            if (Visual != null)
            {
                Canvas.SetLeft(Visual, X);
                Canvas.SetTop(Visual, Y);
            }
        }
    }
}
