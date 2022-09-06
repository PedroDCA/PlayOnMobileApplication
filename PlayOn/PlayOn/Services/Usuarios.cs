using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlayOn.Models;
using SQLite;
using Xamarin.Essentials;

namespace PlayOn.Services
{
	public class Usuarios : IUsuarios
	{
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PlayOnData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<UsuarioModel>();
        }

        public async Task AgregarUsuario(
			string nombre,
            string usuario,
			string contrasenna,
			string imagen,
			int idtipoUsuario,
			bool administrador,
			string tipoUsuario)
		{
            var image = "Rubik_cube.png";
            var user = new UsuarioModel
            {
                Nombre = nombre,
                Usuario = usuario,
                Contrasenna = contrasenna,
                Imagen = image,
                IdTipoUsuario = idtipoUsuario,
                Administrador = administrador,
                TipoUsuario = tipoUsuario
            };

            var id = await db.InsertAsync(user);
        }

        public async Task<IEnumerable<UsuarioModel>> ObtieneUsuarios()
        {
            await Init();

            var usuario = await db.Table<UsuarioModel>().ToListAsync();
            return usuario;
        }

        public async Task<UsuarioModel> ObtieneUsuarios(int id)
        {
            await Init();

            var usuario = await db.Table<UsuarioModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return usuario;
        }
    }
}

