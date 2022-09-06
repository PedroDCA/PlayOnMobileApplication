using System;
using System.Collections.Generic;
using System.Text;
using PlayOn.Interfaces;

namespace PlayOn.Models
{
    class AgregarModificador : ITipoModificador
    {
        public string TextoAMostrar { get; set; }

        public AgregarModificador(string textoAMostrar)
        {
            TextoAMostrar = textoAMostrar;
        }

        public int ConseguirNuevoInventario(int digitoIngresado, int actualInventario)
        {
            var nuevoInventario = actualInventario + digitoIngresado;

            return nuevoInventario;
        }
    }
}
