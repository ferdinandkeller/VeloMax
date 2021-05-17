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

namespace VéloMax.pages
{
    public sealed partial class FournisseursUIMain : Page
    {
        public FournisseursUIMain()
        {
            this.InitializeComponent();
            MyDataGrid.ItemsSource = Fournisseur.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterFournisseurUI));
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            Fournisseur f = (Fournisseur)MyDataGrid.SelectedItem;
            if (f != null)
            {
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(CatalogueUI), f);
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Fournisseur)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(FournisseursUIMain));
        }
    }
}
