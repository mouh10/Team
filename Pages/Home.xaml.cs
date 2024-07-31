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
using WPFModernVerticalMenu.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using System.Collections.Specialized;
using WPFModernVerticalMenu;

namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Nom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Txt_Description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Txt_Prix_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Txt_Quantite_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Vider()
        {
            Txt_Nom.Text = "";
            Txt_Description.Text = "";
            Txt_Prix.Text = "";
            Txt_Quantite.Text = "";
            Txt_Message.Text = "";
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (Txt_Nom.Text.Trim() != "" &&
            Txt_Description.Text.Trim() != "" &&
            Txt_Prix.Text.Trim() != "" &&
            Txt_Quantite.Text.Trim() != "")
            {
                var Valeurs = new Dictionary<string, string>
                {
                    { "Nom", Txt_Nom.Text },
                    { "Description", Txt_Description.Text },
                    { "Prix", Txt_Prix.Text },
                    { "Quantite", Txt_Quantite.Text },
                };
                var Content = new FormUrlEncodedContent(Valeurs);
                try
                {
                    using (var Client = new HttpClient())
                    {
                        Client.BaseAddress = new Uri("http://localhost/APIPhp/");
                        Client.DefaultRequestHeaders.Accept.Clear();
                        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var Response = Client.PostAsync("Create.php", Content).Result;
                        if (Response.IsSuccessStatusCode)
                        {
                            Vider();
                            Txt_Message.Text = "PRODUIT AJOUTER !";
                        }
                        else
                        {
                            Txt_Message.Text = "DETECTION D'ERREUR !";
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Txt_Message.Text = "TOUS LES CHAMPS SONT OBLIGATOIRE !";
            }
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Vider();
        }
    }
}
