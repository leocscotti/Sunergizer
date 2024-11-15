using System.ComponentModel.DataAnnotations;

namespace Sunergizer_API.DTO
{
    public class ComunidadeRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 5 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [MaxLength(50, ErrorMessage = "A cidade deve ter no máximo 5 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [MaxLength(2, ErrorMessage = "A UF deve seguir o padrão de unidade federativa, ex: SP")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [MinLength(2, ErrorMessage = "O numero de usuarios deve ser pelo menos 2")]
        public string TotalUsuarios { get; set; }
    }
}
