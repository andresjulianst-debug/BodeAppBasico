namespace BodeAppBasico.Models
{
   
    public class DetalleFactura
    {
        public int Id { get; set; } // <-- Agrega esta línea
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
    }
}
