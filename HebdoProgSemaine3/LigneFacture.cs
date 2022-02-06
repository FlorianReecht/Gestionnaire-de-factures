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
        Produit Produit { get; set; }
        int Qte { get; set; }

        public double calculPrixTotal()
        {
            return Qte*Produit.Prix;
        }
        
        public override  string ToString()
        {
            int euroAsciicode = 0x80;
            return Produit.LibProduit + "   " + Qte +"  "+ calculPrixTotal()+"€";

        }
        public LigneFacture(Produit p,int q)
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
