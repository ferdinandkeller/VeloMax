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
        public string numAString { get { return numA.ToString(); } }
        public string telB { get; set; }
        public string mailB { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ObservableCollection<Boutique> GetBoutiques(string connectionString)
        {
            const string GetBoutiquesQuery = "select * from boutique;";

            var boutiques = new ObservableCollection<Boutique>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetBoutiquesQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var boutique = new Boutique();
                                    boutique.numB = reader.GetInt32(0);
                                    boutique.nomB = reader.GetString(1);
                                    boutique.numA = reader.GetInt32(2);
                                    boutique.telB = reader.GetString(3);
                                    boutique.mailB = reader.GetString(4);
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
