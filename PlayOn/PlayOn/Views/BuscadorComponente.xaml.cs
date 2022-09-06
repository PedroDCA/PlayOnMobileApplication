using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayOn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayOn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscadorComponente : ContentView
    {
        public static readonly BindableProperty ProductosEncontradosProperty = BindableProperty.Create(nameof(ProductosEncontrados), typeof(List<ArticuloModel>), typeof(BuscadorComponente), new List<ArticuloModel>());
        private string TipoFiltroSeleccionado = "Sku,Id";

        public List<ArticuloModel> ProductosEncontrados
        {
            get => (List<ArticuloModel>)GetValue(BuscadorComponente.ProductosEncontradosProperty);
            set
            {
                SetValue(BuscadorComponente.ProductosEncontradosProperty, value);
                OnPropertyChanged("ProductosEncontrados");
            }
        }
        public BuscadorComponente()
        {
            InitializeComponent();
            ActualizarProductosEncontrados();
        }

        async private void ActualizarProductosEncontrados()
        {
            var lista = new List<ArticuloModel>();
            if (!string.IsNullOrEmpty(entTexto.Text))
            {
                var tipoFiltro = ConvertirValorAPropiedad(TipoFiltroSeleccionado);
                lista = await ConseguirProductosFiltrados(tipoFiltro, entTexto.Text);
            }

            ProductosEncontrados = lista;
        }

        private string ConvertirValorAPropiedad(string valor)
        {
            var valoresFinales = valor.Split(',');
            for (var i = 0; i < valoresFinales.Length; i++)
            {
                valoresFinales[i] = $"ArticuloModel.{valoresFinales[i]}";
            }

            return string.Join(" || ", valoresFinales);
        }

        async private Task<List<ArticuloModel>> ConseguirProductosFiltrados(string tipoFiltro, string texto)
        {
            var productosEncontrados = await App.Database.EncontrarProductosPorFiltro(tipoFiltro, texto);

            return productosEncontrados;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var botonRadio = ((RadioButton)sender);
            if (botonRadio.IsChecked)
            {
                TipoFiltroSeleccionado = botonRadio.Value.ToString();
            }

            entTexto.Text = string.Empty;
            ActualizarProductosEncontrados();
        }

        private void entTexto_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualizarProductosEncontrados();
        }
    }
}