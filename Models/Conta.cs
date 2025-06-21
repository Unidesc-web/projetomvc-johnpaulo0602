// Models/Conta.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetomvc_johnpaulo0602.Models
{
    public class Conta
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome da conta é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Saldo inicial é obrigatório")]
        public decimal SaldoInicial { get; set; }
        
        // Propriedade calculada para saldo atual (não mapeada no banco)
        [NotMapped]
        public decimal SaldoAtual { get; set; }
        
        // Relacionamentos
        public int InstituicaoFinanceiraId { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; } = null!;
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}