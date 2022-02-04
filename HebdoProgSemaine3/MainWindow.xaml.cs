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

namespace HebdoProgSemaine3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   

    public partial class MainWindow : Window
    {
        public ConnexionBd _connexion;//Permet la connexion à la base de données et fait les requètes 
        
        void TextChanged(object Sender, TextChangedEventArgs e)
        {
            TextBox o = (TextBox)Sender;
            _connexion.UpdateProduitList(o.Text);

        }

        public MainWindow()
        {
            InitializeComponent();
            ListProduit.ItemsSource = ConnexionBd.ProductList;
            _connexion = new ConnexionBd();
            //_connexion.Connect();
            _connexion.Fill_Produit_List();
            nomProduit.TextChanged += new TextChangedEventHandler(TextChanged);

        }

        private void CalculCout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrixTotale_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
  
    }
}
