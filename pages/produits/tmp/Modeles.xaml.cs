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
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Diagnostics;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Produits
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Modeles : Page
    {
        public Modeles()
        {
            this.InitializeComponent();
            /*
            foreach (DataGridColumn c in MyDataGrid.Columns)
            {
                if (c.Tag != null && c.Tag.Equals("MaLigne"))
                {
                    (c as DataGridComboBoxColumn).ItemsSource = ConvertisseurLigneModel.LigneVersListe();
                }
            }
            */
        }

        public ReadOnlyCollection<Modele> modeles
        {
            get => Modele.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterModele));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Modele)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(Modeles));
        }
    }
}
