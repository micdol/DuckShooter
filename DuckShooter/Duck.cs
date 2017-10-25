using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DuckShooter
{
    class Duck : Image
    {
        const int WIDTH = 80;
        const int HEIGHT = 80;

        private double amp;
        private double phase;
        private double period;
        private double velocity;
        private double maxX;

        //top left pos
        private Point pos;
        public Point Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
        }

        public Duck(Point pos, double maxX) : base()
        {
            this.pos = pos; this.maxX = maxX; isAlive = true;
            Width = WIDTH;
            Height = HEIGHT;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Source = new BitmapImage(new Uri("Resources/Images/duck.png", UriKind.Relative));
            Margin = new Thickness(pos.X, pos.Y, 0, 0);

            var r = new Random();
            amp = r.Next(20, 50);
            phase = r.NextDouble() * Math.PI * 2.0;
            period = r.Next(100, 500);
            velocity = r.NextDouble() * 3 + 1;
        }

        public void move()
        {
            var nx = (pos.X - velocity);
            var ny = pos.Y + (amp * Math.Sin((Math.PI * 2.0 * nx + phase) / period));
            Margin = new Thickness(nx, ny, 0, 0);
            pos = new Point(nx, pos.Y);
            if(nx < -WIDTH)
            {
                velocity *= -1;
                RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = -1; 
                RenderTransform = flipTrans;
            }
            else if(nx > maxX)
            {
                velocity *= -1;
                RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = 1;
                RenderTransform = flipTrans;
            }
        }

        public bool shoot(Point shot)
        {
            Rect r = new Rect(pos, new Size(WIDTH, HEIGHT));
            return r.Contains(shot);
        }
        
        
    }
}
