using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace VéloMax.bdd
{
    public class Individu
    {
        /* Attributs */
        public readonly int numI;

        public string nomI
        {
            get => ControlleurRequetes.ObtenirChampString("Individu", "numI", numI, "nomI");
            set { ControlleurRequetes.ModifierChamp("Individu", "numI", numI, "nomI", value); }
        }
        public string prenomI
        {
            get => ControlleurRequetes.ObtenirChampString("Individu", "numI", numI, "prenomI");
            set { ControlleurRequetes.ModifierChamp("Individu", "numI", numI, "prenomI", value); }
        }
        public int numA
        {
            get => ControlleurRequetes.ObtenirChampInt("Individu", "numI", numI, "numA");
            set { ControlleurRequetes.ModifierChamp("Individu", "numI", numI, "numA", value); }
        }
        public Adresse adresse
        {
            get => new Adresse(numA);
        }
        public string telI
        {
            get => ControlleurRequetes.ObtenirChampString("Individu", "numI", numI, "telI");
            set { ControlleurRequetes.ModifierChamp("Individu", "numI", numI, "telI", value); }
        }
        public string mailI
        {
            get => ControlleurRequetes.ObtenirChampString("Individu", "numI", numI, "mailI");
            set { ControlleurRequetes.ModifierChamp("Individu", "numI", numI, "mailI", value); }
        }
        public bool estMembreFidelio
        {
            get
            {
                int res = 0;
                string req = $"SELECT COUNT(*) FROM Fidelio WHERE numI={numI} AND ";
                SelectionneUn(req, (MySqlDataReader reader) => { res = reader.GetInt32("COUNT(*)"); });
                return res == 0;
            }
        }
        public Fidelio fidelio
        {
            get => new Fidelio(numI);
        }

        /* Instantiation */
        public Individu(int numI)
        {
            this.numI = numI;
        }

        public Individu(string nomI, string prenomI, int numA, string telI, string mailI)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Individu (nomI, prenomI, numA, telI, mailI) VALUES ('{nomI}', '{prenomI}', {numA}, '{telI}', '{mailI}')");
            this.numI = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            new Adresse(numA).Supprimer();
            ControlleurRequetes.SupprimerElement("Individu", "numI", numI);
        }

        /* Liste */
        public static ReadOnlyCollection<Individu> Lister()
        {
            List<Individu> list = new List<Individu>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu", (MySqlDataReader reader) => { list.Add(new Individu(reader.GetInt32("numI"))); });
            return new ReadOnlyCollection<Individu>(list);
        }
        
        public static ReadOnlyCollection<Individu> ListerFidelio()
        {
            List<Individu> list = new List<Individu>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu NATURAL JOIN Fidelio", (MySqlDataReader reader) => { list.Add(new Individu(reader.GetInt32("numI"))); });
            return new ReadOnlyCollection<Individu>(list);
        }

        public string VersJson()
        {
            ControlleurJson cjson = new ControlleurJson();
            cjson.AjouterNormalChamp("nomI", nomI);
            cjson.AjouterNormalChamp("prenomI", prenomI);
            cjson.AjouterNormalJson("adresse", adresse.VersJson());
            cjson.AjouterNormalChamp("telI", telI);
            cjson.AjouterNormalChamp("mailI", mailI);
            return cjson.json_normal;
        }

        /*public static string FidelioFinJSON()
        {
            List<Individu> list = new List<Individu>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numI FROM Individu NATURAL JOIN Fidelio", (MySqlDataReader reader) => { list.Add(new Individu(reader.GetInt32("numI"))); });

            string json = "[";
            foreach (Individu i in list)
            {
                Adresse a = i.adresse;
                json += $"{{\"nomI\": \"{i.nomI}\", \"prenomI\": \"{i.prenomI}\", \"adresse\": {{\"rue\": \"{a.rue}\", \"ville\": \"{ville}\", \"codepostal\": \"{}\"";
            }
            return json + "]";
        }*/
    }
}
