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

namespace HebdoProgSemaine3.Vues
{
   
    public partial class MainVue : UserControl
    {
        ConnexionBd _connexion;

        /**
        public MainVue()
        {
            InitializeComponent();
        }
        **/
        public MainVue(ConnexionBd co)
        {
            InitializeComponent();
            
            _connexion = co;
            _connexion.ClearAllLists();
            _connexion.Fill_Client_List();

            ClientList.ItemsSource = ConnexionBd.ClientList;
            nomClient.TextChanged += new TextChangedEventHandler(TextChanged);
            if(_connexion._currentClient != null)
            {
                currentClient.Content= _connexion._currentClient.Nom + _connexion._currentClient.Prenom;
            }

        }
        void TextChanged(object Sender, TextChangedEventArgs e)
        {
            //o.text est vide si on rajoute le style par dessus
            TextBox o = (TextBox)Sender;
            _connexion.UpdateClientList(o.Text);

        }
   

        private void ClickOnClientOnList(object sender, MouseButtonEventArgs e)//quand on clique sur un client de la liste
        {
            ListBox listBox = sender as ListBox;
            Client currentCli = listBox.SelectedItem as Client;
            if(currentCli != null)
            {
                _connexion._currentClient = currentCli;
                currentClient.Content = _connexion._currentClient.Nom + " " + _connexion._currentClient.Prenom;//Nom du client séléctionné dans la page
            }
           
        }
    }
}
