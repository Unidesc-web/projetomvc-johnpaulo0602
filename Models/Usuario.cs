// Models/Usuario.cs
using System.ComponentModel.DataAnnotations;

namespace projetomvc_johnpaulo0602.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "Estado é obrigatório")]
        public string Estado { get; set; }
        
        // Relacionamentos
        public List<Conta> Contas { get; set; } = new List<Conta>();
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
