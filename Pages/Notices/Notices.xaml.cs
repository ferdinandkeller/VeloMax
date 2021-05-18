using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
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
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using VéloMax.bdd;

namespace VéloMax.Pages
{
    public class Notice
    {
        public string modele { get; set; }
        public string piece { get; set; }
        public string quant { get; set; }
        public string dates { get; set; }

        public Notice(int numM,string nomM, int numP,string descriptionP,int quant, DateTime dateIntroP, DateTime dateDiscP)
        {
            this.modele = $"[{numM}] " + nomM;
            this.piece = $"[{numP}] " + descriptionP;
            this.quant = quant.ToString();
            this.dates = dateIntroP.ToShortDateString() + "->" + dateDiscP.ToShortDateString();
        }
    }
    public sealed partial class Notices : Page
    {
        public Notices()
        {
            this.InitializeComponent();
            refModeleCombo.ItemsSource = Modele.ListerString();
        }

        public static List<Notice> DonnerNotice(int numM)
        {
            string requete = $"Select numM, nomM,numP, descriptionP, quant, dateIntroP,dateDiscP from modele natural join compositionmodele natural join piece where numM={numM};";
            List<Notice> list = new List<Notice>();
            ControlleurRequetes.SelectionnePlusieurs(requete, (MySqlDataReader reader) => {list.Add(new Notice(reader.GetInt32("numM"), reader.GetString("nomM"), reader.GetInt32("numP"), reader.GetString("descriptionP"), reader.GetInt32("quant"), reader.GetDateTime("dateIntroP"), reader.GetDateTime("dateDiscP"))); });
            return list;
        }

        public void Type_Model_Change(object sender, SelectionChangedEventArgs e)
        {
            MyDataGrid.ItemsSource = DonnerNotice(Modele.Lister()[refModeleCombo.SelectedIndex].numM);
        }

        private async void ExporterJSON(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json" });
            savePicker.SuggestedFileName = "clients_importants";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, Fidelio.FidelioFinJSON());
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            }
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
