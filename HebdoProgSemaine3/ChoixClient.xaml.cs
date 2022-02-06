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
using System.Windows.Shapes;
using System.Windows.Input;

namespace HebdoProgSemaine3
{
    /// <summary>
    /// Logique d'interaction pour ChoixClient.xaml
    /// </summary>
    public partial class ChoixClient : Window
    {


        public ConnexionBd _connexion;//Permet la connexion à la base de données et fait les requètes  
        public ChoixClient()
        {

            InitializeComponent();
            _connexion = new ConnexionBd();
            _connexion.Fill_Client_List();
            ClientList.ItemsSource = ConnexionBd.ClientList;
            nomClient.TextChanged += new TextChangedEventHandler(TextChanged);
           
        }
        void TextChanged(object Sender, TextChangedEventArgs e)
        {
            TextBox o = (TextBox)Sender;
            _connexion.UpdateClientList(o.Text);
        }

        private void Choose_Client_Click(object sender, RoutedEventArgs e)//Changement de Page
        {
            if(_connexion.CurrentClient!=null)
            {
                MainWindow w = new MainWindow();
                w.Show();
                w.nomCli.Content = " Client séléctioné : " + currentClient.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez d'abord selectionner un client");
            }
    

        }

        private void See_Facture_Client_Click(object sender, RoutedEventArgs e)
        {
            new VoirFacturesClient().Show();    
            this.Close();
        }

        private void ClickOnClientOnList(object sender, MouseButtonEventArgs e)//Séléction du client
        {
            ListBox listBox = sender as ListBox;
            Client currentCli=listBox.SelectedItem as Client;
            _connexion.CurrentClient = currentCli;
            currentClient.Text =_connexion.CurrentClient.Nom + " "+ _connexion.CurrentClient.Prenom;//Nom du client séléctionné dans la page
            
        }
    }
}
