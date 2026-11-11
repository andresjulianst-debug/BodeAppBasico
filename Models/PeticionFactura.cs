namespace BodeAppBasico.Models
{
    public class PeticionFactura
// esta clase funiona como un objeto de transferencia DTO, recibe los datos que el usuario envía desde el frontend
// Maneja los datos que ingresa el usuario separado de los datos persistententes del sistema
    {
        public string CedulaCliente { get; set; } = "";
        public int IdBodega { get; set; }
        //Bodega desde la que se hará la factura
        public List<PeticionProducto> Productos { get; set; } = new List<PeticionProducto>();
    }
}
