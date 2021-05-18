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
using Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using VéloMax.bdd;
using VéloMax.pages;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace VéloMax.pages
{
    public sealed partial class StatVentes : Page
    {
        public StatVentes()
        {
            this.InitializeComponent();
        }
        public ReadOnlyCollection<EtatStockModele> ventesModeles
        {
            get => ContenuCommandeModele.ListerQuantitesVenduesM();
        }
        public ReadOnlyCollection<EtatStockPiece> ventesPieces
        {
            get => ContenuCommandePiece.ListerQuantitesVenduesP();
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
