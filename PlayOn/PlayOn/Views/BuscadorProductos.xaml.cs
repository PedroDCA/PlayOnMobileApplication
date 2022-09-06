using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayOn.Models;
using PlayOn.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayOn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscadorProductos : ContentPage
    {
        public BuscadorProductos()
        {
            InitializeComponent();
        }

        private void buscadorComponente_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ProductosEncontrados"))
            {
                var buscador = (BuscadorComponente)sender;
                ActualizarLista(buscador.ProductosEncontrados);
            }
        }

        private void ActualizarLista(List<ArticuloModel> productosDisponibles)
        {
            var productos = new ObservableCollection<TarjetaProductoViewModel>();
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
    }
}