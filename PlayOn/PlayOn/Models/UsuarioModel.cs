using SQLite;
namespace PlayOn.Models
{
    public class UsuarioModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Contrasenna { get; set; }
        public string Imagen { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}

