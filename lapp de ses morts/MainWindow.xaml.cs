using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace lapp_de_ses_morts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Login());
        }

    }
}