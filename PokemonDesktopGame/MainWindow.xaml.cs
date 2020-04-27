using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using PokemonDesktopGame.Class;

namespace PokemonDesktopGame
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timerGet = new DispatcherTimer();
        ArrayList flys = new ArrayList();
        ArrayList pets = new ArrayList();
        int cap = 50;

        Storyboard ComboFadeSB;
        Storyboard ComboHigh1SB;
        Storyboard LabelComboSB;
        public MainWindow()
        {
            InitializeComponent();

            DebugWindow debugWindow = new DebugWindow();
            debugWindow.Show();


            timer1.Interval = new System.TimeSpan(0, 0, 0,0,200);
            timer1.Tick += new EventHandler(timer1_Tick);

            timerGet.Interval = new System.TimeSpan(0, 0, 0, 0, 10);
            timerGet.Tick += new EventHandler(timerGet_Tick);
            //timerGet.Start();

            ComboFadeSB = (Storyboard)Resources["ComboFadeSB"];
            ComboHigh1SB = (Storyboard)Resources["ComboHighBG1SB"];
            LabelComboSB = (Storyboard)Resources["LabelComboSB"];
            for (int i = 0; i < cap; i++)
            {
                flys.Add(new Class.FlyOut());
                ((Class.FlyOut)flys[i]).can = mainCanvas;
                ((Class.FlyOut)flys[i]).ComboFadeSB = ComboFadeSB;
                ((Class.FlyOut)flys[i]).ComboHigh1SB = ComboHigh1SB;
                ((Class.FlyOut)flys[i]).lbCombo = this.labelCombo;
                ((Class.FlyOut)flys[i]).imgComboHigh1BG = this.ImgComboHigh1BG;
                ((Class.FlyOut)flys[i]).LabelComboSB = LabelComboSB;
                ((Class.FlyOut)flys[i]).initialize();
            }

            for (int i = 1; i <= 487; i++)
            {
                pets.Add(new BitmapImage(new Uri("/PokemonDesktopGame;component/Res/Images/Pokemon/pokemon%20%28" + i.ToString() + "%29.png", UriKind.Relative)));
            }

        }

        void timerGet_Tick(object sender, EventArgs e)
        {
            Common.srt0 = System.Windows.Input.Mouse.GetPosition(mainWindow).ToString();
        }

        int num = 0;
        Random rd = new Random();
        void timer1_Tick(object sender, EventArgs e)
        {

            Class.FlyOut obj = (Class.FlyOut)flys[num];
            
            obj.point = new Point(rd.Next(-50, 1700),rd.Next(-50,1100));
            obj.speed = rd.Next(200, 400);
            obj.derection = rd.Next(180 ,360);

            obj.comboCan = ComboCanvas;
            obj.source = (ImageSource)pets[rd.Next(1, 487) - 1];
            obj.ready();
            obj.start();
            num++;
            if (num == cap-1)
            {
                num = 0;
            }

            Common.srt1 = num.ToString();
            
        }

        private void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //Point p = new Point();

            //p = e.GetPosition(mainCanvas);
            
        }
        

        

        private void mainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
//             if (timer1.IsEnabled)
//             { timer1.Stop(); }
//             else
//             { timer1.Start(); }
            
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            start();
        }

        private void start()
        {
            Common.dt1 = DateTime.Now;
            timer1.Start();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}
