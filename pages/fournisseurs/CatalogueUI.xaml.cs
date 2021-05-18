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

namespace VéloMax.pages
{
    public sealed partial class CatalogueUI : Page
    {
        Fournisseur f;

        public CatalogueUI()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            f = (Fournisseur)e.Parameter;
            MyDataGrid.ItemsSource = CatalFournisseur.Lister(f);
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterCatalUI), f);
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((CatalFournisseur)MyDataGrid.SelectedItem).Supprimer();
            MyDataGrid.ItemsSource = CatalFournisseur.Lister(f);
        }
    }
}
