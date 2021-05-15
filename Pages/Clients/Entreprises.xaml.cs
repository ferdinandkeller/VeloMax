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
using static VéloMax.Boutique;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages.Clients
{


    public sealed partial class Entreprises : Page
    {
        public Entreprises()
        {
            this.InitializeComponent();
            ListeBoutiques.ItemsSource = GetBoutiques((App.Current as App).ConnectionString);
            //ListeBoutiques.Source = GetBoutiques((App.Current as App).ConnectionString);
        }

        public void ButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AjouterBoutique), null);
        }

        public void IdClicked(object sender, RoutedEventArgs e)
        {
            //A faire (tri par Id)
            
            
        }

        public void nomClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

        public void adresseClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

        public void telClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

        public void mailClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

        public void remiseClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

        private void ListeBoutiques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}


