using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace HebdoProgSemaine3
{
    public class Facture
    {
        public ObservableCollection<LigneFacture> LignesFactures { get; set; }
        public Facture()
        {
            LignesFactures = new ObservableCollection<LigneFacture>();
        }
        public void AddLigne(LigneFacture nextLine)
        {
            LignesFactures.Add(nextLine);
        }
        int Produit { get; set; }
        public double calculPrix()
        {
            double retour=0.0;
            foreach (LigneFacture ligne in LignesFactures)
            {
                retour += ligne.calculPrixTotal();
            }
            return retour;
         
        }
    }
}
