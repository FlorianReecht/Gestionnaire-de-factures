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
            ClientList.ItemsSource = ConnexionBd.ClientList;
            _connexion = new ConnexionBd();
            _connexion.Fill_Client_List();

            nomClient.TextChanged += new TextChangedEventHandler(TextChanged);
           
        }
        void TextChanged(object Sender, TextChangedEventArgs e)
        {
            TextBox o = (TextBox)Sender;
            _connexion.UpdateClientList(o.Text);
        }

        private void Choose_Client_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private void See_Facture_Client_Click(object sender, RoutedEventArgs e)
        {
            VoirFacturesClient newWindow=new VoirFacturesClient();
            newWindow.Show();
            this.Close();
        }
    }
}
