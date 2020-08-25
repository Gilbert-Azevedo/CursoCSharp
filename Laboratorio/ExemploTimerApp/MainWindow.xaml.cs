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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ExemploTimerApp
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>

    public delegate double Teste(double x);
        
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Atualizar;
        }

        private DispatcherTimer timer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime x = DateTime.Now;
            txt.Text = x.ToString("hh:mm:ss");
        }

        private void Atualizar(object sender, EventArgs e)
        {
            DateTime x = DateTime.Now;
            txt.Text = x.ToString("hh:mm:ss");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled) timer.Start();
            else timer.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Teste x = Dobro;
            MessageBox.Show(x(2).ToString());
        }

        public static double Dobro(double x)
        {
            return 2 * x;
        }
    }
}
