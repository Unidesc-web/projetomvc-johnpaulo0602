// Models/Gasto.cs
using System.ComponentModel.DataAnnotations;

namespace projetomvc_johnpaulo0602.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Valor { get; set; }
        
        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Data { get; set; }
        
        public bool EstaPago { get; set; } = false;
        
        public string? Tags { get; set; } // Nullable porque é opcional
        
        public bool EhDespesaFixa { get; set; } = false;
        
        // Relacionamentos
        [Required(ErrorMessage = "Categoria é obrigatória")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        
        [Required(ErrorMessage = "Conta é obrigatória")]
        public int ContaId { get; set; }
        public Conta Conta { get; set; } = null!;
        
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}