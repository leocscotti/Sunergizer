using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunergizer_API.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdFonte { get; set; }
        public FonteEnergia FonteEnergia { get; set; }
        public DateTime DataRegistro { get; set; } = DateTime.Now;
        public double KwhConsumidos { get; set; }
    }
}
