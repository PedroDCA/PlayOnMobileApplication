using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayOn.Models;

namespace PlayOn.Services
{
	public interface IUsuarios
	{
		Task AgregarUsuario(
			string nombre,
			string usuario,
			string contrasenna,
			string imagen,
			int idtipoUsuario,
			bool administrador,
			string tipoUsuario);
		Task<IEnumerable<UsuarioModel>> ObtieneUsuarios();
		Task<UsuarioModel> ObtieneUsuarios(int id);
	}
}

