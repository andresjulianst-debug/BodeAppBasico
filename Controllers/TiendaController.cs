using Microsoft.AspNetCore.Mvc;
using BodeAppBasico.Models;
using BodeAppBasico.Services;

namespace BodeAppBasico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaServicio _servicio;

        // Inyección de la Interfaz del Servicio
        public TiendaController(ITiendaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("clientes")]
        public IActionResult ObtenerClientes() => Ok(_servicio.ObtenerClientes());

        [HttpGet("productos")]
        public IActionResult ObtenerProductos() => Ok(_servicio.ObtenerProductos());

        [HttpGet("bodegas")]
        public IActionResult ObtenerBodegas() => Ok(_servicio.ObtenerBodegas());

        [HttpGet("inventarios")]
        public IActionResult ObtenerInventarios() => Ok(_servicio.ObtenerInventarios());

        [HttpGet("facturas")]
        public IActionResult ObtenerFacturas() => Ok(_servicio.ObtenerFacturas());

        [HttpPost("agregar-cliente")]
        public IActionResult AgregarCliente([FromBody] Cliente c)
        {
            _servicio.AgregarCliente(c);
            return Ok(new { mensaje = "Cliente agregado exitosamente." });
        }

        [HttpPost("agregar-producto")]
        public IActionResult AgregarProducto([FromBody] Producto p)
        {
            _servicio.AgregarProducto(p);
            return Ok(new { mensaje = "Producto agregado al catálogo exitosamente." });
        }

        [HttpPost("ingresar-mercancia")]
        public IActionResult IngresarMercancia([FromBody] PeticionIngreso p)
        {
            var resultado = _servicio.IngresarMercancia(p);
            if (!resultado.Exito) return BadRequest(new { mensaje = resultado.Mensaje });
            return Ok(new { mensaje = resultado.Mensaje });
        }

        [HttpPost("trasladar")]
        public IActionResult TrasladarMercancia([FromBody] PeticionTraslado t)
        {
            var resultado = _servicio.TrasladarMercancia(t);
            if (!resultado.Exito) return BadRequest(new { mensaje = resultado.Mensaje });
            return Ok(new { mensaje = resultado.Mensaje });
        }

        [HttpPost("generar-factura")]
        public IActionResult GenerarFactura([FromBody] PeticionFactura peticion)
        {
            var resultado = _servicio.GenerarFactura(peticion);
            if (!resultado.Exito) return BadRequest(new { mensaje = resultado.Mensaje });
            return Ok(new { mensaje = resultado.Mensaje, numero = resultado.NumeroFactura });
        }
    }
}