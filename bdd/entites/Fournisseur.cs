using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class Fournisseur
    {
        /* Attributs */
        public readonly int numF;

        public int siret
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fournisseur", "numF", numF, "siret"); }
            set { ControlleurRequetes.ModifierChamp("Fournisseur", "numF", numF, "siret", value); }
        }
        public string nomF
        {
            get { return ControlleurRequetes.ObtenirChampString("Fournisseur", "numF", numF, "nomF"); }
            set { ControlleurRequetes.ModifierChamp("Fournisseur", "numF", numF, "nomF", value); }
        }
        public int numA
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fournisseur", "numF", numF, "numA"); }
            set { ControlleurRequetes.ModifierChamp("Fournisseur", "numF", numF, "numA", value); }
        }
        public Adresse adresse
        {
            get => new Adresse(numA);
            set => numA = value.numA;
        }
        public string contact
        {
            get { return ControlleurRequetes.ObtenirChampString("Fournisseur", "numF", numF, "contact"); }
            set { ControlleurRequetes.ModifierChamp("Fournisseur", "numF", numF, "contact", value); }
        }
        public int reactivite
        {
            get { return ControlleurRequetes.ObtenirChampInt("Fournisseur", "numF", numF, "reactivite"); }
            set { ControlleurRequetes.ModifierChamp("Fournisseur", "numF", numF, "reactivite", value); }
        }
        public ReadOnlyCollection<CatalFournisseur> catalogue
        {
            get => CatalFournisseur.Lister(numF);
        }

        /* Instantiation */
        public Fournisseur(int numF)
        {
            this.numF = numF;
        }

        public Fournisseur(int siret, string nomF, int numA, string contact, int reactivite)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Fournisseur (siret, nomF, numA, contact, reactivite) VALUES ({siret}, '{nomF}', {numA}, '{contact}', {reactivite})");
            this.numF = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            new Adresse(numA).Supprimer();
            ControlleurRequetes.SupprimerElement("Fournisseur", "numF", numF);
        }

        /* Liste */
        public static ReadOnlyCollection<Fournisseur> Lister()
        {
            List<Fournisseur> list = new List<Fournisseur>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numF FROM Fournisseur", (MySqlDataReader reader) => { list.Add(new Fournisseur(reader.GetInt32("numF"))); });
            return new ReadOnlyCollection<Fournisseur>(list);
        }
    }
}
