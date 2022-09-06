using System;
using System.Collections.Generic;
using System.Text;
using PlayOn.Models;
using Xamarin.Forms;

namespace PlayOn.ViewModels
{
    class InformacionProductoViewModel
    {
        public string Nombre { get; set; }
        public int CantidadDisponible { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string Fecha { get; set; }
        public string SKU { get; set; }
        public int EdadMinima { get; set; }
        public ImageSource ImagenFuente { get; set; }

        public InformacionProductoViewModel(ArticuloModel articuloModel)
        {
            this.Nombre = articuloModel.Nombre;
            this.CantidadDisponible = articuloModel.Cantidad;
            this.Precio = articuloModel.Precio;
            this.Descripcion = articuloModel.Descripcion;
            this.Marca = articuloModel.Marca;
            this.Fecha = articuloModel.FechaIngreso.ToLongDateString();
            this.SKU = articuloModel.Sku + articuloModel.Id;
            this.EdadMinima = articuloModel.EdadMinima;
            this.ImagenFuente = articuloModel.Imagen;
            this.Categoria = articuloModel.Categoria;
        }
    }
}
