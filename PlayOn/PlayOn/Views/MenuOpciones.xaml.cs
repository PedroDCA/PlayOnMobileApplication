using PlayOn.Models;
using PlayOn.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlayOn.Views
{
    public partial class MenuOpciones : ContentPage
    {
        ObservableCollection<MenuOpcionesViewModel> items;
        public static bool PermiteConsultar { get; set; }
        public static bool PermiteBuscar { get; set; }
        public static bool PermiteReducirCantidad { get; set; }
        public static bool PermiteAumentarCantidad { get; set; }
        public static bool PermiteAgregarArticulos { get; set; }

        public MenuOpciones()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            int idUsuario = Preferences.Get("IdUsuario", -1);
            var datosUsuario = await App.Database.CargarDatosUsuario(idUsuario);
            if (datosUsuario.Imagen != null)
                xfImage.Source = ImageSource.FromFile(datosUsuario.Imagen);
            lblNombre.Text = datosUsuario.Nombre;
            var permisos = await App.Database.ValidarPermisosUsuario(idUsuario);
            PermiteConsultar = permisos[0].PermiteConsultar;
            PermiteBuscar = permisos[0].PermiteBuscar;
            PermiteReducirCantidad = permisos[0].PermiteReducirCantidad;
            PermiteAumentarCantidad = permisos[0].PermiteAumentarCantidad;
            PermiteAgregarArticulos = permisos[0].PermiteAgregarArticulos;
            lblTipoUsuario.Text = permisos[0].TipoUsuario;
            CargarOpciones();
            base.OnAppearing();
        }

        void CargarOpciones()
        {

            items = new ObservableCollection<MenuOpcionesViewModel>();

            items.Add(new MenuOpcionesViewModel { Id = 1, Titulo = "Mantenimiento de usuario", Fondo = "white", FuenteIcono = "persona.png", TargetType = typeof(RegistrarUsuario), ParametrosConstructor = new Object[] { false } });

            if (PermiteConsultar)
                items.Add(new MenuOpcionesViewModel { Id = 6, Titulo = "Ver artículos disponibles", Fondo = "white", FuenteIcono = "inventario.png", TargetType = typeof(ArticulosDisponibles) });
            if (PermiteBuscar)
                items.Add(new MenuOpcionesViewModel { Id = 7, Titulo = "Buscar artículos", Fondo = "white", FuenteIcono = "buscar.png", TargetType = typeof(BuscadorProductos) });
            if (PermiteReducirCantidad)
                items.Add(new MenuOpcionesViewModel { Id = 8, Titulo = "Reducir cantidad", Fondo = "white", FuenteIcono = "restar.png", TargetType = typeof(ModificarCantidadProducto), ParametrosConstructor = new Object[] { new ReducirModificador("Reducir") } });
            if (PermiteAumentarCantidad)
                items.Add(new MenuOpcionesViewModel { Id = 9, Titulo = "Aumentar cantidad", Fondo = "white", FuenteIcono = "sumar.png", TargetType = typeof(ModificarCantidadProducto), ParametrosConstructor = new Object[] { new AgregarModificador("Aumentar") } });
            if (PermiteAgregarArticulos)
                items.Add(new MenuOpcionesViewModel { Id = 10, Titulo = "Agregar artículos", Fondo = "white", FuenteIcono = "inventario.png", TargetType = typeof(AgregarProducto) });

            items.Add(new MenuOpcionesViewModel { Id = 5, Titulo = "Cerrar Sesión", Fondo = "white", FuenteIcono = "logOff.png", TargetType = null });

            listaOpciones.ItemsSource = items;
        }

        async void listaOpciones_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuOpcionesViewModel;

            if (item != null)
            {
                if (item.Id == 5)
                    App.Current.MainPage = new NavigationPage(new HomePage());
                else
                    await Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType, item.ParametrosConstructor));

                listaOpciones.SelectedItem = null;
            }
        }
    }
}

