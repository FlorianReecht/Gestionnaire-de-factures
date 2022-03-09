using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebdoProgSemaine3
{
    public class Client
    {
        public int CLI_CODE { get; set; }
        public string CLI_NOM { get; set; }
        public string CLI_PRENOM { get; set; }
        public string CLI_ADR { get; set; }
        public string CLI_COMP { get; set; }
        public int CLI_CP { get; set; }
        public string CLI_VILLE { get; set; }
        public string TEL { get; set; }


        public Client(int n,string no,string p)
        {
            CLI_CODE = n;
            CLI_NOM = no;
            CLI_PRENOM = p;
            CLI_ADR = "";
            CLI_COMP = "";
            CLI_CP = 0;
            CLI_VILLE = "";
            TEL ="";
           
        }
        public Client()
        {

        }
        public Client(string no,string p)
        {
            CLI_NOM = no;
            CLI_PRENOM=p;
        }
        public Client(int n,string no,string p,string a,string comp,int cp,string v,string tel)
        {
            CLI_CODE = n;
            CLI_NOM = no;
            CLI_PRENOM = p;
            CLI_ADR = a;
            CLI_COMP = comp;
            CLI_CP = cp;
            CLI_VILLE = v;
            TEL = tel;
        }
        //Affichage dans la liste
        public override string ToString()
        {
            return CLI_CODE+ "." + CLI_NOM + " " + CLI_PRENOM;
        }

    }
}
