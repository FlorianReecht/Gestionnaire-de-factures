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
        Facture CurrentFacture { get; set; }//La facture que l'on est entrain de traiter
        LigneFacture currentLigneFacture { get; set; }
  
        public MainWindow()//Constructeur
        {
            InitializeComponent();
            CurrentFacture = new Facture();
            ListProduit.ItemsSource = ConnexionBd.ProductList;
            currentFacture.ItemsSource = CurrentFacture.LignesFactures;
            _connexion = new ConnexionBd();
            _connexion.Fill_Produit_List();
            
            //Modification des Champs de text
            nomProduit.TextChanged += new TextChangedEventHandler(TextChanged);
            QteBox.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);

        }

        void TextChanged(object Sender, TextChangedEventArgs e)//Changement du champ de recherche produit
        {
            TextBox o = (TextBox)Sender;
            _connexion.UpdateProduitList(o.Text);

        }

        private void ClickOnProduitOnList(object sender, MouseButtonEventArgs e)//Séléction du produit dans la liste
        {
            ListBox listBox = sender as ListBox;
            Produit p=listBox.SelectedItem as Produit;

            currentProduit.Text = listBox.SelectedItem.ToString();
            _connexion.CurrentProduit = p;
        }

        private void goBackToMain(object sender, RoutedEventArgs e)//Revenir à la page précédente
        {
            new ChoixClient().Show();
            this.Close();
        }

        private void CalculCout_Click(object sender, RoutedEventArgs e)
        {
            double prix = CurrentFacture.calculPrix();
            PrixTotal.Text= "Prix Total :" +prix.ToString() +"€";
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t=  sender as TextBox;
            int parsedValue;
            if (!int.TryParse(t.Text, out parsedValue))
            {
                t.Text = "";//Le reset se fait deux fois à opti
            }
            else
            {
                int qte = Int32.Parse(t.Text);
                MessageBox.Show(qte.ToString());
                
            }
        }

        private void currentFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)//Lorsque l'on sélectionne une ligne de la facture Actuelle
        {
            ComboBox comboBox = sender as ComboBox;
            currentLigneFacture=comboBox.SelectedItem as LigneFacture;

        }

        private void AjoutFacture(object sender, RoutedEventArgs e)
        {
            LigneFacture newLine = new LigneFacture(_connexion.CurrentProduit,Int32.Parse(QteBox.Text));
            CurrentFacture.AddLigne(newLine);
        }
    }
}
