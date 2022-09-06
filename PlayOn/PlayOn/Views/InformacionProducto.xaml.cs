using System;
using System.Collections.Generic;
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
    public partial class InformacionProducto : ContentPage
    {

        public InformacionProducto(string identificador)
        {
            InitializeComponent();
            LlenarInformacion(identificador);
        }

        async private void LlenarInformacion(string identificador)
        {
            var informacionProducto = await ConseguirInformacionProducto(identificador);

            lblNombre.Text = informacionProducto.Nombre;
            lblCantidadDisponible.Text = $"Disponibles: {informacionProducto.CantidadDisponible}";
            lblPrecio.Text = $"Precio: {informacionProducto.Precio}";
            imgFoto.Source = informacionProducto.ImagenFuente;
            lblDescripcion.Text = informacionProducto.Descripcion;
            lblMarca.Text = informacionProducto.Marca;
            lblFechaIngreso.Text = informacionProducto.Fecha;
            lblSKU.Text = informacionProducto.SKU;
            lblEdadMinima.Text = $"Edad minima: {informacionProducto.EdadMinima} años";
            lblCategorias.Text = informacionProducto.Categoria;
        }

        async private Task<InformacionProductoViewModel> ConseguirInformacionProducto(string identificador)
        {
            ArticuloModel productoModel = await App.Database.ConseguirInformacionProducto(identificador);

            var productoViewModel = new InformacionProductoViewModel(productoModel);

            return productoViewModel;
        }
    }
}