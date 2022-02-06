﻿using MySql.Data.MySqlClient;
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
   
    public class ConnexionBd //Classe Etat Back de l'application qui permet la connexion à la base de données et de gérer les clients/ produits... 
    {
        //Listes de l'application 
        //public static ObservableCollection<string> ClientList = new ObservableCollection<string>();
        //public static ObservableCollection<string> ProductList = new ObservableCollection<string>();

        public static ObservableCollection<Client> ClientList = new ObservableCollection<Client>();
        public static ObservableCollection<Produit> ProductList = new ObservableCollection<Produit>();
        public static ObservableCollection<LigneFacture> CurrentFacture = new ObservableCollection<LigneFacture>();
       
        List<String> config = new List<String>(); //Lecture dans le fichier config.xml en chemin absolu changer pour le chemin relatif

        //client choisi pour update les factures 
        public Client CurrentClient { get; set; }
        public Produit CurrentProduit { get; set; }
        
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
                    int num = (int)reader["CLI_CODE"];
                    string nom = reader["CLI_NOM"].ToString();
                    string prenom = reader["CLI_PRENOM"].ToString();
                    Client cli= new Client(num,nom, prenom);
                    ClientList.Add(cli);
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
                    Produit p = new Produit((int)reader["PRO_CODE"], reader["PRO_LIB"].ToString(), (double)reader["PRO_PRIX"]);
                    ProductList.Add(p);
                }
                c.Close ();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error while fill list produit :" + ex.Message);
            }


        }
        public void UpdateClientList(string NomOuPrenom)
        {
            ClientList.Clear();
            if (NomOuPrenom == "")
            {
                Fill_Client_List();
            }
            else
            {
                try
                {
                    MySqlConnection c = Connect();
                    MySqlCommand command = new MySqlCommand(null, c);
                    command.CommandText = "SELECT DISTINCT * FROM hebdochall3.client WHERE CLI_NOM LIKE  @Nom  OR CLI_PRENOM  LIKE @Nom  ";
                    MySqlParameter nom = new MySqlParameter("@Nom", MySqlDbType.Text, 50);
                    nom.Value = "%"+NomOuPrenom+"%";
                    command.Parameters.Add(nom);
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int num = (int)reader["CLI_CODE"];
                        string nomcli = reader["CLI_NOM"].ToString();
                        string prenomcli = reader["CLI_PRENOM"].ToString();
                        Client cli = new Client(num, nomcli, prenomcli);
                        ClientList.Add(cli);
                    }

                    c.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error while select :" + ex.Message);
                }

            }
            
        }
        public void UpdateProduitList(string nomProduit)
        {
            ProductList.Clear();
            if(nomProduit == "")
            {
                Fill_Produit_List();
            }
            else
            {
                try
                {
                    MySqlConnection c =Connect();
                    MySqlCommand command = new MySqlCommand(null, c);
                    command.CommandText = "SELECT * FROM hebdochall3.produit WHERE PRO_LIB LIKE @nomProduit";
                    MySqlParameter nom=new MySqlParameter("@nomProduit",MySqlDbType.Text, 50);
                    nom.Value="%"+nomProduit+"%";
                    command.Parameters.Add(nom);
                    command.Prepare();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Produit p = new Produit((int)reader["PRO_CODE"], reader["PRO_LIB"].ToString(), (double)reader["PRO_PRIX"]);
                        ProductList.Add(p);    
                    }
                    c.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error while select :" + ex.Message);
                }
            }
        }
    }
}
