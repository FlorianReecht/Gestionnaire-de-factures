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
            currentClientLabel.Content = connexion._currentClient.Nom + " " + connexion._currentClient.Prenom;
            connexion.ClearAllLists();
            connexion.Fill_Facture_List(connexion._currentClient);

        }


    }
}
