using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlayOn.Models;
using SQLite;
using Xamarin.Essentials;

namespace PlayOn.Services
{
	public class Articulos : IArticulos
	{
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PlayOnData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ArticuloModel>();
        }

        public async Task AgregarArticulo(
            string sku,
            string nombre,
            string marca,
            int edadMinima,
            string descripcion,
            int cantidad,
            double precio,
            DateTime fechaIngreso,
            string imagen,
            int idCategoria)
        {
            await Init();
            var image = "Rubik_cube.png";
            var articulo = new ArticuloModel
            {
                Sku = sku,
                Nombre = nombre,
                Marca = marca,
                EdadMinima = edadMinima,
                Descripcion = descripcion,
                Cantidad = cantidad,
                Precio = precio,
                FechaIngreso = fechaIngreso,
                Imagen = image,
                IdCategoria = idCategoria
            };

            var id = await db.InsertAsync(articulo);
        }

        public async Task<IEnumerable<ArticuloModel>> ObtieneArticulos()
        {
            await Init();

            var art = await db.Table<ArticuloModel>().ToListAsync();
            return art;
        }

        public async Task<ArticuloModel> ObtieneArticulos(int id)
        {
            await Init();

            var art = await db.Table<ArticuloModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return art;
        }

        public async Task RemoverArticulo(int id)
        {
            await Init();

            await db.DeleteAsync<ArticuloModel>(id);
        }
    }
}

