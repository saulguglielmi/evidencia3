using System;
using System.IO; 
using System.Text.Json;
using Microsoft.UI.Xaml;

namespace pos
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; } = new();

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void GuardarTicket_Click(object sender, RoutedEventArgs e)
        {

            if (ViewModel.Carrito.Count == 0) return;

            var ticket = new
            {
                Folio = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                Fecha = DateTime.Now,
                Productos = ViewModel.Carrito,
                Total = ViewModel.Total
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(ticket, options);

            try
            {
                string documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string rutaArchivo = Path.Combine(documentos, $"Ticket_{DateTime.Now:yyyyMMdd_HHmmss}.json");

                File.WriteAllText(rutaArchivo, jsonString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al guardar: {ex.Message}");
            }
        }
    }
}