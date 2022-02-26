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
        public int NumFacture { get; set; }
        public int NumClient { get; set; }
        public DateTime DateFacture { get; set; }
        public ObservableCollection<LigneFacture> LignesFactures { get; set; }
        public Facture(int numC)
        {
            LignesFactures = new ObservableCollection<LigneFacture>();
            NumClient = numC;
            DateFacture = DateTime.Now;
        }
        public Facture(int numC, DateTime d, int numF)
        {
            NumFacture= numF;
            DateFacture = d;
            NumClient= numC;
        }
        

        
        public Facture()
        {
            LignesFactures = new ObservableCollection<LigneFacture>();
            NumClient = 1;
            DateFacture= DateTime.Now;
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

        public override string ToString()
        {
            return "Facture numéro "+ this.NumFacture +"  effectuée le :" + this.DateFacture.ToString();
        }
    }
}
