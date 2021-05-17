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

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("produitsVelos", typeof(VéloMax.pages.ModelesUI)),
            ("produitsPieces", typeof(VéloMax.pages.PiecesUI)),
        };

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(
            string navItemTag,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            _page = item.Page;

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = NV_Produits_CF.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                NV_Produits_CF.Navigate(_page, null, transitionInfo);
            }

        }

        private void NavView_BackRequested(NavigationView sender,
                                           NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private bool TryGoBack()
        {
            if (!NV_Produits_CF.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NV_Produits.IsPaneOpen &&
                (NV_Produits.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NV_Produits.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NV_Produits_CF.GoBack();
            return true;
        }
    }
}
