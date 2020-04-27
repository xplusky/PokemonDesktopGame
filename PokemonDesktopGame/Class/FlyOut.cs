using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PokemonDesktopGame;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Media;
using System.Collections;
namespace PokemonDesktopGame.Class
{
    public class FlyOut
    {
        
        public FlyOut(Point point,double speed,double derection)
        {
            this.point = point;
            this.speed = speed;
            this.derection = derection;

        }
        public FlyOut()
        {}

        DispatcherTimer timer1 = new DispatcherTimer();
        public Point point { get; set; }
        public double speed { get; set; }
        public double derection { get; set; }
        
        public Canvas can { get; set; }
        Image flyImg = new Image();
        Image cutImg = new Image();
        public DateTime beginTime { get; set; }
        public int num { get; set; }
        public ImageSource source { get; set; }
        double speedX { get; set; }
        double speedY { get; set; }
        public bool firstInit { get; set; }
        
        double size = 160;

        Storyboard faseInSB = new Storyboard();
        Storyboard changeSizeSB = new Storyboard();
        Storyboard restoreSizeSB = new Storyboard();
        Storyboard faseOutSB = new Storyboard();
        Storyboard cutSB = new Storyboard();
        public Storyboard ComboFadeSB { get; set; }
        public Storyboard ComboHigh1SB { get; set; }
        Canvas canvas = new Canvas();

        public Image imgComboHigh1BG { get; set; }

        public TextBlock lbCombo { get; set; }

        public Storyboard LabelComboSB  { get; set; }
        public void initialize()
        {

            can.Children.Add(canvas);
            canvas.Width = size;
            canvas.Height = size;

            canvas.Children.Add(flyImg);
            flyImg.Stretch = Stretch.Fill;
            flyImg.Width = size;
            flyImg.Height = size;
            timer1.Interval = new System.TimeSpan(0, 0, 0, 0, 5);
            timer1.Tick += new EventHandler(timer1_Tick);
            flyImg.RenderTransformOrigin = new Point(0.5, 0.5);

            canvas.MouseMove += new System.Windows.Input.MouseEventHandler(canvas_MouseMove);
            cutImg.Visibility = Visibility.Collapsed;
            canvas.Children.Add(cutImg);
            cutImg.Stretch = Stretch.Fill;
            cutImg.Width = size;
            cutImg.Height = size;
            cutImg.RenderTransformOrigin = new Point(0.5, 0.5);
            cutImg.Source = new BitmapImage(new Uri("/PokemonDesktopGame;component/Res/Images/Effect/Cut1.png", UriKind.Relative));

            //fadein storyboard
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, flyImg);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            faseInSB.Children.Add(doubleAnimation);

            DoubleAnimation cutInDA = new DoubleAnimation(1, new Duration(TimeSpan.FromMilliseconds(10)));
            Storyboard.SetTarget(cutInDA, cutImg);
            Storyboard.SetTargetProperty(cutInDA, new PropertyPath("Opacity"));
            faseInSB.Children.Add(cutInDA);

            //cut storyboard
            DoubleAnimation cutDA = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(300)));
            Storyboard.SetTarget(cutDA, cutImg);
            Storyboard.SetTargetProperty(cutDA, new PropertyPath("Opacity"));
            cutSB.Children.Add(cutDA);

            //fadeout storyboard
            DoubleAnimation fadeOutDA = new DoubleAnimation( 0, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(fadeOutDA, flyImg);
            Storyboard.SetTargetProperty(fadeOutDA, new PropertyPath("Opacity"));
            faseOutSB.Children.Add(fadeOutDA);

            DoubleAnimation cutOutDA = new DoubleAnimation( 0, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(cutOutDA, cutImg);
            Storyboard.SetTargetProperty(cutOutDA, new PropertyPath("Opacity"));
            faseOutSB.Children.Add(cutOutDA);
        }

        ArrayList points = new ArrayList();
        void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isReady == true)
            {
                points.Add(e.GetPosition(flyImg));
                if (points.Count == 4)
                {
                    double o = -(Math.Atan2(-(((Point)points[3]).Y - ((Point)points[0]).Y), ((Point)points[3]).X - ((Point)points[0]).X) * 180 / Math.PI);
                    Common.srt2 = (o).ToString();
                    cutImg.RenderTransform = new RotateTransform(o, 0.5, 0.5);
                    cutImg.Visibility = Visibility.Visible;
                    cutSB.Begin();
                    faseOutSB.Begin();
                    Canvas.SetLeft(comboCan, imgPoint.X);
                    Canvas.SetTop(comboCan, imgPoint.Y);
                    if (DateTime.Now - Common.dt1 < TimeSpan.FromMilliseconds(2000))
                    {
                        Common.comboPoint++;
                        Common.dt1 = DateTime.Now;
                    }
                    else
                    {
                        Common.comboPoint = 1;
                        Common.dt1 = DateTime.Now;
                    }
                    if (Common.comboPoint > 5)
                    {
                        imgComboHigh1BG.Visibility = Visibility.Visible;
                        LabelComboSB.Begin();
                        ComboHigh1SB.Begin();

                    }
                    else
                    {
                        ComboHigh1SB.Stop();
                        LabelComboSB.Stop();
                        imgComboHigh1BG.Visibility = Visibility.Hidden;
                    }
                    if (Common.comboPoint != 1)
                    {
                        lbCombo.Text = Common.comboPoint.ToString() + "\nCOMBO";
                        ComboFadeSB.Begin();
                    }

                    using (SoundPlayer player = new SoundPlayer(PokemonDesktopGame.Properties.Resources.cut1))
                    {

                        player.Play();
                    }
                    points.Clear();
                    isReady = false;

                }
            }
        }

        public bool isReady { get; set; }
        public void ready()
        {
            
            timer1.Stop();
            flyImg.Source = source;
            Canvas.SetLeft(flyImg, point.X);
            Canvas.SetTop(flyImg, point.Y);
            speedX = Math.Cos(derection / 180 * Math.PI) * speed;
            speedY = Math.Sin(derection / 180 * Math.PI) * speed;
            beginTime = DateTime.Now;
            restoreSizeSB.Begin();
            isReady = true;
            cutImg.Visibility = Visibility.Collapsed;
            
        }
        

        void timer1_Tick(object sender, EventArgs e)
        {
            fly();
        }

        public void start()
        {
            timer1.Start();
            flyImg.Visibility = Visibility.Visible;
            fadeIn();
        }

        double x { get; set; }
        double y { get; set; }

        public Point imgPoint = new Point();
        public Canvas comboCan { get; set; }
        public void fly()
        {
            
            double t = (DateTime.Now - beginTime).TotalMilliseconds / 1000;
            x = t  * speedX;
            y = t*speedY+ 100 * Math.Pow(t ,2);

            Canvas.SetLeft(flyImg, point.X + x);
            Canvas.SetTop(flyImg, point.Y + y);

            Canvas.SetLeft(cutImg, point.X + x);
            Canvas.SetTop(cutImg, point.Y + y);

            if (Canvas.GetLeft(flyImg) < 800)
            {
                imgPoint.X = Canvas.GetLeft(flyImg) + flyImg.Width;
            }
            else
            {
                imgPoint.X = Canvas.GetLeft(flyImg) - comboCan.Width;
            }
            imgPoint.Y = Canvas.GetTop(flyImg);
            if (point.X + x < -300 || point.X + x > 1900 || point.Y + y > 1300)
            {
                timer1.Stop();
                points.Clear();
                flyImg.Visibility = Visibility.Collapsed;
            }

            
        }

        void fadeIn()
        {
            faseInSB.Begin();
        }

    }
}
