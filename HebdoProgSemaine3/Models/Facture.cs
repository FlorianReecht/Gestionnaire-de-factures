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
        public int FAC_NUM { get; set; }
        public int CLI_NUM { get; set; }
        public DateTime FAC_DATE { get; set; }
        public ObservableCollection<LigneFacture> LignesFactures { get; set; }
        public Facture(int numC)
        {
            LignesFactures = new ObservableCollection<LigneFacture>();
            CLI_NUM = numC;
            FAC_DATE = DateTime.Now;
        }
        public Facture(int numC, DateTime d, int numF)
        {
            FAC_NUM= numF;
            FAC_DATE = d;
            CLI_NUM= numC;
        }
        public Facture()
        {
            CLI_NUM = 0;
            FAC_NUM = 0;
            FAC_DATE= DateTime.Now;
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

        public override string ToString()
        {
            return "Facture numéro "+ this.FAC_NUM +"  effectuée le :" + this.FAC_DATE.ToString();
        }
    }
}
