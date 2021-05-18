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
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Diagnostics;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class CommandesEncoursUI : Page
    {
        public CommandesEncoursUI()
        {
            this.InitializeComponent();
            MyDataGrid.ItemsSource = Commande.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterCommandeUI));
        }

        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            Commande c = (Commande)MyDataGrid.SelectedItem;
            if (c != null)
            {
                ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(DetailCommandeUI), c.numC);
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Commande)MyDataGrid.SelectedItem).Supprimer();
            MyDataGrid.ItemsSource = Commande.Lister();
        }
    }
}
