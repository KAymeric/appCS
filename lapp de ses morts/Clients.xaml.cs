using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace lapp_de_ses_morts
{
    /// <summary>
    /// Logique d'interaction pour Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        public Clients()
        {
            InitializeComponent();
            getClients();
        }

        public async void createClient(object sender, RoutedEventArgs e)
        {
            string name = nameInput.Text;
            string adress = adressInput.Text;
            string siret = siretInput.Text;

            string url = "http://localhost:8080/client";
            string jsonData = "{ \"name\": \"" + name + "\" , \"adress\": \"" + adress + "\", \"siret\": \"" + siret + "\"}";

            try
            {
                string response = await SendPostRequestAsync(url, jsonData);
                MessageBox.Show($"Response: {response}");
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

        private async void getClients()
        {
            string url = "http://localhost:8080/client";

            using HttpClient client = new HttpClient();


            try
            {
                // Envoie une requête GET
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Vérifie que le code HTTP est 2xx

                string json = await response.Content.ReadAsStringAsync();

                Console.WriteLine("JSON brut reçu :");
                Console.WriteLine(json);

                // Désérialiser la liste des clients
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var clients = JsonSerializer.Deserialize<List<Client>>(json, options);

                Console.WriteLine("\nListe des clients désérialisés :");
                foreach (var clientData in clients)
                {
                    Console.WriteLine($"ID : {clientData.Id}, Nom : {clientData.Name}, Adresse : {clientData.Address}, SIRET : {clientData.Siret}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Siret { get; set; }
    }
}
