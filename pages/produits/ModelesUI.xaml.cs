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

namespace VéloMax.pages
{
    public sealed partial class ModelesUI : Page
    {
        public ModelesUI()
        {
            this.InitializeComponent();
            /*
            foreach (DataGridColumn c in MyDataGrid.Columns)
            {
                if (c.Tag != null && c.Tag.Equals("MaLigne"))
                {
                    (c as DataGridComboBoxColumn).ItemsSource = ConvertisseurLigneModel.LigneVersListeString();
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
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterModeleUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Modele)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(ModelesUI));
        }
    }
}


