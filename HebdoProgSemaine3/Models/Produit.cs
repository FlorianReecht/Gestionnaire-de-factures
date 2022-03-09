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
    public class produit
    {
        public int PRO_CODE { get; set; }
        public string PRO_LIB { get; set; }
        public double PRO_PRIX { get; set; }
        Categorie PRO_CAT;

        public produit(int n,string  L,double P)
        {
            PRO_CODE = n;
            PRO_LIB = L;
            PRO_PRIX = P;
            PRO_CAT = Categorie.Autre;
        }
        public override string ToString()
        {
            return PRO_CODE + ". " + PRO_LIB;
        }

    }

}
