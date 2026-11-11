namespace BodeAppBasico.Models
{
    public class Factura
    {
        public int Numero { get; set; }
        public string CedulaCliente { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public int IdBodega { get; set; }
        public string NombreBodega { get; set; } = ""; // Para mostrar el nombre en las consultas
        public DateTime Fecha { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
        public double TotalPagar { get; set; }
    }
}
