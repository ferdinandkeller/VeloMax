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
        
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("clientEntreprise", typeof(VéloMax.pages.BoutiquesUI)),
            ("clientParticulier", typeof(VéloMax.pages.IndividusUI)),
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
            var preNavPageType = NavigationContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                NavigationContentFrame.Navigate(_page, null, transitionInfo);
            }

        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private bool TryGoBack()
        {
            if (!NavigationContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavViewClients.IsPaneOpen &&
                (NavViewClients.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavViewClients.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NavigationContentFrame.GoBack();
            return true;
        }
    }
}
