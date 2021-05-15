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
    public class Boutique : INotifyPropertyChanged
    {
        public int numB { get; set; }

        public string numBString { get { return numB.ToString(); } }
        public string nomB { get; set; }
        public int numA { get; set; }
        public string adresse { get; set; }
        public string telB { get; set; }
        public string mailB { get; set; }
        public string remise { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ObservableCollection<Boutique> GetBoutiques(string connectionString)
        {
            const string GetBoutiquesQuery = "select * from boutique natural join adresse;";

            var boutiques = new ObservableCollection<Boutique>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(GetBoutiquesQuery, conn);
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var boutique = new Boutique();
                                    boutique.numB = reader.GetInt32(1);
                                    boutique.nomB = reader.GetString(2);
                                    boutique.numA = reader.GetInt32(0);
                                    boutique.telB = reader.GetString(3);
                                    boutique.mailB = reader.GetString(4);
                                    boutique.adresse = reader.GetString(5) + " " + reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8) + " ";
                                    boutique.remise = "0";
                                    boutiques.Add(boutique);
                                    
                                }
                            }
                        }
                    }
                }
                return boutiques;
            }
            catch (Exception eSql)
            {
                Console.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
    
}
