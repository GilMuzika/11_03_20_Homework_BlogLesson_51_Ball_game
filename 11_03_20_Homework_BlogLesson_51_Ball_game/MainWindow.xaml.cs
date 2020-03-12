using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11_03_20_Homework_BlogLesson_51_Ball_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thickness BallStartpos = new Thickness(116, 160, 0, 0);
        Thickness BallCurrentPos;
        Thickness BallnextPos = new Thickness();
        Thickness PadStartPos = new Thickness(200, 390, 0, 0);
        Thickness PadCurrentPos;
        ThicknessAnimation MoveTheBall;
        ThicknessAnimation MoveThePad;
        Storyboard PlayGame = new Storyboard();
        Storyboard PlayPad;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            BallCurrentPos = BallStartpos;
            BallnextPos.Left = playground.Width;
            BallnextPos.Top = playground.Height;
            AnimateBall(BallnextPos, BallCurrentPos);

            playground.KeyDown += new KeyEventHandler(playground_KeyDown);
            ball.LayoutUpdated += new EventHandler(ball_LayoutUpdated);
        }

        void ball_LayoutUpdated(object sender, EventArgs e)
        {
            BallCurrentPos = ball.Margin;
            if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left - 30) && ball.Margin.Left <= (pad.Margin.Left + 30))
            {
                BallnextPos.Top = 0;
                BallnextPos.Left = BallCurrentPos.Left - 200;
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left + 30) && ball.Margin.Left <= (pad.Margin.Left + 60))
            {
                BallnextPos.Top = 0;                
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left + 60) && ball.Margin.Left <= (pad.Margin.Left + 100))
            {
                BallnextPos.Top = 0;
                BallnextPos.Left = BallCurrentPos.Left + 200;
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if(ball.Margin.Top <= 25)
            {
                BallnextPos.Top = playground.Height;
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if(ball.Margin.Left <= 0)
            {
                BallnextPos.Left = playground.Width;
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if(ball.Margin.Left >= playground.Width - 50)
            {
                BallnextPos.Left = 0;
                AnimateBall(BallnextPos, BallCurrentPos);
            }
            else if(ball.Margin.Top >= pad.Margin.Top + ball.Height)
            {
                MessageBox.Show("Game Over!", "Padding Ball");
                Application.Current.Shutdown();
            }
        }
        void playground_KeyDown(object sender, KeyEventArgs e)
        {
            PadCurrentPos = pad.Margin;
            double xpadValue = 0;
            if (e.Key == Key.Left)
                if (pad.Margin.Left > -100)
                    xpadValue = -50;
            if (e.Key == Key.Right)
                if (pad.Margin.Left <= (playground.Width - pad.Width))
                    xpadValue = 50;
            AnimatePad(xpadValue);
        }
        void AnimateBall(Thickness next, Thickness current)
        {
            MoveTheBall = new ThicknessAnimation();
            MoveTheBall.From = current;
            MoveTheBall.To = next;
            MoveTheBall.Duration = new Duration(TimeSpan.FromSeconds(3));
            Storyboard.SetTargetName(MoveTheBall, "ball");
            Storyboard.SetTargetProperty(MoveTheBall, new PropertyPath(Rectangle.MarginProperty));
            if (PlayGame.Children.Count > 0) PlayGame.Children.RemoveAt(0);
            PlayGame.Children.Add(MoveTheBall);
            PlayGame.Begin(this, true);
        }
        private void AnimatePad(double x)
        {
            MoveThePad = new ThicknessAnimation();
            MoveThePad.From = PadCurrentPos;
            MoveThePad.To = new Thickness(pad.Margin.Left + x, pad.Margin.Top, 0, 0);
            MoveThePad.Duration = new Duration(TimeSpan.FromSeconds(0));
            Storyboard.SetTargetName(MoveThePad, "pad");
            Storyboard.SetTargetProperty(MoveThePad, new PropertyPath(Rectangle.MarginProperty));
            PlayPad = new Storyboard();
            if (PlayPad.Children.Count > 0) PlayPad.Children.RemoveAt(0);
            PlayPad.Children.Add(MoveThePad);
            PlayPad.Begin(this, true);
        }

        private void ShowAboutBox(object sender, RoutedEventArgs e)
        {
            //Here supposed to be a call to a Windows Form object, but I didn't managed to add a Wondows Form to a WPF project
        }
        private void ExitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void currentGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
