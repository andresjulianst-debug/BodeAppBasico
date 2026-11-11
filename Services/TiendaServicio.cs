using System;
using System.Collections.Generic;
using System.Linq;
using BodeAppBasico.Data;
using BodeAppBasico.Models;

namespace BodeAppBasico.Services
{
    public class TiendaServicio : ITiendaServicio
    {
        private readonly AppDbContext _bd;

        // Inyección de la Capa de Datos (SQLite)
        public TiendaServicio(AppDbContext bd)
        {
            _bd = bd;
        }

        public List<Cliente> ObtenerClientes() => _bd.Clientes.ToList();
        public List<Producto> ObtenerProductos() => _bd.Productos.ToList();
        public List<Bodega> ObtenerBodegas() => _bd.Bodegas.ToList();
        public List<Inventario> ObtenerInventarios() => _bd.Inventarios.ToList();
        public List<Factura> ObtenerFacturas() => _bd.Facturas.ToList();

        public void AgregarCliente(Cliente c)
        {
            _bd.Clientes.Add(c);
            _bd.SaveChanges();
        }

        public void AgregarProducto(Producto p)
        {
            _bd.Productos.Add(p);
            _bd.SaveChanges();
        }

        //Ingresar Mercancia a la bodega central 
        public (bool Exito, string Mensaje) IngresarMercancia(PeticionIngreso p)
        {
            Producto productoEncontrado = null;
            foreach (var prod in _bd.Productos)
            {
                if (prod.Codigo == p.CodigoProducto)
                {
                    productoEncontrado = prod;
                    break;
                }
            }

            if (productoEncontrado == null)
            {
                return (false, "El producto no existe en la base de datos.");
            }

            Inventario inventarioEncontrado = null;
            foreach (var inv in _bd.Inventarios)
            {
                if (inv.IdBodega == 1 && inv.CodigoProducto == p.CodigoProducto)
                {
                    inventarioEncontrado = inv;
                    break;
                }
            }

            if (inventarioEncontrado == null)
            {
                Inventario nuevoInventario = new Inventario
                {
                    IdBodega = 1,
                    CodigoProducto = p.CodigoProducto,
                    Cantidad = p.Cantidad
                };
                _bd.Inventarios.Add(nuevoInventario);
            }
            else
            {
                inventarioEncontrado.Cantidad += p.Cantidad;
            }

            _bd.SaveChanges();
            return (true, $"Ingresadas {p.Cantidad} unidades a Rionegro Central.");
        }


        //Traslado de mercancia desde la bodega central a otra bodega
        public (bool Exito, string Mensaje) TrasladarMercancia(PeticionTraslado t)
        {
            Inventario origen = null;
            foreach (var inv in _bd.Inventarios)
            {
                if (inv.IdBodega == 1 && inv.CodigoProducto == t.CodigoProducto)
                {
                    origen = inv;
                    break;
                }
            }

            if (origen == null || origen.Cantidad < t.Cantidad)
            {
                return (false, "No hay suficiente stock en la bodega central.");
            }

            origen.Cantidad -= t.Cantidad;

            Inventario destino = null;
            foreach (var inv in _bd.Inventarios)
            {
                if (inv.IdBodega == t.IdBodegaDestino && inv.CodigoProducto == t.CodigoProducto)
                {
                    destino = inv;
                    break;
                }
            }

            if (destino == null)
            {
                Inventario nuevoDestino = new Inventario
                {
                    IdBodega = t.IdBodegaDestino,
                    CodigoProducto = t.CodigoProducto,
                    Cantidad = t.Cantidad
                };
                _bd.Inventarios.Add(nuevoDestino);
            }
            else
            {
                destino.Cantidad += t.Cantidad;
            }

            _bd.SaveChanges();
            return (true, "Traslado realizado con éxito.");
        }

        // Generación de facturas
        public (bool Exito, string Mensaje, int NumeroFactura) GenerarFactura(PeticionFactura peticion)
        {
            double totalFactura = 0;
            List<DetalleFactura> listaDetalles = new List<DetalleFactura>();

            foreach (var item in peticion.Productos)
            {
                Producto prodEncontrado = null;
                foreach (var p in _bd.Productos)
                {
                    if (p.Codigo == item.Codigo)
                    {
                        prodEncontrado = p;
                        break;
                    }
                }

                Inventario invEncontrado = null;
                foreach (var inv in _bd.Inventarios)
                {
                    if (inv.IdBodega == peticion.IdBodega && inv.CodigoProducto == item.Codigo)
                    {
                        invEncontrado = inv;
                        break;
                    }
                }

                if (invEncontrado == null || invEncontrado.Cantidad < item.Cantidad)
                {
                    return (false, "No hay stock suficiente para procesar la venta.", 0);
                }

                invEncontrado.Cantidad -= item.Cantidad;
                double subtotal = prodEncontrado.Precio * item.Cantidad;
                totalFactura += subtotal;

                listaDetalles.Add(new DetalleFactura
                {
                    Producto = prodEncontrado.Nombre,
                    Cantidad = item.Cantidad,
                    Subtotal = subtotal
                });
            }

            int mayorNumero = 0;
            foreach (var fact in _bd.Facturas)
            {
                if (fact.Numero > mayorNumero)
                {
                    mayorNumero = fact.Numero;
                }
            }
            int nuevoNumero = mayorNumero + 1;

            Factura facturaFinal = new Factura
            {
                Numero = nuevoNumero,
                CedulaCliente = peticion.CedulaCliente,
                IdBodega = peticion.IdBodega,
                Fecha = DateTime.Now,
                TotalPagar = totalFactura,
                Detalles = listaDetalles
            };

            _bd.Facturas.Add(facturaFinal);
            _bd.SaveChanges();

            return (true, "Factura generada con éxito", nuevoNumero);
        }
    }
}
