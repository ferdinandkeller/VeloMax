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

        public AjouterCommandeUI()
        {
            this.InitializeComponent();

            refPiecesCombo.ItemsSource = Piece.ListerString();
            refModeleCombo.ItemsSource = Modele.ListerString();
        }

        public void Type_Client_Change(object sender, SelectionChangedEventArgs e)
        {
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
                pieces.Add(new ComP(Piece.Lister()[refPiecesCombo.SelectedIndex], int.Parse(quantiteP.Text)));
            } catch { }
        }

        public void AjoutModeleCommande(object sender, RoutedEventArgs e)
        {
            try
            {
                modeles.Add(new ComM(Modele.Lister()[refPiecesCombo.SelectedIndex], int.Parse(quantiteM.Text)));
            }
            catch { }
        }

        private void AjoutCommande(object sender, RoutedEventArgs e)
        {
            if (ClientCombo.SelectedIndex == 0)
            {
                Individu ind = Individu.Lister()[AdaptableCombo.SelectedIndex];

                Commande c = new Commande(ind.adresse, DateTime.Parse(dateC.Text), DateTime.Parse(dateL.Text));

                foreach (ComP cp in pieces)
                {
                    new ContenuCommandePiece(c, cp.p, cp.q);
                }
                foreach (ComM cm in modeles)
                {
                    new ContenuCommandeModele(c, cm.m, cm.q);
                }

                new ExecuteurCommandeIndividu(c, ind);
            }
            else if (ClientCombo.SelectedIndex == 1)
            {
                Boutique bout = Boutique.Lister()[AdaptableCombo.SelectedIndex];

                Commande c = new Commande(bout.adresse, DateTime.Parse(dateC.Text), DateTime.Parse(dateL.Text));

                foreach (ComP cp in pieces)
                {
                    new ContenuCommandePiece(c, cp.p, cp.q);
                }
                foreach (ComM cm in modeles)
                {
                    new ContenuCommandeModele(c, cm.m, cm.q);
                }

                new ExecuteurCommandeBoutique(c, bout);
            }
        }
    }
}
