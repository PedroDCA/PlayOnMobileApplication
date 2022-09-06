using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PlayOn.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

/********************************************************
 * Universiodad Latinoamericana de Ciencia y Tecnología
 * DISEÑO DE APLICACIONES DE SOFTWARE
 * Proyecto final 
 * Autor: Andrés Alvarado Barrantes
 * Fecha de creación: 11 de Junio del 2022
 * ******************************************************/

namespace PlayOn.Views
{	
	public partial class MenuHamburguesaItems : ContentPage
	{
		ObservableCollection<MenuHamburguesaItemsViewModel> items;
        public ListView ListView { get { return listView; } }

        public MenuHamburguesaItems()
		{
			InitializeComponent ();
            items = new ObservableCollection<MenuHamburguesaItemsViewModel>();
            items.Add(new MenuHamburguesaItemsViewModel { Id = 3, Titulo = "Menú principal", Fondo = "white", FuenteIcono = "persona.png", TargetType = typeof(MenuOpciones) });
            items.Add(new MenuHamburguesaItemsViewModel { Id = 2, Titulo = "Registrar", Fondo = "white", FuenteIcono = "persona.png", TargetType = typeof(RegistrarUsuario) });
            items.Add(new MenuHamburguesaItemsViewModel { Id = 1, Titulo = "Cerrar Sesión", Fondo = "white", FuenteIcono = "persona.png", TargetType = null });

            listView.ItemsSource = items;

            CargarDatos();

        }

        //Muestra los datos del usuario en el menú hamburguesa
        async void CargarDatos()
        {
            int idUsuario = Preferences.Get("IdUsuario", -1);
            var datosUsuario = await App.Database.CargarDatosUsuario(idUsuario);
            if (datosUsuario.Imagen != null)
                xfImage.Source = ImageSource.FromFile(datosUsuario.Imagen);
            lblNombre.Text = datosUsuario.Nombre;
        }
    }
}

