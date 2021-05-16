using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.System;
using VéloMax.bdd;

namespace VéloMax
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            foreach (Boutique b in Boutique.Lister())
            {
                Debug.Write(b.nomB);
            }
        }

        
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("commandes", typeof(VéloMax.Pages.CommandeMain)),
            ("stocks", typeof(VéloMax.Pages.StocksMain)),
            ("produits", typeof(VéloMax.Pages.ProduitsMain)),
            ("clients", typeof(VéloMax.Pages.ClientsMain)),
            ("fournisseurs", typeof(VéloMax.Pages.FournisseursMain)),
            ("statistiques", typeof(VéloMax.Pages.StatistiquesMain)),
            ("notices", typeof(VéloMax.Pages.Notices)),
            ("home", typeof(VéloMax.Pages.Accueil))
            };

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            // You can also add items in code.
            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = "My content",
                Icon = new SymbolIcon((Symbol)0xF1AD),
                Tag = "content"
            });
            */
            _pages.Add(("home", typeof(VéloMax.Pages.Accueil)));

            // Add handler for ContentFrame navigation.
            NavigationContentFrame.Navigated += On_Navigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("home", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());

            // Listen to the window directly so the app responds
            // to accelerator keys regardless of which element has focus.
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                CoreDispatcher_AcceleratorKeyActivated;

            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
        }

        private void NavView_ItemInvoked(NavigationView sender,NavigationViewItemInvokedEventArgs args)
        {
           if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        // NavView_SelectionChanged is not used in this example, but is shown for completeness.
        // You will typically handle either ItemInvoked or SelectionChanged to perform navigation,
        // but not both.
        private void NavView_SelectionChanged(NavigationView sender,NavigationViewSelectionChangedEventArgs args)
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

                // Get the page type before navigation so you can prevent duplicate
                // entries in the backstack.
                var preNavPageType = NavigationContentFrame.CurrentSourcePageType;

                // Only navigate if the selected page isn't currently loaded.
                if (!(_page is null) && !Type.Equals(preNavPageType, _page))
                {
                    NavigationContentFrame.Navigate(_page, null, transitionInfo);
                }
            
        }

        private void NavView_BackRequested(NavigationView sender,
                                           NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            // When Alt+Left are pressed navigate back
            if (e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown
                && e.VirtualKey == VirtualKey.Left
                && e.KeyStatus.IsMenuKeyDown == true
                && !e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private bool TryGoBack()
        {
            if (!NavigationContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NavigationContentFrame.GoBack();
            ;
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = NavigationContentFrame.CanGoBack;

            if (NavigationContentFrame.SourcePageType == typeof(VéloMax.Pages.Accueil))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Accueil";
            }
            else if(NavigationContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }
    }
}
