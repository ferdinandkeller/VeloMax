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

namespace VéloMax.pages
{
    public sealed partial class ClientsMainUI : Page
    {
        public ClientsMainUI()
        {
            this.InitializeComponent();
            NavViewClients.SelectedItem = NavViewClients_Default;
            NavigationContentFrame.Navigate(typeof(IndividusUI));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (((NavigationViewItem)args.SelectedItem).Tag)
            {
                case "clientParticulier":
                    NavigationContentFrame.Navigate(typeof(IndividusUI));
                    break;
                case "clientEntreprise":
                    NavigationContentFrame.Navigate(typeof(BoutiquesUI));
                    break;
            }
        }
    }
}
