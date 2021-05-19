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

namespace VéloMax.pages
{
    public sealed partial class AjouterModeleUI : Page
    {
        
        public List<ComP> pieces = new List<ComP>();
        public List<ComM> modeles = new List<ComM>();
        public string content = "";

        public AjouterModeleUI()
        {
            this.InitializeComponent();
            ligneM.ItemsSource = ConvertisseurLigneModel.LigneVersListeString();
            dateIntroM.SelectedDate = DateTimeOffset.Now;
            dateDiscM.SelectedDate = DateTimeOffset.Now.AddYears(10);
            dateDiscM.MinYear = DateTimeOffset.Now;
            refPiecesCombo.ItemsSource = Piece.ListerString();
        }

        public void Ajouter_Modele(object sender, RoutedEventArgs e)
        {
            try {
                DateTime dateI = new DateTime(dateIntroM.SelectedDate.Value.Year, dateIntroM.SelectedDate.Value.Month, dateIntroM.SelectedDate.Value.Day);
                DateTime dateD = new DateTime(dateDiscM.SelectedDate.Value.Year, dateDiscM.SelectedDate.Value.Month, dateDiscM.SelectedDate.Value.Day);
                Modele mod = new Modele(nomM.Text, descriptionM.Text, int.Parse(tailleM.Text), ConvertisseurLigneModel.LigneVersListe()[ligneM.SelectedIndex], int.Parse(prixM.Text),dateI,dateD, int.Parse(quantStockM.Text));
                foreach (ComP cp in pieces)
                {
                    new CompositionModele(mod, cp.p, cp.q);
                }
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(ModelesUI));
            } catch { }
        }

        public void AfficherContenuModele()
        {
            string t = "PIECES :\n";
            foreach (ComP cp in pieces)
            {
                t += $"[{cp.p.numP}] x{cp.q} ({cp.p.prixP* cp.q}€)\n";
            }
            Content.Text = t;
        }

        public void AjoutPieceModele(object sender, RoutedEventArgs e)
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
                AfficherContenuModele();
            }
            catch { }
        }


    }
}
