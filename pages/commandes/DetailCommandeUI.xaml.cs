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
    public sealed partial class DetailCommandeUI : Page
    {
        Commande c;

        public DetailCommandeUI()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            c = new Commande((int)e.Parameter);
            string t = "PIECES :\n";
            foreach (ContenuCommandePiece ccp in ContenuCommandePiece.Lister(c))
            {
                t += $"[{ccp.numP}] x{ccp.quantPieceC}\n";
            }
            t += "\nMODELES :\n";
            foreach (ContenuCommandeModele ccm in ContenuCommandeModele.Lister(c))
            {
                t += $"[{ccm.numM}] {ccm.modele.nomM} x{ccm.quantModeleC}\n";
            }
            Debug.WriteLine(t);
            Content.Text = t;
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(CommandesEncoursUI));
        }
    }
}
