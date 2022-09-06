using System;
using System.ComponentModel;
using PlayOn;
using PlayOn.Views;
using Xamarin.Forms;

namespace TarjetaProducto.Controls
{
    [DesignTimeVisible(true)]
    public partial class TarjetaProducto : ContentView
    {
        public static readonly BindableProperty NombreProductoProperty = BindableProperty.Create(nameof(NombreProducto), typeof(string), typeof(TarjetaProducto), string.Empty);
        public static readonly BindableProperty ImagenFuenteProperty = BindableProperty.Create(nameof(ImagenFuente), typeof(ImageSource), typeof(TarjetaProducto), default(ImageSource));
        public static readonly BindableProperty FooterProperty = BindableProperty.Create(nameof(Footer), typeof(string), typeof(TarjetaProducto), string.Empty);
        public static readonly BindableProperty IdentificadorProperty = BindableProperty.Create(nameof(Identificador), typeof(string), typeof(TarjetaProducto), string.Empty);

        public string NombreProducto
        {
            get => (string)GetValue(TarjetaProducto.NombreProductoProperty);
            set => SetValue(TarjetaProducto.NombreProductoProperty, value);
        }

        public string Footer
        {
            get => (string)GetValue(TarjetaProducto.FooterProperty);
            set => SetValue(TarjetaProducto.FooterProperty, value);
        }

        public string Identificador
        {
            get => (string)GetValue(TarjetaProducto.IdentificadorProperty);
            set => SetValue(TarjetaProducto.IdentificadorProperty, value);
        }

        public ImageSource ImagenFuente
        {
            get => (ImageSource)GetValue(TarjetaProducto.ImagenFuenteProperty);
            set => SetValue(TarjetaProducto.ImagenFuenteProperty, value);
        }

        public TarjetaProducto()
        {
            InitializeComponent();
        }

        async public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await ParentView.Navigation.PushAsync(new InformacionProducto(Identificador), true);
        }
    }
}