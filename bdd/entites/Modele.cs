using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VéloMax.bdd
{
    public enum Ligne
    {
        VTT,
        VeloCourse,
        Classique,
        BMX
    }

    public class Modele
    {
        /* Attributs */
        public readonly int numM;

        public string nomM
        {
            get { return ControlleurRequetes.ObtenirChampString("Modele", "numM", numM, "nomM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "nomM", value); }
        }
        public string descriptionM
        {
            get { return ControlleurRequetes.ObtenirChampString("Modele", "numM", numM, "descriptionM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "descriptionM", value); }
        }
        public int tailleM
        {
            get { return ControlleurRequetes.ObtenirChampInt("Modele", "numM", numM, "tailleM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "tailleM", value); }
        }
        public Ligne ligne
        {
            get { Ligne l; Enum.TryParse(ControlleurRequetes.ObtenirChampString("Modele", "numM", numM, "ligne"), out l); return l; }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "ligne", value.ToString()); }
        }
        public int prixM
        {
            get { return ControlleurRequetes.ObtenirChampInt("Modele", "numM", numM, "prixM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "prixM", value); }
        }
        public DateTime dateIntroM
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Modele", "numM", numM, "dateIntroM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "dateIntroM", value); }
        }
        public DateTime dateDiscM
        {
            get { return ControlleurRequetes.ObtenirChampDatetime("Modele", "numM", numM, "dateDiscM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "dateDiscM", value); }
        }
        public int quantStockM
        {
            get { return ControlleurRequetes.ObtenirChampInt("Modele", "numM", numM, "quantStockM"); }
            set { ControlleurRequetes.ModifierChamp("Modele", "numM", numM, "quantStockM", value); }
        }
        public ReadOnlyCollection<CompositionModele> composition
        {
            get => CompositionModele.Lister(numM);
        }


        /* Instantiation */
        public Modele(int numM)
        {
            this.numM = numM;
        }
        public Modele(string nomM, string descriptionM, int tailleM, Ligne ligne, int prixM, DateTime dateIntroM, DateTime dateDiscM, int quantStockM)
        {
            ControlleurRequetes.Inserer($"INSERT INTO Modele (nomM, descriptionM, tailleM, ligne, prixM, dateIntroM, dateDiscM, quantStockM) VALUES ('{nomM}', '{descriptionM}', {tailleM}, '{ligne.ToString()}', {prixM}, '{dateIntroM.ToString("yyyy-MM-dd HH:mm:ss")}', '{dateDiscM.ToString("yyyy-MM-dd HH:mm:ss")}', {quantStockM})");
            this.numM = ControlleurRequetes.DernierIDUtilise();
        }

        /* Suppression */
        public void Supprimer()
        {
            ControlleurRequetes.SupprimerElement("Modele", "numM", numM);
        }

        /* Liste */
        public static ReadOnlyCollection<Modele> Lister()
        {
            List<Modele> list = new List<Modele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT numM FROM Modele", (MySqlDataReader reader) => { list.Add(new Modele(reader.GetInt32("numM"))); });
            return new ReadOnlyCollection<Modele>(list);
        }

        /* Commander */
        public int PrixCommande()
        {
            int prix_total = 0;
            foreach (CompositionModele cm in CompositionModele.Lister(this))
            {
                CatalFournisseur piece_moins_cher = cm.piece.PieceMoinsCher();
                if (piece_moins_cher != null)
                {
                    prix_total += piece_moins_cher.prixPieceF * cm.quant;
                }
            }
            return prix_total;
        }
        public int TempsCommande()
        {
            int temps_max = 0;
            foreach (CompositionModele cm in CompositionModele.Lister(this))
            {
                CatalFournisseur piece_moins_cher = cm.piece.PieceMoinsCher();
                if (piece_moins_cher != null && piece_moins_cher.delaiF > temps_max)
                {
                    temps_max = piece_moins_cher.delaiF;
                }
            }
            return temps_max;
        }

        /* Autojointure */
        public ReadOnlyCollection<Modele> ModelesSimilaires()
        {
            List<Modele> list = new List<Modele>();
            ControlleurRequetes.SelectionnePlusieurs($"SELECT m2.numM FROM Modele AS m1 JOIN Modele AS m2 ON (m1.ligne = m2.ligne) WHERE m1.numM={numM} AND m1.numM<>m2.numM", (MySqlDataReader reader) => { list.Add(new Modele(reader.GetInt32("numM"))); });
            return new ReadOnlyCollection<Modele>(list);
        }
    }
}
