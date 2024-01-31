using System.ComponentModel.DataAnnotations;

namespace DatajudClient.Domain.Models
{
    public class ModelBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
