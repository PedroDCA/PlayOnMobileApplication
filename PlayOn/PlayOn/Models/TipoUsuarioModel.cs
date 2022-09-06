using SQLite;
namespace PlayOn.Models
{
    public class TipoUsuarioModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string TipoUsuario { get; set;}
		public bool PermiteConsultar { get; set; }
		public bool PermiteBuscar { get; set; }
		public bool PermiteReducirCantidad { get; set;}
		public bool PermiteAumentarCantidad { get; set; }
		public bool PermiteAgregarArticulos { get; set; }

	}
}

