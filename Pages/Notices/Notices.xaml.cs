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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Notices : Page
    {
        public List<Notice> notices { get; set; }
        public Notices()
        {
            this.InitializeComponent();

        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
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
            if (NavViewNotices.IsPaneOpen &&
                (NavViewNotices.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavViewNotices.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;
            NavigationContentFrame.GoBack();
            return true;
        }

        public void Enregistrer_Clicked(object sender, RoutedEventArgs e)
        {
            //A faire


        }
        private void ListeBoutiques_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void nomClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }
        public void tailleClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }
        public void telClicked(object sender, RoutedEventArgs e)
        {
            //A faire 
        }

    }
    
    public class Notice
    {
        public string Name { get; set; }
        public bool ExportXAML { get; set; }
        public bool ExportJSON { get; set; }
        public bool Exporttxt { get; set; }
    }
}
