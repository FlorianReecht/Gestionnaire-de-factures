using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace HebdoProgSemaine3
{
    public class ConnexionBd
    {
        public static ObservableCollection<string> ClientList = new ObservableCollection<string>();
        public static ObservableCollection<string> ProductList = new ObservableCollection<string>();
        public static ObservableCollection<TextBox> QteBoxList = new ObservableCollection<TextBox>();//Box contenant les quantitées , calcul de cout.
        public static List<(int,string, double)> AssociationQuantitéPrix = new List<(int,string, double)>();
        List<String> config = new List<String>();
        //Association entre l'id du produit, la quantité et le prix.

        public MySqlConnection Connect()
        {
            string path = "C:/Users/reflo/source/repos/HebdoProgSemaine3/HebdoProgSemaine3/Config.xml";
            XDocument doc = XDocument.Load(path);
            try
            {
                IEnumerable<XElement> node = doc.Element("bdConfig").Elements();
                foreach (XElement element in node)
                {
                    config.Add(element.Value.ToString());
                }

                MySqlConnection connection = new MySqlConnection();
               
                connection.ConnectionString = "datasource="+config[0]+";port="+config[1]+";username="+config[2]+";password="+config[3]+";";
                connection.Open();
                return connection;


            }
            catch (Exception ex)
            {
                MessageBox.Show("error :" + ex.Message);
                return null;
            }

        }
        public void Fill_Client_List()
        {
            String request = "SELECT * FROM hebdochall3.client";
            try
            {
                MySqlConnection c = Connect();
                MySqlCommand c2 = new MySqlCommand(request, c);
                MySqlDataReader reader = c2.ExecuteReader();
                while (reader.Read())
                {
                    string chaine = String.Format("{0},{1}", reader["CLI_NOM"], reader["CLI_PRENOM"]);
                    ClientList.Add(chaine);
                }
                c.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error while select :" + ex.Message);
            }
            
        }
        public void Fill_Produit_List()
        {
            String request = "SELECT * FROM hebdochall3.produit";
            try
            {
                MySqlConnection c = Connect();
                MySqlCommand c2 = new MySqlCommand(request, c);
                MySqlDataReader reader = c2.ExecuteReader();
                while (reader.Read())
                {
                    string chaine = String.Format("{0}", reader["PRO_LIB"]);
                    ProductList.Add(chaine);
                    TextBox t=new TextBox();
                    t.Text="0";
                    QteBoxList.Add(t);


                }
                c.Close ();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error while fill list produit :" + ex.Message);
            }


        }
    }
}
