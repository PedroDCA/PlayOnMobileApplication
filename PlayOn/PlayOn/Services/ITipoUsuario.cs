using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayOn.Models;

namespace PlayOn.Services
{
	public interface ITipoUsuario
	{
		Task AgregarTipoUsuario(
			string tipoUsuario,
			bool permiteConsultar,
			bool permiteBuscar,
			bool permiteReducirCantidad,
			bool permiteAumentarCantidad,
			bool permiteAgregarArticulos);
		Task<IEnumerable<TipoUsuarioModel>> ObtieneTipoUsuarios();
		Task<TipoUsuarioModel> ObtieneTipoUsuarios(int id);
	}
}

