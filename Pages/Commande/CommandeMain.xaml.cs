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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CommandeMain : Page
    {
        public CommandeMain()
        {
            this.InitializeComponent();
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>{
            ("commande1", typeof(VéloMax.Pages.Commande.EnCours)),
            ("commande2", typeof(VéloMax.Pages.Commande.Envoyee)),
            ("commande3", typeof(VéloMax.Pages.Commande.AjouterCommande))
        };

        
        

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
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

        private bool TryGoBack()
        {
            if (!NavigationContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavViewCommandes.IsPaneOpen &&
                (NavViewCommandes.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavViewCommandes.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NavigationContentFrame.GoBack();
            return true;
        }

    }
}
