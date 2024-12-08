namespace ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Models
{
    public class Carrito
    {
        public string Codigo { get; set; }
        public string NombreArticulo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get { return Precio * Cantidad; } }
    }
}
