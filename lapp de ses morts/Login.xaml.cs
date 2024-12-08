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
using System.Net.Http;

namespace lapp_de_ses_morts
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        public async void OnClick(object sender, RoutedEventArgs e)
        {
            string username = usernameText.Text;
            string password = passwordText.Text;

            string url = "http://localhost:8080/login";
            string jsonData = "{ \"username\": \"" + username + "\" , \"password\": \"" + password + "\"}";

            try
            {
                string response = await SendPostRequestAsync(url, jsonData);
                MessageBox.Show($"Response: {response}");
                NavigationService.Navigate(new Dashboard());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task<string> SendPostRequestAsync(string url, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
