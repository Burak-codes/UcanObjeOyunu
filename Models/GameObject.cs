using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace UcanObjeOyunu.Models
{
    public abstract class GameObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public UIElement Visual { get; protected set; } = null!;
        public bool IsActive { get; set; } = true;

        public Rect Bounds => new Rect(X, Y, Width, Height);

        public virtual void Update() { }

        public bool IntersectsWith(GameObject other)
        {
            return Bounds.IntersectsWith(other.Bounds);
        }
    }
}
