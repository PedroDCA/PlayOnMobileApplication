using PlayOn.Models;
using PlayOn.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlayOn.Views
{
    public partial class ArticulosDisponibles : ContentPage
    {
        ObservableCollection<TarjetaProductoViewModel> productos;
        public ArticulosDisponibles()
        {
            InitializeComponent();
            CargarProductos();
        }

        async private void CargarProductos()
        {
            productos = new ObservableCollection<TarjetaProductoViewModel>();

            var productosDisponibles = await ConseguirProductosDisponibles();
            foreach (var producto in productosDisponibles)
            {
                productos.Add(new TarjetaProductoViewModel
                {
                    NombreProducto = producto.Nombre,
                    Footer = $"Disponibles: {producto.Cantidad} - Codigo: {producto.Sku}{producto.Id}",
                    Identificador = producto.Sku + producto.Id,
                    ImagenFuente = producto.Imagen == null ? "inventario.png" : producto.Imagen
                }); ;
            }

            listaProductos.ItemsSource = productos;
        }

        async private Task<List<ArticuloModel>> ConseguirProductosDisponibles()
        {
            var productosDisponibles = await App.Database.ConseguirProductosDisponibles();

            return productosDisponibles;
        }
    }
}

