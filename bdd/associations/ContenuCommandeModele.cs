using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public class ContenuCommandeModele
    {
        /* Attributs */
        public readonly int numC;
        public readonly int numM;

        public Commande commande
        {
            get => new Commande(numC);
        }
        public Modele modele
        {
            get => new Modele(numM);
        }
        public int quantModeleC
        {
            get { return ControlleurRequetes.ObtenirChampInt("ContenuCommandeModele", "numC", numC, "numM", numM, "quantModeleC"); }
            set { ControlleurRequetes.ModifierChamp("ContenuCommandeModele", "numC", numM, "numM", numM, "quantModeleC", value); }
        }

        /* Instantiation */
        public ContenuCommandeModele(int numC, int numM)
        {
            this.numC = numC;
            this.numM = numM;
        }
        public ContenuCommandeModele(Commande commande, Modele modele): this(commande.numC, modele.numM)
        {
        }
        public ContenuCommandeModele(int numC, int numM, int quantModeleC)
        {
            ControlleurRequetes.Inserer($"INSERT INTO ContenuCommandeModele (numC, numM, quantModeleC) VALUES ({numC}, {numM}, {quantModeleC})");
            this.numC = numC;
            this.numM = numM;
        }
        public ContenuCommandeModele(Commande commande, Modele modele, int quantModeleC) : this(commande.numC, modele.numM, quantModeleC)
        {
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("ContenuCommandeModele", "numC", numC, "numM", numM);
        }

        /* Liste */
        public static ReadOnlyCollection<ContenuCommandeModele> Lister(int numC)
        {
            List<ContenuCommandeModele> list = new List<ContenuCommandeModele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numM FROM ContenuCommandeModele WHERE numC={numC}", (MySqlDataReader reader) => { list.Add(new ContenuCommandeModele(numC, reader.GetInt32("numM"))); });
            return new ReadOnlyCollection<ContenuCommandeModele>(list);
        }
        public static ReadOnlyCollection<ContenuCommandeModele> Lister(Commande commande)
        {
            return Lister(commande.numC);
        }
    }
}
