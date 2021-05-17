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
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using VéloMax.bdd;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class AjouterCatalUI : Page
    {
        Fournisseur f;

        public AjouterCatalUI()
        {
            this.InitializeComponent();

            pieceCombo.ItemsSource = Piece.ListerString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            f = (Fournisseur)e.Parameter;
        }

        public void AjoutCatalogue(object sender, RoutedEventArgs e)
        {
            try
            {
                Piece p = Piece.Lister()[pieceCombo.SelectedIndex];
                int id = int.Parse(numPF.Text);
                int prix = int.Parse(prixPF.Text);
                int delai = int.Parse(delaiF.Text);
                new CatalFournisseur(f, p, id, prix, delai);
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(CatalogueUI), f);
            }
            catch { }
        }
    }
}
