using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace VéloMax
{
    class Commande : INotifyPropertyChanged
    {
        public int numC { get; set; }

        public string numCString { get { return numC.ToString(); } }
        public int numA { get; set; }

        public string numAString { get { return numA.ToString(); } }
        public DateTime dateC { get; set; }
        public string dateCString { get { return dateC.ToString(); } }
        public DateTime dateL { get; set; }
        public string dateLString { get { return dateL.ToString(); } }
        public bool livree { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ObservableCollection<Commande> GetCommandesEnCours(string connectionString)
        {
            const string GetCommandesQuery = "select * from commande where dateL>=@date;";

            var commandes = new ObservableCollection<Commande>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(GetCommandesQuery, conn);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var commande = new Commande();
                                    commande.numC = reader.GetInt32(0);
                                    commande.numA = reader.GetInt32(1);
                                    commande.dateC = reader.GetDateTime(2);
                                    commande.dateL = reader.GetDateTime(3);
                                    commande.livree = false;
                                    commandes.Add(commande);

                                }
                            }
                        }
                    }
                }
                return commandes;
            }
            catch (Exception eSql)
            {
                Console.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
