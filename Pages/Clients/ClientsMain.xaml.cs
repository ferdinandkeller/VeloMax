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
using VéloMax.Pages;

namespace VéloMax.Pages
{
    public sealed partial class ClientsMain : Page
    {
        public ClientsMain()
        {
            this.InitializeComponent();
            NavViewClients.SelectedItem = NavViewClients_Default;
            NavigationContentFrame.Navigate(typeof(Particuliers));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (((NavigationViewItem)args.SelectedItem).Tag)
            {
                case "clientParticulier":
                    NavigationContentFrame.Navigate(typeof(Particuliers));
                    break;
                case "clientEntreprise":
                    NavigationContentFrame.Navigate(typeof(Entreprises));
                    break;
            }
        }
    }
}
