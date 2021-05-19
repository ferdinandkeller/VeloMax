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
using VéloMax.bdd;
using System.Diagnostics;
using VéloMax.pages;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VéloMax.pages
{
    public class ComP
    {
        public Piece p;
        public int q;
        public ComP(Piece p, int q)
        {
            this.p = p;
            this.q = q;
        }
    }
    public class ComM
    {
        public Modele m;
        public int q;
        public ComM(Modele m, int q)
        {
            this.m = m;
            this.q = q;
        }
    }

    public sealed partial class AjouterCommandeUI : Page
    {
        public List<ComP> pieces = new List<ComP>();
        public List<ComM> modeles = new List<ComM>();
        public string content = "";

        public AjouterCommandeUI()
        {
            this.InitializeComponent();

            refPiecesCombo.ItemsSource = Piece.ListerString();
            refModeleCombo.ItemsSource = Modele.ListerString();

            dateC.SelectedDate = DateTimeOffset.Now;
            dateL.SelectedDate = DateTimeOffset.Now;
            dateC.MinYear = DateTimeOffset.Now;
            dateL.MinYear = DateTimeOffset.Now;
        }

        public void AfficherContenuCommande()
        {
            float tot = 0;
            float rabais = 0;
            string t = "PIECES :\n";
            foreach (ComP cp in pieces)
            {
                t += $"[{cp.p.numP}] x{cp.q} ({cp.p.prixP*cp.q}€)\n";
                tot += cp.p.prixP * cp.q;
            }
            t += "\nMODELES :\n";
            foreach (ComM cm in modeles)
            {
                t += $"[{cm.m.numM}] {cm.m.nomM} x{cm.q} ({cm.m.prixM * cm.q}€)\n";
                tot += cm.m.prixM * cm.q;
            }
            t += "\nTOTAL :\n";
            t += $"{tot} €";
            if (ClientCombo.SelectedIndex == 0)
            {
                Individu ind = Individu.Lister()[AdaptableCombo.SelectedIndex];
                rabais = ind.fidelio.programme.rabais;
                if (rabais == -1) { rabais = 0; }
            }
            else if (ClientCombo.SelectedIndex == 1)
            {
                Boutique bout = Boutique.Lister()[AdaptableCombo.SelectedIndex];
                rabais = float.Parse(bout.remise);
            }
            t += $"\n-> REMISE : {rabais}%";
            float totr = tot * (1 - (rabais / 100));
            t += "\nTOTAL FINAL :\n";
            t += $"{totr} €";
            Debug.WriteLine(t);
            Content.Text = t;
        }

        public void Type_Client_Change(object sender, SelectionChangedEventArgs e)
        {
            pieces = new List<ComP>();
            modeles = new List<ComM>();
            Content.Text = "";
            if (e.AddedItems[0].ToString() == "Particulier")
            {
                AdaptableCombo.ItemsSource = Individu.ListerString();
            }
            else if (e.AddedItems[0].ToString() == "Boutique")
            {
                AdaptableCombo.ItemsSource = Boutique.ListerString();
            }
        }

        public void AjoutPieceCommande(object sender, RoutedEventArgs e)
        {
            try
            {
                Piece p = Piece.Lister()[refPiecesCombo.SelectedIndex];
                int quant = int.Parse(quantiteP.Text);
                bool edited = false;
                foreach (ComP cp in pieces)
                {
                    if (cp.p.numP == p.numP)
                    {
                        cp.q += quant;
                        edited = true;
                        break;
                    }
                }
                if (!edited)
                {
                    pieces.Add(new ComP(p, quant));
                }
                AfficherContenuCommande();
            }
            catch { }
        }

        public void AjoutModeleCommande(object sender, RoutedEventArgs e)
        {
            try
            {
                Modele m = Modele.Lister()[refModeleCombo.SelectedIndex];
                int quant = int.Parse(quantiteM.Text);
                bool edited = false;
                foreach (ComM cm in modeles)
                {
                    if (cm.m.numM == m.numM)
                    {
                        cm.q += quant;
                        edited = true;
                        break;
                    }
                }
                if (!edited)
                {
                    modeles.Add(new ComM(m, quant));
                }
                AfficherContenuCommande();
            }
            catch { }
        }

        private void AjoutCommande(object sender, RoutedEventArgs e)
       {
            DateTime dateCC = new DateTime(dateC.SelectedDate.Value.Year, dateC.SelectedDate.Value.Month, dateC.SelectedDate.Value.Day);
            DateTime dateLL = new DateTime(dateL.SelectedDate.Value.Year, dateL.SelectedDate.Value.Month, dateL.SelectedDate.Value.Day);
            if (ClientCombo.SelectedIndex == 0)
            {
                Individu ind = Individu.Lister()[AdaptableCombo.SelectedIndex];

                Commande com = new Commande(ind.adresse, dateCC, dateLL);
                Debug.WriteLine(com.numC);
                foreach (ComP cp in pieces)
                {
                    new ContenuCommandePiece(com, cp.p, cp.q);
                }
                foreach (ComM cm in modeles)
                {
                    new ContenuCommandeModele(com, cm.m, cm.q);
                }
                new ExecuteurCommandeIndividu(com, ind);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(CommandesEncoursUI));
            }
            else if (ClientCombo.SelectedIndex == 1)
            {
                Boutique bout = Boutique.Lister()[AdaptableCombo.SelectedIndex];

                Commande c = new Commande(bout.adresse, dateCC, dateLL);

                foreach (ComP cp in pieces)
                {
                    new ContenuCommandePiece(c, cp.p, cp.q);
                }
                foreach (ComM cm in modeles)
                {
                    new ContenuCommandeModele(c, cm.m, cm.q);
                }

                new ExecuteurCommandeBoutique(c, bout);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(CommandesEncoursUI));
            }
        }
    }
}
