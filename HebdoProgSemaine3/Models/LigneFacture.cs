using HebdoProgSemaine3.Models;
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
        public Facture facture { get; set; }
        public produit Produit { get; set; }
        public int LIG_FACT { get; set; }
        public int LIG_PROD { get; set; } //numéro du produit associé à la facture
        public int LIG_QTE { get; set; }


        public double calculPrixTotal()
        {
            return LIG_QTE*Produit.PRO_PRIX;
        }
        public LigneFacture()
        {
            facture = new Facture();
            Produit = new produit();
            LIG_QTE = 0;
            LIG_PROD = Produit.PRO_CODE;
            LIG_FACT = facture.FAC_NUM;
        }
        
        public override  string ToString()
        {
            string libProduit;
            double  prixTot;
            using (var db = new HebdoChallDbContext()) 
            {
                libProduit = db.Produit.Where(p => p.PRO_CODE == LIG_PROD).Select(p => p.PRO_LIB).SingleOrDefault().ToString();
                produit p = db.Produit.Where(p => p.PRO_CODE == LIG_PROD).SingleOrDefault();
                prixTot = p.PRO_PRIX * LIG_QTE;
            }
            int euroAsciicode = 0x80;
            return  LIG_QTE +"  * " +libProduit  + "Prix Ligne :  " + prixTot +"€";

        }
        public LigneFacture(produit p,int q)
        {
            Produit= p;
            LIG_QTE= q;
            facture = new Facture();
            LIG_FACT = facture.FAC_NUM;

        }
    }
}
