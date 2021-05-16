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
using System.Diagnostics;
using VéloMax.bdd;
using VéloMax.Pages;

namespace VéloMax
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationPrincipale.SelectedItem = NavigationPrincipale_Default;
            NavigationContentFrame.Navigate(typeof(Accueil));
        }

        private void NavigationPrincipale_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (((NavigationViewItem)args.SelectedItem).Tag)
            {
                case "home":
                    NavigationContentFrame.Navigate(typeof(Accueil));
                    break;
                case "commandes":
                    break;
                case "clients":
                    NavigationContentFrame.Navigate(typeof(ClientsMain));
                    break;
                case "programmes":
                    break;
                case "fournisseurs":
                    break;
                case "stocks":
                    break;
                case "statistiques":
                    break;
                case "notices":
                    break;
            }
        }
    }
}
