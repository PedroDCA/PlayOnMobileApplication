using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlayOn.Views
{
    public partial class HomePage : ContentPage
    {
        bool RecordarUsuario;

        public HomePage()
        {
            InitializeComponent();
            RecordarUsuario = Preferences.Get("RecordarUsuario", false);
            swtRecordarDatos.IsToggled = RecordarUsuario;
            if (RecordarUsuario == true)
            {
                entUsuario.Text = Preferences.Get("Usuario", null);
                entContrasenna.Text = Preferences.Get("Contrasenna", null);
            }
        }

        async void btnIniciar_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(entUsuario.Text) && !string.IsNullOrEmpty(entContrasenna.Text))
            {
                int datosValidos = await App.Database.ValidarInicioSesion(entUsuario.Text, entContrasenna.Text);

                if (datosValidos != -1)
                {
                    Preferences.Set("IdUsuario", datosValidos);
                    if (RecordarUsuario == true)
                    {
                        Preferences.Set("Usuario", entUsuario.Text);
                        Preferences.Set("Contrasenna", entContrasenna.Text);
                    }

                    App.Current.MainPage = new NavigationPage(new MenuOpciones());
                }

                else
                {
                    await ShakeShakeShake();
                    await DisplayAlert("Ingreso", "Usuario no registrado o contraseña inválida", "Aceptar");
                }
            }
            else
                await ShakeShakeShake();
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

        async void btnRegistro_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarUsuario(true));
        }

        void swtRecordarDatos_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            RecordarUsuario = e.Value;
            Preferences.Set("RecordarUsuario", RecordarUsuario);
        }
    }
}

