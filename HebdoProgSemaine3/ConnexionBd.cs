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
using HebdoProgSemaine3.Models;

namespace HebdoProgSemaine3
{
   
    public class ConnexionBd //Classe Etat Back de l'application qui permet la connexion à la base de données et de gérer les clients/ produits... 
    {

        //Nouvelle version du code disparition des attributs statiques apparition de champs currentFacture currentProduit et currentClient 
        //Connexion Bd dans chacune des vues.

        public static ObservableCollection<Client> ClientList = new ObservableCollection<Client>();
        public static ObservableCollection<produit> ProductList = new ObservableCollection<produit>();
        public static ObservableCollection<LigneFacture> CurrentFacture = new ObservableCollection<LigneFacture>();
        public static ObservableCollection<Facture> FactureList = new ObservableCollection<Facture>();
       
        List<String> config = new List<String>(); //Lecture dans le fichier config.xml en chemin absolu changer pour le chemin relatif

      
        public produit CurrentProduit { get; set; }
        public Client _currentClient { get; set; }
        public void ClearAllLists()
        {
            ClientList.Clear();
            ProductList.Clear();
            FactureList.Clear();
           
        }

        //Association entre l'id du produit, la quantité et le prix.
        public void FillClientListButWithEFCore()
        {
            using(var db = new HebdoChallDbContext())
            {
                foreach(var client in db.Client)
                {
                    ClientList.Add(client);
                }
            }
        }
        public void AddClientToListWithFCore(Client cli)
        {
            using(var db= new HebdoChallDbContext())
            {
                db.Client.Add(cli);
                db.SaveChanges();
            }
        }
        public void DeleteClientFromList(Client cli)
        {
            using(var db=new HebdoChallDbContext())
            {
                db.Client.Remove(cli);
                db.SaveChanges();
                ClientList.Remove(cli);

            }
        }
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
                    produit p = new produit((int)reader["PRO_CODE"], reader["PRO_LIB"].ToString(), (double)reader["PRO_PRIX"]);
                    ProductList.Add(p);
                }
                c.Close ();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error while fill list produit :" + ex.Message);
            }


        }
        public void Fill_Facture_List(Client c)
        {
            String request = "SELECT * FROM hebdochall3.facture WHERE CLI_NUM = @idCli ";
            try
            {
                MySqlConnection co=Connect();
                MySqlCommand cmd = new MySqlCommand(request, co);
                MySqlParameter id = new MySqlParameter("@idCli", MySqlDbType.Int32);
                id.Value = c.CLI_CODE;
                cmd.Parameters.Add(id);
                cmd.Prepare();
                MySqlDataReader reader=cmd.ExecuteReader();
                while (reader.Read())
                {
                    int num = (int)reader["FAC_NUM"];
                    DateTime date = (DateTime)reader["FAC_DATE"];
                    Facture f=new Facture(_currentClient.CLI_CODE,date, num);
                    ConnexionBd.FactureList.Add(f);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while filling list facture :" + ex.Message);
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
                        produit p = new produit((int)reader["PRO_CODE"], reader["PRO_LIB"].ToString(), (double)reader["PRO_PRIX"]);
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
        
        
        public void InsertFactureIntoBdd(Client Selectedclient ,Facture facture)//Insert fonctionne Inserer les lignes facture correspondantes.
        {
            //Insertion de la facture
            //Récupération du dernier ID
            //Insertion des  lignes à partir du dernier ID.
            try
            {


                string reqtInsertFacture = "INSERT INTO hebdochall3.facture (FAC_DATE,CLI_NUM) VALUES (@date,@numCli)";
                string reqInsertLigneFacture = "INSERT INTO hebdochall3.ligne (LIG_FACT,LIG_PROD,LIG_QTE) VALUES (@numFacture,@numproduit,@quantitep)";
                string requestLastInsert = "SELECT LAST_INSERT_ID() ";
                
               
                
                MySqlConnection conn = Connect();
                MySqlCommand cmd = new MySqlCommand(reqtInsertFacture, conn);
                MySqlCommand cmd2= new MySqlCommand(reqInsertLigneFacture, conn);
                MySqlCommand cmd3 = new MySqlCommand(requestLastInsert, conn);
                MySqlParameter datep = new MySqlParameter("@date", MySqlDbType.Date);
                MySqlParameter numC = new MySqlParameter("@numCli", MySqlDbType.Int32);
                MySqlParameter numFacture = new MySqlParameter("@numFacture", MySqlDbType.Int32);
                MySqlParameter numProduit = new MySqlParameter("@numproduit", MySqlDbType.Int32);
                MySqlParameter qte = new MySqlParameter("@quantitep", MySqlDbType.Int32);
                cmd2.Parameters.Add(numFacture);
                cmd2.Parameters.Add(qte);
                cmd2.Parameters.Add(numProduit);
                numC.Value = _currentClient.CLI_CODE;//Attribut statique pas la meilleure solution.
                datep.Value = facture.DateFacture;
                cmd.Parameters.Add(datep);
                cmd.Parameters.Add(numC);
                cmd.Prepare();
                var query = cmd.ExecuteNonQuery();
                var lastIndex = cmd3.ExecuteScalar();
                ///MessageBox.Show("Last insert id   : " + lastIndex.ToString());
                if (query != 0)
                {
                    MessageBox.Show("Insertion Facture validée");
                }
                foreach (LigneFacture ligneFacture in facture.LignesFactures)
                {
    
                    numFacture.Value = lastIndex;
                    qte.Value = ligneFacture.Qte;
                    numProduit.Value =ligneFacture.Produit.PRO_CODE;

                    cmd2.Prepare();
                    var query2= cmd2.ExecuteNonQuery();
                    MessageBox.Show("Insertion ligne réussie");
                }

                
         
            }
            catch(Exception ex)
            {
                MessageBox.Show("error While Insert facture"+ ex.Message);
            }
            

        }
        public void InsertClientIntoBdd(Client c)//Ajoute le client dans la base
        {
            string requestLastInsert = "SELECT LAST_INSERT_ID() ";
            string reqInsertClient = "INSERT INTO hebdochall3.client (CLI_CODE,CLI_NOM,CLI_PRENOM,CLI_ADR,CLI_COMP,CLI_CP,CLI_VILLE,TEL) VALUES (@numclient,@nom,@prenom,@adresse,@comp,@cp,@ville,@tel)";
            MySqlConnection conn = Connect();
            MySqlCommand cmd = new MySqlCommand(requestLastInsert, conn);
            MySqlCommand cmd2 = new MySqlCommand(reqInsertClient, conn);
            MySqlParameter num= new MySqlParameter("@numclient",MySqlDbType.Int32);
            num.Value=cmd.ExecuteScalar();
            cmd2.Parameters.Add(num);
            MySqlParameter nom = new MySqlParameter("@nom", MySqlDbType.VarChar, 50);
            MySqlParameter prenom = new MySqlParameter("@prenom", MySqlDbType.VarChar, 50);
            MySqlParameter adresse = new MySqlParameter("@adresse", MySqlDbType.VarChar, 50);
            MySqlParameter comp = new MySqlParameter("@comp", MySqlDbType.VarChar, 50);
            MySqlParameter ville = new MySqlParameter("@ville", MySqlDbType.VarChar, 50);
            MySqlParameter tel = new MySqlParameter("@tel", MySqlDbType.VarChar, 50);
            MySqlParameter cp = new MySqlParameter("@cp", MySqlDbType.Int32);
            nom.Value = c.CLI_NOM.ToString();
            adresse.Value = "testAdresse";
            comp.Value = "testComp";
            ville.Value = "testVille";
            tel.Value = "testTel";
            cp.Value = 51500;
            prenom.Value = c.CLI_PRENOM.ToString();
            cmd2.Parameters.Add(nom);
            cmd2.Parameters.Add(prenom);
            cmd2.Parameters.Add(adresse);
            cmd2.Parameters.Add(comp);
            cmd2.Parameters.Add(cp);
            cmd2.Parameters.Add(ville);
            cmd2.Parameters.Add(tel);
            cmd2.Prepare();
            var query=cmd2.ExecuteNonQuery();
            if (query != 0)
            {
                MessageBox.Show("Insertion client effectuée");
            }
            else
            {
                MessageBox.Show("Erreur pdt l'insertion client");
            }



        }


    }
}
