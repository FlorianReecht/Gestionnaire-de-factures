using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebdoProgSemaine3
{
    public class Client
    {
        public int NumCli { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Client(int n,string no,string p)
        {
            NumCli = n;
            Nom = no;
            Prenom = p;
        }
        //Affichage dans la liste
        public override string ToString()
        {
            return NumCli+ "." + Nom + " " + Prenom;
        }

    }
}
