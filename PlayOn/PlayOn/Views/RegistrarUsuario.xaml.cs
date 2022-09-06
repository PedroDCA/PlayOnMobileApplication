using PlayOn.Models;
using PlayOn.Utilidades;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlayOn.Views
{
    public partial class RegistrarUsuario : ContentPage
    {
        bool TiposUsuariosCreados;
        public int IdTipoUsuarioSeleccionado = -1;
        public string TipoUsuarioSeleccionado;
        private TapGestureRecognizer imagenTapped;
        public byte[] imageAsBytes { get; set; }
        bool CrearUsuario;
        UsuarioModel datosUsuario;

        public RegistrarUsuario(bool crearUsuario)
        {
            InitializeComponent();
            CrearUsuario = crearUsuario;
            imagenTapped = new TapGestureRecognizer() { Command = new Command(ImagenTapped) };
            this.xfImage.GestureRecognizers.Add(imagenTapped);
        }

        protected override async void OnAppearing()
        {
            TiposUsuariosCreados = Preferences.Get("TiposUsuariosCreados", false);
            if (TiposUsuariosCreados == false)
                await GenerarTiposResponsableBD();
            pkrTipo_Usuario.ItemsSource = await App.Database.ObtieneTipoUsuarioAsync();

            if (CrearUsuario == true)
            {
                this.Title = "Registro de Usuario";
                btnAgregarUsuario.Text = "Agregar";
            }
            else
            {
                this.Title = "Mantenimiento de Usuario";
                btnAgregarUsuario.Text = "Modificar";
                int idUsuario = Preferences.Get("IdUsuario", -1);
                datosUsuario = await App.Database.CargarDatosUsuario(idUsuario);
                if (datosUsuario.Imagen != null)
                    xfImage.Source = ImageSource.FromFile(datosUsuario.Imagen);
                entNombre.Text = datosUsuario.Nombre;
                entUsuario.Text = datosUsuario.Usuario;
                entContrasenna.Text = datosUsuario.Contrasenna;
                pkrTipo_Usuario.SelectedIndex = datosUsuario.IdTipoUsuario - 1;
            }

            base.OnAppearing();
        }

        async private void ImagenTapped()
        {

            var opcion = await DisplayActionSheet("Opciones", "Cancelar", null, "Tomar foto", "Cargar foto");

            var resultado = await AgregarFoto.AgregarNuevaFoto(opcion);

            if (resultado.Contains("Error"))
            {
                await DisplayAlert("Error", resultado, "Aceptar");
                return;
            }

            xfImage.Source = string.IsNullOrEmpty(resultado) ? "persona.png" : ImageSource.FromFile(resultado);
        }

        public async Task GenerarTiposResponsableBD()
        {
            await App.Database.AgregarTipoUsuarioAsync(new TipoUsuarioModel
            {
                TipoUsuario = "Administrador",
                PermiteAgregarArticulos = true,
                PermiteAumentarCantidad = true,
                PermiteBuscar = true,
                PermiteConsultar = true,
                PermiteReducirCantidad = true
            });

            await App.Database.AgregarTipoUsuarioAsync(new TipoUsuarioModel
            {
                TipoUsuario = "Vendedor",
                PermiteAgregarArticulos = false,
                PermiteReducirCantidad = false,
                PermiteAumentarCantidad = false,
                PermiteBuscar = true,
                PermiteConsultar = true
            });

            await App.Database.AgregarTipoUsuarioAsync(new TipoUsuarioModel
            {
                TipoUsuario = "Cajero",
                PermiteAgregarArticulos = false,
                PermiteReducirCantidad = true,
                PermiteAumentarCantidad = false,
                PermiteBuscar = true,
                PermiteConsultar = true
            });

            await App.Database.AgregarTipoUsuarioAsync(new TipoUsuarioModel
            {
                TipoUsuario = "Operador de descargar",
                PermiteAgregarArticulos = true,
                PermiteReducirCantidad = false,
                PermiteAumentarCantidad = true,
                PermiteBuscar = true,
                PermiteConsultar = true
            });

            Preferences.Set("TiposUsuariosCreados", true);

        }

        void pkrTipo_Usuario_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var field = (TipoUsuarioModel)picker.ItemsSource[selectedIndex];
                IdTipoUsuarioSeleccionado = field.Id;
                TipoUsuarioSeleccionado = field.TipoUsuario;
            }
        }

        Regex rgx = new Regex("[^A-Za-z0-9]");
        async void btnAgregarUsuario_Clicked(System.Object sender, System.EventArgs e)
        {
            if (CrearUsuario == true)
            {
                if (!string.IsNullOrEmpty(entUsuario.Text) && !string.IsNullOrEmpty(entNombre.Text) && !string.IsNullOrEmpty(entContrasenna.Text) && IdTipoUsuarioSeleccionado != -1)
                {
                    datosUsuario = await App.Database.ObtieneUsuarios(entUsuario.Text);

                    bool tieneCaracteresEspeciales = rgx.IsMatch(entContrasenna.Text);

                    if (datosUsuario != null)
                        await DisplayAlert("Datos de Usuario", "Este usuario ya existe", "Aceptar");

                    else if (entContrasenna.Text.Length >= 5 && tieneCaracteresEspeciales)
                    {
                        await App.Database.AgregarUsuarioAsync(new UsuarioModel
                        {
                            Nombre = entNombre.Text,
                            Usuario = entUsuario.Text,
                            Contrasenna = entContrasenna.Text,
                            IdTipoUsuario = IdTipoUsuarioSeleccionado,
                            Imagen = ((FileImageSource)xfImage.Source).File
                        });
                        await DisplayAlert("Datos de Usuario", "Usuario creado", "Aceptar");
                        lblRequisitosContraseña.TextColor = Color.Default;
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        lblRequisitosContraseña.TextColor = Color.Red;
                        await ShakeShakeShake();
                    }
                }
                else
                {
                    await ShakeShakeShake();
                    await DisplayAlert("Datos de Usuario", "No ha especificado todos los datos", "Aceptar");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(entUsuario.Text) && !string.IsNullOrEmpty(entNombre.Text) && !string.IsNullOrEmpty(entContrasenna.Text) && IdTipoUsuarioSeleccionado != -1)
                {
                    bool tieneCaracteresEspeciales = rgx.IsMatch(entContrasenna.Text);

                    if (entContrasenna.Text.Length >= 5 && tieneCaracteresEspeciales)
                    {
                        datosUsuario.Nombre = entNombre.Text;
                        datosUsuario.Usuario = entUsuario.Text;
                        datosUsuario.Contrasenna = entContrasenna.Text;
                        datosUsuario.IdTipoUsuario = IdTipoUsuarioSeleccionado;
                        datosUsuario.Imagen = ((FileImageSource)xfImage.Source).File;

                        if (Preferences.Get("RecordarUsuario", false) == true)
                        {
                            Preferences.Set("Usuario", entUsuario.Text);
                            Preferences.Set("Contrasenna", entContrasenna.Text);
                        }

                        await App.Database.ActualizarUsuarioAsync(datosUsuario);
                        await DisplayAlert("Datos de Usuario", "Usuario modificado", "Aceptar");
                        lblRequisitosContraseña.TextColor = Color.Default;
                    }
                    else
                    {
                        lblRequisitosContraseña.TextColor = Color.Red;
                        await ShakeShakeShake();
                    }
                }
                else
                {
                    await ShakeShakeShake();
                    await DisplayAlert("Datos de Usuario", "No ha especificado todos los datos", "Aceptar");
                }
            }
        }

        async Task ShakeShakeShake()
        {
            try
            {
                // Usa la duración de la vibración por defecto
                Vibration.Vibrate();

                // Usa la duración de la vibración asignada
                //var duration = TimeSpan.FromSeconds(1);
                //Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // No soportado en el dispositivo
            }
            catch (Exception ex)
            {
                // Ocurrió otro error.
            }
            uint timeout = 50;
            await controles.TranslateTo(-15, 0, timeout);
            await controles.TranslateTo(15, 0, timeout);
            await controles.TranslateTo(-10, 0, timeout);
            await controles.TranslateTo(10, 0, timeout);
            await controles.TranslateTo(-5, 0, timeout);
            await controles.TranslateTo(5, 0, timeout);
            controles.TranslationX = 0;
        }
    }
}

