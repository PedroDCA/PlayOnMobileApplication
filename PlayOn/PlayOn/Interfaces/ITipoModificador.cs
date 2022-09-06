using System;
using System.Collections.Generic;
using System.Text;

namespace PlayOn.Interfaces
{
    public interface ITipoModificador
    {
        string TextoAMostrar { get; set; }

        int ConseguirNuevoInventario(int digitoIngresado, int actualInventario);
    }
}
