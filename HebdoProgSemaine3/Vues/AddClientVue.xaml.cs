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
    /// Logique d'interaction pour AddClientVue.xaml
    /// </summary>
    public partial class AddClientVue : UserControl
    {
        ConnexionBd _connexion;
        public AddClientVue(ConnexionBd co)
        {
            InitializeComponent();
            _connexion = co;
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            Client addingCLi= new Client(0,nomCli.Text.ToString(),PrenomCli.Text.ToString(),"test adresse","test complément",51000,"test ville","testtel");
            _connexion.AddClientToListWithFCore(addingCLi);

        }
    }
}
