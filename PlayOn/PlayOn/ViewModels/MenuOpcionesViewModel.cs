using System;

namespace PlayOn.ViewModels
{
    public class MenuOpcionesViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string FuenteIcono { get; set; }
        public string Fondo { get; set; }
        public bool EsVisible { get; set; }
        public Type TargetType { get; set; }
        public Object[] ParametrosConstructor { get; set; }
    }
}

