using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayOn.Interfaces;
using PlayOn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayOn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarCantidadProducto : ContentPage
    {
        private const string IngreseElMontoConstante = "Ingrese el monto a";
        private const string InventarioConstante = "inventario";
        private const string CantidadActualConstante = "Cantidad actual en inventario:";
        private ITipoModificador actualModificador;
        private string actualIdentificador = string.Empty;
        private int actualCantidad = 0;
        public ModificarCantidadProducto(ITipoModificador tipoModificacion)
        {
            InitializeComponent();
            actualModificador = tipoModificacion;
            ActualizarBotonModificarTexto(actualModificador.TextoAMostrar);
            ActualizarEtiquetaModificarTexto(actualModificador.TextoAMostrar);
            ActualizarTituloModificarTexto(actualModificador.TextoAMostrar);
        }

        private void ActualizarBotonModificarTexto(string nombreDeModificacion)
        {
            btnModificar.Text = $"{nombreDeModificacion} {InventarioConstante}";
        }

        private void ActualizarEtiquetaModificarTexto(string nombreDeModificacion)
        {
            lblMontoModificar.Text = $"{IngreseElMontoConstante} {nombreDeModificacion.ToLower()}";
        }

        private void ActualizarCantidadActualTexto(int cantidadDatos)
        {
            lblCantidadActual.Text = $"{CantidadActualConstante} {cantidadDatos}";
        }

        private void ActualizarTituloModificarTexto(string nombreDeModificacion)
        {
            ventana.Title = $"{nombreDeModificacion} {InventarioConstante}";
        }

        private void buscador_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ProductosEncontrados"))
            {
                var buscador = (BuscadorComponente)sender;
                var lista = ConvertirEnListaString(buscador.ProductosEncontrados);

                pckLista.ItemsSource = lista;
            }
        }

        private List<string> ConvertirEnListaString(List<ArticuloModel> productosEncontrados)
        {
            var listaOpciones = new List<string>();
            foreach (var producto in productosEncontrados)
            {
                var opcionTexto = $"{producto.Nombre} - {producto.Sku}{producto.Id}";
                listaOpciones.Add(opcionTexto);
            }

            return listaOpciones;
        }

        async public void ActualizarProducto(int digitoIngresado, string identificador)
        {
            var nuevoInventario = actualModificador.ConseguirNuevoInventario(digitoIngresado, actualCantidad);

            if (nuevoInventario < 0)
            {
                await DisplayAlert("Error", "El resultado final debe de acabar en 0 o positivo", "Aceptar");
                return;
            }

            await App.Database.ActualizarInventarioProducto(nuevoInventario, identificador);
            await DisplayAlert("Modificado", "El cambio se ha realizado correctamente", "Aceptar");
            await Navigation.PopAsync();
        }

        async private void btnModificar_Clicked(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Contains("-") || txtCantidad.Text.Contains(".") || txtCantidad.Text.Contains(","))
            {
                await DisplayAlert("Error", "El valor no puede ser negativo o contener decimales", "Aceptar");
                return;
            }
            var digitoIngresado = Int32.Parse(txtCantidad.Text);
            ActualizarProducto(digitoIngresado, actualIdentificador);
        }

        async private void pckLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var productoSeleccionado = picker.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(productoSeleccionado))
            {
                nuevoMontoComponente.IsVisible = false;
                return;
            }
            actualIdentificador = productoSeleccionado.Split('-')[1].Trim();
            actualCantidad = await App.Database.ConseguirInventarioProducto(actualIdentificador);
            ActualizarCantidadActualTexto(actualCantidad);
            nuevoMontoComponente.IsVisible = true;
        }
    }
}