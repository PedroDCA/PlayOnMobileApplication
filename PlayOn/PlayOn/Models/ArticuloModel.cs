using System;
using SQLite;

namespace PlayOn.Models
{
	public class ArticuloModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public int EdadMinima { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
    }
}

