namespace BodeAppBasico.Models
{
    public class PeticionTraslado
    // esta clase funiona como un objeto de transferencia DTO, recibe los datos que el usuario envía desde el frontend
    // Maneja los datos que ingresa el usuario separado de los datos persistententes del sistema
    {
        public string CodigoProducto { get; set; } = "";
        public int Cantidad { get; set; }
        public int IdBodegaDestino { get; set; }
    }
}
