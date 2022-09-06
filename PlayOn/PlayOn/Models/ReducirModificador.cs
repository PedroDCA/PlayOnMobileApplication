using System;
using System.Collections.Generic;
using System.Text;
using PlayOn.Interfaces;

namespace PlayOn.Models
{
    class ReducirModificador : ITipoModificador
    {
        public string TextoAMostrar { get; set; }

        public ReducirModificador(string textoAMostrar)
        {
            TextoAMostrar = textoAMostrar;
        }

        public int ConseguirNuevoInventario(int digitoIngresado, int actualInventario)
        {
            var nuevoInventario = actualInventario - digitoIngresado;

            return nuevoInventario;
        }
    }
}
