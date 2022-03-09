using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
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

namespace HebdoProgSemaine3.Vues
{
    /// <summary>
    /// Logique d'interaction pour AddFactureVue.xaml
    /// </summary>
    public partial class AddFactureVue : UserControl
    {
        ConnexionBd _connexion;
        Facture CurrentFacture { get; set; }//La facture que l'on est entrain de traiter
        LigneFacture currentLigneFacture { get; set; }


        public AddFactureVue(ConnexionBd co)
        {
            InitializeComponent();
            _connexion = co;
            CurrentFacture = new Facture();
            currentLigneFacture = new LigneFacture();

            _connexion.ClearAllLists();
            _connexion.Fill_Client_List();
            ListProduit.ItemsSource = ConnexionBd.ProductList;
            currentFacture.ItemsSource = CurrentFacture.LignesFactures;
            _connexion.Fill_Produit_List();
            nomProduit.TextChanged += new TextChangedEventHandler(TextChanged);
            QteBox.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);

        }

        private void Imprimer_Click(object sender, RoutedEventArgs e)
        {
            _connexion.InsertFactureIntoBdd(_connexion._currentClient, CurrentFacture);
        }

        private void currentFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)//Lorsque l'on sélectionne une ligne de la facture Actuelle
        {
            ComboBox comboBox = sender as ComboBox;
            currentLigneFacture = comboBox.SelectedItem as LigneFacture;

        }

        private void PrintFacture(object sender, RoutedEventArgs e)
        {
            //Création d'un document pdf avec Syncfusion.pdf
            using (PdfDocument document = new PdfDocument())
            {
                try
                {
                    int FinalPdfLength = 80;
                    PdfPage page = document.Pages.Add();
                    PdfGraphics graphics = page.Graphics;
                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                    int finalLength = 80;



                    PdfGrid pdfGrid = new PdfGrid();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Produit");
                    dataTable.Columns.Add("Qte");
                    dataTable.Columns.Add("Prix");
                    foreach (LigneFacture ligne in CurrentFacture.LignesFactures)
                    {
                        dataTable.Rows.Add(new object[] { ligne.Produit.PRO_LIB.ToString(), ligne.Qte.ToString(), (ligne.Produit.PRO_PRIX * ligne.Qte).ToString() + "e" });
                        finalLength += 20; 
                    }
                    pdfGrid.DataSource = dataTable;
                    pdfGrid.Draw(page, new System.Drawing.PointF(80, 80));
                    graphics.DrawString("Client : " + _connexion._currentClient.ToString(), font, PdfBrushes.Black, new System.Drawing.PointF(0, 20));
                    graphics.DrawString("Facture du  " + CurrentFacture.DateFacture.ToString(), font, PdfBrushes.Black, new System.Drawing.PointF(0, 0));
                    graphics.DrawString("Prix total  : " + CurrentFacture.calculPrix().ToString() + " € ", font, PdfBrushes.Black, new System.Drawing.PointF(0, finalLength));//Passer ça en position relative en fonction de la taille du tableau

                    document.Save("MVVMFacture.pdf");



                    MessageBox.Show("Impression effectu" +
                        "ée");
                    document.Close(true);



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'impression " + ex.Message);
                }

            }

        }

        void TextChanged(object Sender, TextChangedEventArgs e)//Changement du champ de recherche produit
        {
            TextBox o = (TextBox)Sender;
            _connexion.UpdateProduitList(o.Text);

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
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

        //Fonction de clique sur les différents bouttons
        private void ClickOnProduitOnList(object sender, MouseButtonEventArgs e)//Séléction du produit dans la liste
        {
            ListBox listBox = sender as ListBox;
            produit p = listBox.SelectedItem as produit;
            if(listBox.SelectedItem!=null)
            {
                currentProduit.Content = listBox.SelectedItem.ToString();
                _connexion.CurrentProduit = p;
            }

        }
        private void AjoutFacture(object sender, RoutedEventArgs e)
        {
            LigneFacture newLine = new LigneFacture(_connexion.CurrentProduit, Int32.Parse(QteBox.Text));
            bool found = false;
            for (int i = 0; i < CurrentFacture.LignesFactures.Count; i++)
            {
                if (CurrentFacture.LignesFactures[i].Produit.PRO_CODE == newLine.Produit.PRO_CODE)
                {
                    found = true;
                    //On update la quantité et le prix 
                    CurrentFacture.LignesFactures[i] = new LigneFacture(newLine.Produit, CurrentFacture.LignesFactures[i].Qte + newLine.Qte);
                    //CurrentFacture.LignesFactures[i].calculPrixTotal();
                    break;
                }
            }

            if (!found)
            {
                CurrentFacture.AddLigne(newLine);
            }
            PrixTotal.Content = "Prix Total : " + CurrentFacture.calculPrix().ToString() + " €";
        }


    }
}
