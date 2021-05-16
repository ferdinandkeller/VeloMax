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
using Windows.UI.Core;
using Windows.System;
using Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using VéloMax.bdd;
using VéloMax.Pages;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Commandes
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Envoyee : Page
    {
        public Envoyee()
        {
            this.InitializeComponent();
        }

        /*
        public ReadOnlyCollection<Boutique> entreprises
        {
            get => Boutique.Lister();
        }
        */

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            // ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterBoutiqueUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            // ((Boutique)MyDataGrid.SelectedItem).Supprimer();
            // ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(BoutiquesUI));
        }
    }
}
