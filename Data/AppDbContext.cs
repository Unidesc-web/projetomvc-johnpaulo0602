// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Models;

namespace projetomvc_johnpaulo0602.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        // Tabelas do banco
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<InstituicaoFinanceira> InstituicoesFinanceiras { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamentos
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany(u => u.Gastos)
                .HasForeignKey(g => g.UsuarioId);
                
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.CategoriaId);
                
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Conta)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.ContaId);
                
            modelBuilder.Entity<Conta>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(c => c.UsuarioId);
                
            modelBuilder.Entity<Conta>()
                .HasOne(c => c.InstituicaoFinanceira)
                .WithMany(i => i.Contas)
                .HasForeignKey(c => c.InstituicaoFinanceiraId);
                
            // Dados iniciais - Instituições Financeiras
            modelBuilder.Entity<InstituicaoFinanceira>().HasData(
                new InstituicaoFinanceira { Id = 1, Nome = "Carteira", Cor = "#4F46E5" },
                new InstituicaoFinanceira { Id = 2, Nome = "Nubank", Cor = "#8A2BE2" },
                new InstituicaoFinanceira { Id = 3, Nome = "Banco do Brasil", Cor = "#FFD700" },
                new InstituicaoFinanceira { Id = 4, Nome = "Bradesco", Cor = "#DC143C" },
                new InstituicaoFinanceira { Id = 5, Nome = "Itaú", Cor = "#FF6600" },
                new InstituicaoFinanceira { Id = 6, Nome = "Santander", Cor = "#FF0000" },
                new InstituicaoFinanceira { Id = 7, Nome = "Caixa", Cor = "#1E90FF" },
                new InstituicaoFinanceira { Id = 8, Nome = "Inter", Cor = "#FF8C00" },
                new InstituicaoFinanceira { Id = 9, Nome = "C6 Bank", Cor = "#000000" },
                new InstituicaoFinanceira { Id = 10, Nome = "PicPay", Cor = "#21C25E" },
                new InstituicaoFinanceira { Id = 11, Nome = "Banco Original", Cor = "#32CD32" },
                new InstituicaoFinanceira { Id = 12, Nome = "BTG Pactual", Cor = "#4169E1" },
                new InstituicaoFinanceira { Id = 13, Nome = "XP Investimentos", Cor = "#FF4500" },
                new InstituicaoFinanceira { Id = 14, Nome = "Mercado Pago", Cor = "#009EE3" },
                new InstituicaoFinanceira { Id = 15, Nome = "Outros", Cor = "#808080" }
            );
            
            // Dados iniciais - Categorias
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Alimentação", Cor = "#FF6B6B", Icone = "UtensilsCrossed" },
                new Categoria { Id = 2, Nome = "Transporte", Cor = "#4ECDC4", Icone = "Car" },
                new Categoria { Id = 3, Nome = "Saúde", Cor = "#45B7D1", Icone = "Heart" },
                new Categoria { Id = 4, Nome = "Educação", Cor = "#F9CA24", Icone = "GraduationCap" },
                new Categoria { Id = 5, Nome = "Lazer", Cor = "#6C5CE7", Icone = "Gamepad2" },
                new Categoria { Id = 6, Nome = "Casa", Cor = "#A0E7E5", Icone = "Home" },
                new Categoria { Id = 7, Nome = "Roupas", Cor = "#FD79A8", Icone = "Shirt" },
                new Categoria { Id = 8, Nome = "Mercado", Cor = "#00B894", Icone = "ShoppingCart" },
                new Categoria { Id = 9, Nome = "Contas", Cor = "#E17055", Icone = "FileText" },
                new Categoria { Id = 10, Nome = "Outros", Cor = "#B2BEC3", Icone = "DollarSign" }
            );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}