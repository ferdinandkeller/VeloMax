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

namespace VéloMax.pages
{
    public sealed partial class FidelioUI : Page
    {
        public FidelioUI()
        {
            this.InitializeComponent();
        }

        public ReadOnlyCollection<Fidelio> fidelios
        {
            get => Fidelio.Lister();
        }

        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(AjouterFidelioUI));
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Fidelio)MyDataGrid.SelectedItem).Supprimer();
            ((this.Frame.Parent as NavigationView).Content as Frame).Navigate(typeof(FidelioUI));
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
    }
}
