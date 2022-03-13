using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour VoirFacturesVue.xaml
    /// </summary>
    public partial class VoirFacturesVue : UserControl
    {
        ConnexionBd connexion;
        public VoirFacturesVue(ConnexionBd co)
        {
            connexion = co;
            InitializeComponent();
            ListFactureClient.ItemsSource = ConnexionBd.FactureList;
            LigneFactureOfFacture.ItemsSource = ConnexionBd.currentViewFacture;

            currentClientLabel.Content = connexion._currentClient.CLI_NOM + " " + connexion._currentClient.CLI_PRENOM;
            connexion.ClearAllLists();
            connexion.FillListFactureButWithEfCore(connexion._currentClient);

        }

        private void ChangeFactureEvent(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = sender as ListBox;
            if(box.SelectedItem!=null)
            {
                ConnexionBd.currentViewFacture.Clear();
                Facture f = box.SelectedItem as Facture;
                connexion.FillLignesFactureFromFacture(f);
                MessageBox.Show(ConnexionBd.currentViewFacture.Count.ToString());


            }

        }
    }
}
