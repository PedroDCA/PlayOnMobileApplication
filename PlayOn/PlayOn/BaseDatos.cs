using System.Collections.Generic;
using System.Threading.Tasks;
using PlayOn.Models;
using SQLite;
namespace PlayOn
{
	public class BaseDatos
	{
		private readonly SQLiteAsyncConnection _database;

		public BaseDatos(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ArticuloModel>();
            _database.CreateTableAsync<TipoUsuarioModel>();
            _database.CreateTableAsync<UsuarioModel>();
        }

        public Task<List<ArticuloModel>> ObtieneArticuloAsync()
        {
            return _database.Table<ArticuloModel>().ToListAsync();
        }

        public Task<int> AgregarArticuloAsync(ArticuloModel articulo)
        {
            return _database.InsertAsync(articulo);
        }

        public Task<int> AgregarTipoUsuarioAsync(TipoUsuarioModel tipo)
        {
            return _database.InsertAsync(tipo);
        }

        public Task<List<TipoUsuarioModel>> ObtieneTipoUsuarioAsync()
        {
            return _database.Table<TipoUsuarioModel>().ToListAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> ObtieneUsuarios()
        {
            var usuario = await _database.Table<UsuarioModel>().ToListAsync();
            return usuario;
        }

        public async Task<int> ValidarInicioSesion(string usuario, string contrasenna)
        {

            var results = await _database.Table<UsuarioModel>().Where(v => v.Usuario == usuario && v.Contrasenna == contrasenna).FirstOrDefaultAsync();

            if (results != null)
            {
                return results.Id;
            }
            else
                return -1;
        }

        public Task<List<TipoUsuarioModel>> ValidarPermisosUsuario(int idUsuario)
        {
            var query =
                "SELECT TipoUsuarioModel.* " +
                "FROM TipoUsuarioModel JOIN UsuarioModel " +
                "ON TipoUsuarioModel.Id = UsuarioModel.IdTipoUsuario " +
                "WHERE UsuarioModel.Id = " + idUsuario;

            return _database.QueryAsync<TipoUsuarioModel>(query);
        }

        public Task<List<ArticuloModel>> ConseguirProductosDisponibles()
        {
            var query =
                "SELECT ArticuloModel.* " +
                "FROM ArticuloModel " +
                "WHERE ArticuloModel.Cantidad > 0";

            return _database.QueryAsync<ArticuloModel>(query);
        }

        public Task<List<ArticuloModel>> EncontrarProductosPorFiltro(string tipoFiltro, string filtrador)
        {
            var query =
                "SELECT ArticuloModel.* " +
                "FROM ArticuloModel " +
                $"WHERE {tipoFiltro} LIKE '%{filtrador}%'";

            return _database.QueryAsync<ArticuloModel>(query);
        }

        async public Task<int> ConseguirInventarioProducto(string identificador)
        {
            var query =
                "SELECT ArticuloModel.Cantidad " +
                "FROM ArticuloModel " +
                $"WHERE (ArticuloModel.Sku || ArticuloModel.Id) = '{identificador}'";

            var producto = await _database.QueryAsync<ArticuloModel>(query);
            return producto[0].Cantidad;
        }

        async public Task<ArticuloModel> ConseguirInformacionProducto(string identificador)
        {
            var query =
                "SELECT ArticuloModel.* " +
                "FROM ArticuloModel " +
                $"WHERE (ArticuloModel.Sku || ArticuloModel.Id) = '{identificador}'";

            var producto = await _database.QueryAsync<ArticuloModel>(query);
            return producto[0] ?? new ArticuloModel();
        }

        public Task<List<int>> ActualizarInventarioProducto(int nuevoInventario, string identificador)
        {
            var query =
                "UPDATE ArticuloModel " +
                $"SET Cantidad = {nuevoInventario} " +
                $"WHERE (Sku || Id) = '{identificador}'";

            return _database.QueryAsync<int>(query);
        }

        public async Task<UsuarioModel> CargarDatosUsuario(int id)
        {
            var datoUsuario = await _database.Table<UsuarioModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return datoUsuario;
        }

        public async Task<UsuarioModel> ObtieneUsuarios(string usuario)
        {
            var datoUsuario = await _database.Table<UsuarioModel>()
                .FirstOrDefaultAsync(c => c.Usuario == usuario);

            return datoUsuario;
        }

        public Task<int> AgregarUsuarioAsync(UsuarioModel usuario)
        {
            return _database.InsertAsync(usuario);
        }

        public Task<int> ActualizarUsuarioAsync(UsuarioModel usuario)
        {
            return _database.UpdateAsync(usuario);
        }
    }
}

