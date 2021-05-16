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
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class ProduitsUIMain : Page
    {
        public ProduitsUIMain()
        {
            this.InitializeComponent();
            NV_Produits.SelectedItem = NV_Produits_Default;
            NV_Produits_CF.Navigate(typeof(PiecesUI));
        }

        private void NV_Produits_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (((NavigationViewItem)args.SelectedItem).Tag)
            {
                case "produits_pieces":
                    NV_Produits_CF.Navigate(typeof(PiecesUI));
                    break;
                case "produits_modeles":
                    NV_Produits_CF.Navigate(typeof(ModelesUI));
                    break;
            }
        }
    }
}
