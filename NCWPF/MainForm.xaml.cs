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
using System.Windows.Shapes;

namespace NCWPF
{
    /// <summary>
    /// MainForm.xaml 的交互逻辑
    /// </summary>
    public partial class MainForm : Window
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("ddd");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Console.WriteLine(sender.ToString());
            Storyboard storyboard = new Storyboard();   //创建Storyboard对象
            DoubleAnimation doubleAnimation = new DoubleAnimation(
              0,
              1,
              new Duration(TimeSpan.FromSeconds(1))
            );
            Storyboard.SetTarget(doubleAnimation, Grid2);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }
    }
}
