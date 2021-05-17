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
using VéloMax.bdd;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Diagnostics;
using VéloMax.pages;

namespace VéloMax.pages
{
    public sealed partial class PiecesUI : Page
    {
        public PiecesUI()
        {
            this.InitializeComponent();
        }

        public ReadOnlyCollection<Piece> pieces
        {
            get => Piece.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterPieceUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Individu)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(PiecesUI));
        }

        private async void ExporterXML(object sender, RoutedEventArgs e)
        {
            string xml = "<stocks>\n  <pieces>\n";
            foreach (Piece p in Piece.ListerStockFaible())
            {
                xml += "    <piece>" + p.numP + "</piece>\n";
            }
            xml += "  </pieces>\n  <modeles>\n";
            foreach (Modele m in Modele.ListerStockFaible())
            {
                xml += "    <modele>" + m.numM + "</modele>\n";
            }
            xml += "  </modeles>\n</stocks>";

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("XML", new List<string>() { ".xml" });
            savePicker.SuggestedFileName = "stocks_limites";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, xml);
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
    }
}
