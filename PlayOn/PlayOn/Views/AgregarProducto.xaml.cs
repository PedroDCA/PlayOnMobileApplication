using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayOn.Models;
using PlayOn.Utilidades;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayOn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProducto : ContentPage
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {
            txtCategoria.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtEdadMinima.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtSKU.Text = string.Empty;
        }

        private bool VerificarTodosInputLlenos()
        {
            if (string.IsNullOrEmpty(txtCategoria.Text) ||
                string.IsNullOrEmpty(txtDescripcion.Text) ||
                string.IsNullOrEmpty(txtEdadMinima.Text) ||
                string.IsNullOrEmpty(txtMarca.Text) ||
                string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtSKU.Text) ||
                string.IsNullOrEmpty(txtPrecio.Text))
            {
                return false;
            }

            return true;
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            var todosInputConInformacion = VerificarTodosInputLlenos();
            if (todosInputConInformacion)
            {
                if (txtPrecio.Text.Contains("-") || txtPrecio.Text.Contains(".") || txtPrecio.Text.Contains(","))
                {
                    await DisplayAlert("Error", "El precio no puede ser negativo o contener decimales", "Aceptar");
                }
                else if (txtEdadMinima.Text.Contains("-") || txtEdadMinima.Text.Contains(".") || txtEdadMinima.Text.Contains(","))
                {
                    await DisplayAlert("Error", "La edad no puede ser negativa o contener decimales", "Aceptar");
                }

                else
                {
                    GuardarEnBaseDatos();
                    await DisplayAlert("Datos de Producto", "Producto creado", "Aceptar");
                    await Navigation.PopAsync();
                }
                
            } else
            {
                await DisplayAlert("Datos de Usuario", "No ha especificado todos los datos", "Aceptar");
            }
        }

        async private void GuardarEnBaseDatos()
        {
            var nuevoProducto = new ArticuloModel
            {
                Cantidad = 0,
                Categoria = txtCategoria.Text,
                Descripcion = txtDescripcion.Text,
                EdadMinima = Int32.Parse(txtEdadMinima.Text),
                Imagen = ((FileImageSource)xfImage.Source).File,
                Marca = txtMarca.Text,
                Nombre = txtNombre.Text,
                FechaIngreso = DateTime.Today,
                Precio = Int32.Parse(txtPrecio.Text),
                Sku = txtSKU.Text
            };
            await App.Database.AgregarArticuloAsync(nuevoProducto);
        }

        async private void Foto_Tapped(object sender, EventArgs e)
        {
            var opcion = await DisplayActionSheet("Opciones", "Cancelar", null, "Tomar foto", "Cargar foto");

            var resultado = await AgregarFoto.AgregarNuevaFoto(opcion);

            if (resultado.Contains("Error"))
            {
                await DisplayAlert("Error", resultado, "Aceptar");
                return;
            }

            xfImage.Source = string.IsNullOrEmpty(resultado) ? "inventario.png" : ImageSource.FromFile(resultado);
        }
    }
}