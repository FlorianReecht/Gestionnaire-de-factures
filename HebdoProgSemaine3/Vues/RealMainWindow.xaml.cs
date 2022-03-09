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

namespace HebdoProgSemaine3.Vues
{

    public partial class RealMainWindow : Window
    {
        public ConnexionBd _connexion;//Permet la connexion à la base de données et fait les requètes 


        public RealMainWindow()
        {
            _connexion = new ConnexionBd();
            InitializeComponent();
            ContentView.Content= new MainVue(_connexion);
        }

        //Affichage des vues. 
        private void VoirFactures(object sender, RoutedEventArgs e)
        {
            if (_connexion._currentClient != null)
            {

                ContentView.Content = new VoirFacturesVue(_connexion);
            }
            else
            {
                MessageBox.Show("Veuillez d'abord selectionner un client");
            }
        }
        private void MooveToAddFacturePage(object sender, RoutedEventArgs e)
        {
            if (_connexion._currentClient != null)
            {

                ContentView.Content = new AddFactureVue(_connexion);
            }
            else
            {
                MessageBox.Show("Veuillez d'abord selectionner un client");
            }
        }
        private void goBackToMain(object sender,RoutedEventArgs e)
        {
            ContentView.Content = new MainVue(_connexion);
        }

        private void MooveToAddClientPage(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new AddClientVue(_connexion);
        }
    }
}
