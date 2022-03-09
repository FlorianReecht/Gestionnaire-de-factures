using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HebdoProgSemaine3
{
    public class LigneFacture
    {
        public produit Produit { get; set; }
        public int Qte { get; set; }

        public double calculPrixTotal()
        {
            return Qte*Produit.PRO_PRIX;
        }
        public LigneFacture()
        {
            Produit = null;
            Qte = 0;
        }
        
        public override  string ToString()
        {
            int euroAsciicode = 0x80;
            return Produit.PRO_LIB + "   " + Qte +"  "+ calculPrixTotal()+"€";

        }
        public LigneFacture(produit p,int q)
        {
            Produit= p;
            Qte= q;
        }
        //A mettre dans connexion
        public void InsertFactureIntoBdd(int numCli)
        {
            //Insert numCli numFacture Produit.numProduit Qte 

        }
    }
}
