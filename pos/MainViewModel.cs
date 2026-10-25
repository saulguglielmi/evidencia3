using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel; 
using pos.Models;

namespace pos
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Taco> Menu { get; } = new();
        public ObservableCollection<Taco> Carrito { get; } = new();

        [ObservableProperty]
        public partial double Total { get; set; }

        public MainViewModel()
        {
            Menu.Add(new Taco { Nombre = "Taco al Pastor", Precio = 15.0 });
            Menu.Add(new Taco { Nombre = "Taco de Suadero", Precio = 18.0 });
            Menu.Add(new Taco { Nombre = "Taco de Barbacoa", Precio = 20.0 });
            Menu.Add(new Taco { Nombre = "Taco de Cabeza", Precio = 22.0 });
            Menu.Add(new Taco { Nombre = "Gringa", Precio = 35.0 });
            Menu.Add(new Taco { Nombre = "Quesadilla", Precio = 25.0 });
        }

        [RelayCommand]
        private void AgregarAlCarrito(Taco tacoSeleccionado)
        {
            if (tacoSeleccionado == null || tacoSeleccionado.Precio <= 0) return;

            var existente = Carrito.FirstOrDefault(t => t.Nombre == tacoSeleccionado.Nombre);
            if (existente != null)
            {
                existente.Cantidad++;
            }
            else
            {
                var nuevoTaco = new Taco
                {
                    Nombre = tacoSeleccionado.Nombre,
                    Precio = tacoSeleccionado.Precio,
                    Cantidad = 1
                };

                nuevoTaco.PropertyChanged += Taco_PropertyChanged;
                Carrito.Add(nuevoTaco);
            }
            CalcularTotal();
        }

        [RelayCommand]
        private void QuitarDelCarrito(Taco taco)
        {
            if (taco == null) return;

            taco.PropertyChanged -= Taco_PropertyChanged;
            Carrito.Remove(taco);
            CalcularTotal();
        }

        private void Taco_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Taco.Cantidad) || e.PropertyName == nameof(Taco.Subtotal))
            {
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            Total = Carrito.Sum(t => t.Subtotal);
        }
    }
}