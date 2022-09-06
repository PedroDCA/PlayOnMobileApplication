using System;
using System.Collections.Generic;
using PlayOn.ViewModels;
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
	public partial class MenuHamburguesaPagina : FlyoutPage
	{
		public MenuHamburguesaPagina()
		{
			InitializeComponent ();
			flyoutPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MenuHamburguesaItemsViewModel;

			if (item != null)
			{
				if (item.Id == 1)
					App.Current.MainPage = new HomePage();
				else
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
					flyoutPage.ListView.SelectedItem = null;
					IsPresented = false;
				}
			}
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			if (IsPresented == true)
				IsPresented = false;
			else
				IsPresented = true;
		}
	}
}

