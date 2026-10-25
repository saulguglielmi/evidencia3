using CommunityToolkit.Mvvm.ComponentModel;

namespace pos.Models
{
    public partial class Taco : ObservableObject
    {
        public string Nombre { get; set; } = string.Empty;
        public double Precio { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Subtotal))] 
        public partial int Cantidad { get; set; }

        public double Subtotal => Precio * Cantidad;
    }
}