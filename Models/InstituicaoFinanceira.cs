// Models/InstituicaoFinanceira.cs
using System.ComponentModel.DataAnnotations;

namespace projetomvc_johnpaulo0602.Models
{
    public class InstituicaoFinanceira
    {
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Cor { get; set; } // Cor em hexadecimal #000000
        
        // Relacionamentos
        public List<Conta> Contas { get; set; } = new List<Conta>();
    }
}