namespace Sunergizer_API.DTO
{
    public class ConsumoRequest
    {
        public int IdUsuario { get; set; }
        public int IdFonte { get; set; }
        public DateTime DataRegistro { get; set; } = DateTime.Now;
        public double KwhConsumidos { get; set; }
    }
}
