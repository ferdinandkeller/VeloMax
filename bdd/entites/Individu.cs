using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Individu
    {
        /* Attributs */
        public readonly int numI;

        public string nomI
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numI", numI, "nomI");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numI", numI, "nomI", value); }
        }
        public string prenomI
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numI", numI, "prenomI");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numI", numI, "prenomI", value); }
        }
        public int numA
        {
            get => ControlleurRequetes.ObtenirChampInt("Boutique", "numI", numI, "numA");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numI", numI, "numA", value); }
        }
        public string telI
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numI", numI, "telI");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numI", numI, "telI", value); }
        }
        public string mailI
        {
            get => ControlleurRequetes.ObtenirChampString("Boutique", "numI", numI, "mailI");
            set { ControlleurRequetes.ModifierChamp("Boutique", "numI", numI, "mailI", value); }
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
    }
}
