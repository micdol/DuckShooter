using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DuckShooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Duck> duckList;
        private int numOfDuckShot;
        private const int MAX_NUM_OF_DUCKS = 5;

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick; //event!!
            dispatcherTimer.Interval = new TimeSpan(1000 / 24, 0, 0);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Start();
            duckList = new List<Duck>();
            numOfDuckShot = 0;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (duckList.Count < MAX_NUM_OF_DUCKS)
            {
                Duck d = new Duck(
                    new Point(
                        window_DuckShooter.Width,
                        new Random().Next(25, (int)window_DuckShooter.Height - 100)),
                    window_DuckShooter.Width);
                duckList.Add(d);
                grid_windowDuckShooter.Children.Add(d);
            }

            foreach(Duck d in duckList)
            {
                d.move();
            }
        }

        private void window_DuckShooter_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(window_DuckShooter);
            var w = image_target.Width;
            var h = image_target.Height;
            image_target.Margin = new Thickness(pos.X - w / 2.0, pos.Y - h / 2.0, 0, 0);
        }

        private void image_target_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < duckList.Count; i++)
            {
                Duck d = duckList[i];
                if (d.shoot(e.GetPosition(window_DuckShooter))) 
                {
                    Console.WriteLine("Removing duck...");
                    grid_windowDuckShooter.Children.Remove(d);
                    duckList.RemoveRange(i, 1);
                    numOfDuckShot++;
                    break;
                }
            }
            label_duckCounter.Content = "DuckCounter: " + numOfDuckShot;
        }
    }
}
