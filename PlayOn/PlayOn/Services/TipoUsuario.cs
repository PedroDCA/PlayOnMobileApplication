using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlayOn.Models;
using SQLite;
using Xamarin.Essentials;

namespace PlayOn.Services
{
    public class TipoUsuario: ITipoUsuario
	{
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PlayOnData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<TipoUsuarioModel>();

            await AgregarTipoUsuario("Administrador", true, true, true, true, true);
        }

        public async Task AgregarTipoUsuario(
            string tipoUsuario,
            bool permiteConsultar,
            bool permiteBuscar,
            bool permiteReducirCantidad,
            bool permiteAumentarCantidad,
            bool permiteAgregarArticulos)
        {
            await Init();

            var usuario = new TipoUsuarioModel
            {
                TipoUsuario = tipoUsuario,
                PermiteConsultar = permiteConsultar,
                PermiteBuscar = permiteBuscar,
                PermiteReducirCantidad = permiteReducirCantidad,
                PermiteAumentarCantidad = permiteAumentarCantidad,
                PermiteAgregarArticulos = permiteAgregarArticulos
            };

            await db.InsertAsync(usuario);
        }

        public async Task<IEnumerable<TipoUsuarioModel>> ObtieneTipoUsuarios()
        {
            await Init();

            var tipoUsuario = await db.Table<TipoUsuarioModel>().ToListAsync();
            return tipoUsuario;
        }

        public async Task<TipoUsuarioModel> ObtieneTipoUsuarios(int id)
        {
            await Init();

            var tipoUsuario = await db.Table<TipoUsuarioModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return tipoUsuario;
        }
    }
}

