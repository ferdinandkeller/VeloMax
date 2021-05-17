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

namespace VéloMax.Pages
{
    public sealed partial class StatistiquesMain : Page
    {
        public StatistiquesMain()
        {
            this.InitializeComponent();
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("statsVentes", typeof(VéloMax.Pages.Statistiques.StatVentes)),
            ("statsClients", typeof(VéloMax.Pages.Statistiques.StatClients)),
            ("statsFidelio", typeof(VéloMax.Pages.Statistiques.StatFidelio)),
            ("statsCommandes", typeof(VéloMax.Pages.Statistiques.StatCommandes))
        };

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
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

            var preNavPageType = NavigationContentFrame.CurrentSourcePageType;

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

            if (NavViewStatistiques.IsPaneOpen &&
                (NavViewStatistiques.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavViewStatistiques.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NavigationContentFrame.GoBack();
            return true;
        }
    }
}
