using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayOn.Models;

namespace PlayOn.Services
{
	public interface IArticulos
	{
        Task AgregarArticulo(
            string sku,
            string nombre,
            string marca,
            int edadMinima,
            string descripcion,
            int cantidad,
            double precio,
            DateTime fechaIngreso,
            string imagen,
            int idCategoria);

        Task<IEnumerable<ArticuloModel>> ObtieneArticulos();
		Task<ArticuloModel> ObtieneArticulos(int id);
		Task RemoverArticulo(int id);
	}
}

