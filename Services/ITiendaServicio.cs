using System.Collections.Generic;
using BodeAppBasico.Models;

namespace BodeAppBasico.Services
{
    public interface ITiendaServicio
    {
        // Consultas (GET)
        List<Cliente> ObtenerClientes();
        List<Producto> ObtenerProductos();
        List<Bodega> ObtenerBodegas();
        List<Inventario> ObtenerInventarios();
        List<Factura> ObtenerFacturas();

        // Operaciones (POST)
        void AgregarCliente(Cliente c);
        void AgregarProducto(Producto p);
        (bool Exito, string Mensaje) IngresarMercancia(PeticionIngreso p);
        (bool Exito, string Mensaje) TrasladarMercancia(PeticionTraslado t);
        (bool Exito, string Mensaje, int NumeroFactura) GenerarFactura(PeticionFactura peticion);
    }
}