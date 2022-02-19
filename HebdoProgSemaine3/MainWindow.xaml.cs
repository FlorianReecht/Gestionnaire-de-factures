using System;
using System.Collections.Generic;
using System.Data;
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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

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
            CurrentFacture = new Facture();//la datetime n'est pas affectée dans le constructeur par défault.
            CurrentFacture.DateFacture=DateTime.Now;
            MessageBox.Show(CurrentFacture.DateFacture.ToString());
            ListProduit.ItemsSource = ConnexionBd.ProductList;
            currentFacture.ItemsSource = CurrentFacture.LignesFactures;
            _connexion = new ConnexionBd();
            _connexion.Fill_Produit_List();
            currentLigneFacture = new LigneFacture();
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

            currentProduit.Content = listBox.SelectedItem.ToString();
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
            PrixTotal.Content= "Prix Total :" +prix.ToString() +"€";
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
            bool found = false;
            for(int i= 0; i < CurrentFacture.LignesFactures.Count; i++)
            {
                if(CurrentFacture.LignesFactures[i].Produit.NumProduit == newLine.Produit.NumProduit)
                {
                    found = true;
                    //On update la quantité et le prix 
                    CurrentFacture.LignesFactures[i]= new LigneFacture(newLine.Produit,CurrentFacture.LignesFactures[i].Qte+newLine.Qte);
                    //CurrentFacture.LignesFactures[i].calculPrixTotal();
                    break;
                }
            }
       
            if (!found)
            {
                CurrentFacture.AddLigne(newLine);
            }
            PrixTotal.Content ="Prix Total : "+CurrentFacture.calculPrix().ToString()+ " €";
        }

        private void Imprimer_Click(object sender, RoutedEventArgs e)
        {
           _connexion.InsertFactureIntoBdd(ConnexionBd.CurrentClient, CurrentFacture);
        }

        private void PrintFacture(object sender, RoutedEventArgs e)
        {
            //Création d'un document pdf avec Syncfusion.pdf
            using(PdfDocument document = new PdfDocument())
            {
                try
                {
                    PdfPage page = document.Pages.Add();
                    PdfGraphics graphics = page.Graphics;
                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                    graphics.DrawString("Facture du  " + CurrentFacture.DateFacture.ToString(), font, PdfBrushes.Black, new System.Drawing.PointF(0, 0));
                    graphics.DrawString("Client : " + ConnexionBd.CurrentClient.ToString(), font, PdfBrushes.Black, new System.Drawing.PointF(0, 20));
                    graphics.DrawString("Prix total  : " + CurrentFacture.calculPrix().ToString()+ " € ", font, PdfBrushes.Black, new System.Drawing.PointF(0, 100));
                    PdfGrid pdfGrid = new PdfGrid();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Produit");
                    dataTable.Columns.Add("Qte");
                    dataTable.Columns.Add("Prix");
                    foreach(LigneFacture ligne in CurrentFacture.LignesFactures)
                    {
                        dataTable.Rows.Add(new object[] {ligne.Produit.LibProduit.ToString(),ligne.Qte.ToString(),(ligne.Produit.Prix*ligne.Qte).ToString()+ "e" });
                    }
                    pdfGrid.DataSource= dataTable;
                    pdfGrid.Draw(page, new System.Drawing.PointF(40,40));
                    document.Save("Facture.pdf");
                    graphics.DrawString("Facture du  " + CurrentFacture.DateFacture.ToString(), font, PdfBrushes.Black, new System.Drawing.PointF(0, 0));


                    MessageBox.Show("Impression effectuée");
                    document.Close(true);



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'impression " + ex.Message);
                }

            }

        }
    }
}
