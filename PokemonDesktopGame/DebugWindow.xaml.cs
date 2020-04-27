using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PokemonDesktopGame
{
    /// <summary>
    /// DebugWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DebugWindow : Window
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        public DebugWindow()
        {
            InitializeComponent();
            timer1.Interval = new System.TimeSpan(0, 0, 0, 0, 10);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            listboxContent.Items[0] = Class.Common.srt0;
            listboxContent.Items[1] = Class.Common.srt1;
            listboxContent.Items[2] = Class.Common.srt2;
            listboxContent.Items.Refresh();
        }

    }
}
