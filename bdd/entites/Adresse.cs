using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Adresse
    {
        /* Attributs */
        public readonly int numA;

        public string rue
        {
            get { return ControlleurRequetes.ObtenirChampString("Adresse", "numA", numA, "rue"); }
            set { ControlleurRequetes.ModifierChamp("Adresse", "numA", numA, "rue", value); }
        }
        public string ville
        {
            get { return ControlleurRequetes.ObtenirChampString("Adresse", "numA", numA, "ville"); }
            set { ControlleurRequetes.ModifierChamp("Adresse", "numA", numA, "ville", value); }
        }
        public int codepostal
        {
            get { return ControlleurRequetes.ObtenirChampInt("Adresse", "numA", numA, "codepostal"); }
            set { ControlleurRequetes.ModifierChamp("Adresse", "numA", numA, "codepostal", value); }
        }
        public string province
        {
            get { return ControlleurRequetes.ObtenirChampString("Adresse", "numA", numA, "province"); }
            set { ControlleurRequetes.ModifierChamp("Adresse", "numA", numA, "province", value); }
        }

        /* Instantiation */
        public Adresse(int numA)
        {
            this.numA = numA;
        }
        public Adresse(string rue, string ville, int codepostal, string province)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Adresse (rue, ville, codepostal, province) VALUES ('{rue}', '{ville}', {codepostal}, '{province}')");
            this.numA = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Adresse", "numA", numA);
        }

        /* Liste */
        public static ReadOnlyCollection<Adresse> Lister()
        {
            List<Adresse> list = new List<Adresse>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numA FROM Adresse", (MySqlDataReader reader) => { list.Add(new Adresse(reader.GetInt32("numA"))); });
            return new ReadOnlyCollection<Adresse>(list);
        }

        /* Conversion */
        public string VersJson()
        {
            ControlleurJson cjson = new ControlleurJson();
            cjson.AjouterNormalChamp("rue", rue);
            cjson.AjouterNormalChamp("ville", ville);
            cjson.AjouterNormalChamp("codepostal", codepostal.ToString());
            cjson.AjouterNormalChamp("province", province);
            return cjson.json_normal;
        }

        public string VersString
        {
            get => $"{rue}, {codepostal} {ville}, {province}";
        }
    }
}
