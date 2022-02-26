using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebdoProgSemaine3
{
    enum Categorie
    {
        Alimentaire,Autre
    }
    public class Produit
    {
        public int NumProduit { get; set; }
        public string LibProduit { get; set; }
        public double Prix { get; set; }

        public Produit(int n,string  L,double P)
        {
            NumProduit = n;
            LibProduit = L;
            Prix = P;
        }
        public override string ToString()
        {
            return NumProduit + ". " + LibProduit;
        }

    }

}
