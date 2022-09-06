using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PlayOn.Utilidades
{
    class AgregarFoto
    {
        public static async Task<string> AgregarNuevaFoto(string opcionSeleccionada)
        {
            var resultado = string.Empty;
            switch (opcionSeleccionada)
            {
                case "Tomar foto":
                    var camara = await Permissions.CheckStatusAsync<Permissions.Camera>();
                    if (camara != PermissionStatus.Granted)
                    {
                        camara = await Permissions.RequestAsync<Permissions.Camera>();
                    }

                    if (camara == PermissionStatus.Granted)
                    {
                        resultado = await TakePhotoAsync();
                    }
                    break;

                case "Cargar foto":

                    var carga = await Permissions.CheckStatusAsync<Permissions.Photos>();
                    if (carga != PermissionStatus.Granted)
                    {
                        carga = await Permissions.RequestAsync<Permissions.Photos>();
                    }

                    if (carga == PermissionStatus.Granted)
                    {
                        resultado = await PickerPhotoAsync();
                    }

                    break;
            }

            return resultado;

        }

        public static async Task<string> PickerPhotoAsync()
        {
            var resultado = string.Empty;
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                resultado = await LoadPhotoAsync(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                resultado = "Error - Caracteristica no soportada";
            }
            catch (PermissionException pEx)
            {
                resultado = "No cuenta con los permisos necesarios";
                // Permissions not granted
            }
            catch (Exception ex)
            {
                resultado = "Error - Error no esperado";
            }

            return resultado;
        }

        async static Task<string> TakePhotoAsync()
        {
            var resultado = string.Empty;
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                resultado = await LoadPhotoAsync(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                resultado = "Error - No hay cámaras disponibles";
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                resultado = "Error - No cuenta con los permisos necesarios";
                // Permissions not granted
            }
            catch (Exception ex)
            {
                resultado = "Error - Error no esperado";
            }

            return resultado;
        }

        async static Task<string> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return string.Empty;
            }
            // save the file into local storage
            var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), photo.FileName);
            //var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (Stream stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);

            }

            return newFile;
        }
    }
}
