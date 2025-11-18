using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UcanObjeOyunu.Models
{
    public class Player : GameObject
    {
        private const double Gravity = 0.5;
        private const double JumpForce = -10;
        private double _velocity;

        public Player()
        {
            Width = 40;
            Height = 40;
            X = 100;
            Y = 200;
            _velocity = 0;

            var ellipse = new Ellipse
            {
                Width = Width,
                Height = Height,
                Fill = Brushes.Red,
                Stroke = Brushes.DarkRed,
                StrokeThickness = 2
            };
            Visual = ellipse;
        }

        public void Jump()
        {
            _velocity = JumpForce;
        }

        public override void Update()
        {
            _velocity += Gravity;
            Y += _velocity;

            // Keep player in bounds
            if (Y < 0)
            {
                Y = 0;
                _velocity = 0;
            }

            if (Visual != null)
            {
                Canvas.SetLeft(Visual, X);
                Canvas.SetTop(Visual, Y);
            }
        }
    }
}
