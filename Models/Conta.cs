// Models/Conta.cs
using System.ComponentModel.DataAnnotations;

namespace projetomvc_johnpaulo0602.Models
{
    public class Conta
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome da conta é obrigatório")]
        public string Nome { get; set; } // Ex: "Carteira", "Conta Corrente"
        
        [Required(ErrorMessage = "Saldo inicial é obrigatório")]
        public decimal SaldoInicial { get; set; }
        
        // Relacionamentos
        public int InstituicaoFinanceiraId { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; }
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}