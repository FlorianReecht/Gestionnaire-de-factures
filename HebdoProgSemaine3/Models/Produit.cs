using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebdoProgSemaine3
{
    public enum Categorie
    {
        Alimentaire,Autre
    }
    public class produit
    {
        public int PRO_CODE { get; set; }
        public string PRO_LIB { get; set; }
        public double PRO_PRIX { get; set; }
        public ObservableCollection<LigneFacture> lignesProduit { get; set; }
        string PRO_CAT;

        public produit(int n,string  L,double P)
        {
            PRO_CODE = n;
            PRO_LIB = L;
            PRO_PRIX = P;
            PRO_CAT = "autre";
            lignesProduit= new ObservableCollection<LigneFacture>();
        }
        public override string ToString()
        {
            return PRO_CODE + ". " + PRO_LIB;
        }
        public produit(int n, string L, double P, string cat)
        {
            PRO_CODE=n;
            PRO_LIB=L;
            PRO_PRIX=P;
            PRO_CAT = cat;
            lignesProduit = new ObservableCollection<LigneFacture>();

        }
        public produit()
        {
            PRO_CODE = 0;
            PRO_CAT = "";
            PRO_PRIX = 0;
            PRO_LIB = "";
            lignesProduit = new ObservableCollection<LigneFacture>();

        }

    }

}
