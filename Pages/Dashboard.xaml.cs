using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFModernVerticalMenu.Model;

namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            DataGride.ItemsSource = Donnee();
        }

        private void DataGride_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private List<Produits> Donnee()
        {
            HttpClient Client;
            Client = new HttpClient();
            var Services = new List<Produits>();
            Client.BaseAddress = new Uri("http://localhost/APIPhp/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var Response = Client.GetAsync("List.php").Result;
            if (Response.IsSuccessStatusCode)
            {
                var ResponseData = Response.Content.ReadAsStringAsync().Result;
                Services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produits>>(ResponseData);
            }
            return Services;
        }

        private List<Produits> RecupererProduit(int P_Id)
        {
            HttpClient Client;
            Client = new HttpClient();
            var Services = new List<Produits>();
            Client.BaseAddress = new Uri("http://localhost/APIPhp/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var Response = Client.GetAsync("Details.php?Id={Produit_Id}").Result;
            if (Response.IsSuccessStatusCode)
            {
                var ResponseData = Response.Content.ReadAsStringAsync().Result;
                Services = JsonConvert.DeserializeObject<List<Produits>>(ResponseData);
            }
            return Services;
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            var produitSelectionne = DataGride.SelectedItem as Produits;
            if (produitSelectionne != null)
            {
                var result = MessageBox.Show(
                     $"Voulez-vous vraiment supprimer le produit : {produitSelectionne.Nom} ?",
                     "Confirmation de Suppression",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    int? Produit_Id = produitSelectionne.Id;
                    using (var Client = new HttpClient())
                    {
                        try
                        {
                            Client.BaseAddress = new Uri("http://localhost/APIPhp/");
                            Client.DefaultRequestHeaders.Accept.Clear();
                            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var Response = Client.GetAsync($"Delete.php?Id={Produit_Id}").Result;
                            if (Response.IsSuccessStatusCode)
                            {
                                MessageBox.Show($"LE PRODUIT {produitSelectionne.Nom} A ETE SUPPRIMER !");
                                DataGride.ItemsSource = Donnee();
                            }
                            else
                            {
                                MessageBox.Show(" ERREUR DE SUPPRESSION !");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"COMMUNICATION IMPOSSIBLE", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("SELECTIONNER UN PRODUIT POUR CONTINUER !");
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            var produitSelectionne = DataGride.SelectedItem as Produits;
            if (produitSelectionne != null)
            {
                List<Produits> P = RecupererProduit(produitSelectionne.Id);
                if (P != null) { 
                    MessageBox.Show("PRODUIT SELECTIONNER");
                }
            }
            else
            {
                MessageBox.Show("SELECTIONNER UN PRODUIT POUR CONTINUER !");
            }
        }
    }
}
