// Models/Categoria.cs
using System.ComponentModel.DataAnnotations;

namespace projetomvc_johnpaulo0602.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome da categoria é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        public string Cor { get; set; } = string.Empty;
        
        [Required]
        public string Icone { get; set; } = string.Empty;
        
        // Relacionamentos
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}