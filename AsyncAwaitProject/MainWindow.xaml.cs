using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace AsyncAwaitProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lbl.Text = "Press button to show async exmaple";
        }

        private async Task<int> Calculate()
        {
            await Task.Delay(5000);
            return 123;
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            lbl.Text = "Performing calculation...";
            int i = await Calculate();
            lbl.Text = i.ToString();

            await Task.Delay(2000);
            lbl.Text = "do downloading...";

            using (var wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                lbl.Text = data.Split('\n')[0].Trim();
            }
        }
    }
}
