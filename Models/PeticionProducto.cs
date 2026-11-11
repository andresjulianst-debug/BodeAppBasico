namespace BodeAppBasico.Models
{
    public class PeticionProducto
    // esta clase funiona como un objeto de transferencia DTO, recibe los datos que el usuario envía desde el frontend
    // Maneja los datos que ingresa el usuario separado de los datos persistententes del sistema
    {
        public string Codigo { get; set; } = "";
        public int Cantidad { get; set; }
    }
}
