namespace BodeAppBasico.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public int IdBodega { get; set; }
        public string CodigoProducto { get; set; } = "";
        public int Cantidad { get; set; }
    }
}
