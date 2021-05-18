using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace VéloMax.bdd
{
    public static class ControlleurRequetes
    {
        /* Requêtes de base */
        public delegate void ExecuteurRequete(MySqlConnection conn);
        public delegate void ParseurRequete(MySqlDataReader reader);

        public static void ObtenirConnection(ExecuteurRequete query)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=root;database=VeloMax");
            conn.Open();
            query(conn);
            conn.Close();
        }

        public static void Inserer(string query)
        {
            ObtenirConnection((MySqlConnection conn) =>
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteReader().Close();
            });
        }

        public static void Modifier(string query)
        {
            ObtenirConnection((MySqlConnection conn) =>
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteReader().Close();
            });
        }

        public static void SelectionneUn(string query, ParseurRequete parser)
        {
            ObtenirConnection((MySqlConnection conn) =>
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    parser(reader);
                }
                reader.Close();
            });
        }

        public static void SelectionnePlusieurs(string query, ParseurRequete parser)
        {
            ObtenirConnection((MySqlConnection conn) =>
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    parser(reader);
                }
                reader.Close();
            });
        }

        public static int DernierIDUtilise()
        {
            int res = -1;
            SelectionneUn("SELECT LAST_INSERT_ID() AS l", (MySqlDataReader reader) =>
            {
                res = reader.GetInt32("l");
            });
            return res;
        }

        public static void Supprimer(string query)
        {
            ObtenirConnection((MySqlConnection conn) =>
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteReader().Close();
            });
        }

        /* Requêtes complexes */
        /*public static bool Existe(string req)
        {
            int result = -1;
            ControlleurRequetes.SelectionneUn(req, (MySqlDataReader reader) =>
            {
                result = reader.GetInt32("COUNT(*)");
            });
            return result > 0;
        }*/

        /*public static bool Libre(string req)
        {
            int result = -1;
            ControlleurRequetes.SelectionneUn(req, (MySqlDataReader reader) =>
            {
                result = reader.GetInt32("COUNT(*)");
            });
            return result == 0;
        }

        public static int Numero(string req)
        {
            int num = -1;
            ControlleurRequetes.SelectionneUn(req, (MySqlDataReader reader) =>
            {
                num = reader.GetInt32("num");
            });
            return num;
        }*/

        public static string ObtenirChampString(string table_name, string key_name, int key_value, string field_name)
        {
            string res = null;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_name}={key_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetString(field_name); });
            return res;
        }
        public static string ObtenirChampString(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name)
        {
            string res = null;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetString(field_name); });
            return res;
        }
        public static int ObtenirChampInt(string table_name, string key_name, int key_value, string field_name)
        {
            int res = -1;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_name}={key_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetInt32(field_name); });
            return res;
        }
        public static int ObtenirChampInt(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name)
        {
            int res = -1;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetInt32(field_name); });
            return res;
        }
        public static DateTime ObtenirChampDatetime(string table_name, string key_name, int key_value, string field_name)
        {
            DateTime res = DateTime.Now;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_name}={key_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetDateTime(field_name); });
            return res;
        }
        public static DateTime ObtenirChampDatetime(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name)
        {
            DateTime res = DateTime.Now;
            string req = $"SELECT {field_name} FROM {table_name} WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}";
            SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetDateTime(field_name); });
            return res;
        }

        public static void ModifierChamp(string table_name, string key_name, int key_value, string field_name, string field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}='{field_value.Replace("'", "''")}' WHERE {key_name}={key_value}");
        }
        public static void ModifierChamp(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name, string field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}='{field_value.Replace("'", "''")}' WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}");
        }
        public static void ModifierChamp(string table_name, string key_name, int key_value, string field_name, int field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}={field_value} WHERE {key_name}={key_value}");
        }
        public static void ModifierChamp(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name, int field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}={field_value} WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}");
        }
        public static void ModifierChamp(string table_name, string key_name, int key_value, string field_name, DateTime field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}='{field_value.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE {key_name}={key_value}");
        }
        public static void ModifierChamp(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value, string field_name, DateTime field_value)
        {
            Modifier($"UPDATE {table_name} SET {field_name}='{field_value.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}");
        }

        public static void SupprimerElement(string table_name, string key_name, int key_value)
        {
            Supprimer($"DELETE FROM {table_name} WHERE {key_name}={key_value}");
        }
        public static void SupprimerElement(string table_name, string key_1_name, int key_1_value, string key_2_name, int key_2_value)
        {
            Supprimer($"DELETE FROM {table_name} WHERE {key_1_name}={key_1_value} AND {key_2_name}={key_2_value}");
        }
    }
}
