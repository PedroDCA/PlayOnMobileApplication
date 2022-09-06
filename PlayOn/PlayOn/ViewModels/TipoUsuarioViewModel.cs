using System;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using PlayOn.Models;
using PlayOn.Services;
using Xamarin.Forms;

namespace PlayOn.ViewModels
{
	public class TipoUsuarioViewModel
	{
        public string TipoUsuario { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteBuscar { get; set; }
        public bool PermiteReducirCantidad { get; set; }
        public bool PermiteAumentarCantidad { get; set; }
        public bool PermiteAgregarArticulos { get; set; }

		public AsyncCommand SaveCommand { get; }
		ITipoUsuario tipoService;

		public TipoUsuarioViewModel()
		{
			SaveCommand = new AsyncCommand(Save);
			tipoService = DependencyService.Get<ITipoUsuario>();
			
		}

		async Task Save()
		{
			//if (string.IsNullOrWhiteSpace(name) ||
			//	string.IsNullOrWhiteSpace(roaster))
			//{
			//	return;
			//}

			await tipoService.AgregarTipoUsuario("Administrador", true, true, true, true, true);
		}
	}
}

