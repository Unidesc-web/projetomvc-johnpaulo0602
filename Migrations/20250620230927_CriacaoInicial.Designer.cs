﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projetomvc_johnpaulo0602.Data;

#nullable disable

namespace projetomvc_johnpaulo0602.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250620230927_CriacaoInicial")]
    partial class CriacaoInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Icone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cor = "#FF6B6B",
                            Icone = "UtensilsCrossed",
                            Nome = "Alimentação"
                        },
                        new
                        {
                            Id = 2,
                            Cor = "#4ECDC4",
                            Icone = "Car",
                            Nome = "Transporte"
                        },
                        new
                        {
                            Id = 3,
                            Cor = "#45B7D1",
                            Icone = "Heart",
                            Nome = "Saúde"
                        },
                        new
                        {
                            Id = 4,
                            Cor = "#F9CA24",
                            Icone = "GraduationCap",
                            Nome = "Educação"
                        },
                        new
                        {
                            Id = 5,
                            Cor = "#6C5CE7",
                            Icone = "Gamepad2",
                            Nome = "Lazer"
                        },
                        new
                        {
                            Id = 6,
                            Cor = "#A0E7E5",
                            Icone = "Home",
                            Nome = "Casa"
                        },
                        new
                        {
                            Id = 7,
                            Cor = "#FD79A8",
                            Icone = "Shirt",
                            Nome = "Roupas"
                        },
                        new
                        {
                            Id = 8,
                            Cor = "#00B894",
                            Icone = "ShoppingCart",
                            Nome = "Mercado"
                        },
                        new
                        {
                            Id = 9,
                            Cor = "#E17055",
                            Icone = "FileText",
                            Nome = "Contas"
                        },
                        new
                        {
                            Id = 10,
                            Cor = "#B2BEC3",
                            Icone = "DollarSign",
                            Nome = "Outros"
                        });
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstituicaoFinanceiraId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InstituicaoFinanceiraId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Gasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("EhDespesaFixa")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EstaPago")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ContaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.InstituicaoFinanceira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InstituicoesFinanceiras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cor = "#4F46E5",
                            Nome = "Carteira"
                        },
                        new
                        {
                            Id = 2,
                            Cor = "#8A2BE2",
                            Nome = "Nubank"
                        },
                        new
                        {
                            Id = 3,
                            Cor = "#FFD700",
                            Nome = "Banco do Brasil"
                        },
                        new
                        {
                            Id = 4,
                            Cor = "#DC143C",
                            Nome = "Bradesco"
                        },
                        new
                        {
                            Id = 5,
                            Cor = "#FF6600",
                            Nome = "Itaú"
                        },
                        new
                        {
                            Id = 6,
                            Cor = "#FF0000",
                            Nome = "Santander"
                        },
                        new
                        {
                            Id = 7,
                            Cor = "#1E90FF",
                            Nome = "Caixa"
                        },
                        new
                        {
                            Id = 8,
                            Cor = "#FF8C00",
                            Nome = "Inter"
                        },
                        new
                        {
                            Id = 9,
                            Cor = "#000000",
                            Nome = "C6 Bank"
                        },
                        new
                        {
                            Id = 10,
                            Cor = "#21C25E",
                            Nome = "PicPay"
                        },
                        new
                        {
                            Id = 11,
                            Cor = "#32CD32",
                            Nome = "Banco Original"
                        },
                        new
                        {
                            Id = 12,
                            Cor = "#4169E1",
                            Nome = "BTG Pactual"
                        },
                        new
                        {
                            Id = 13,
                            Cor = "#FF4500",
                            Nome = "XP Investimentos"
                        },
                        new
                        {
                            Id = 14,
                            Cor = "#009EE3",
                            Nome = "Mercado Pago"
                        },
                        new
                        {
                            Id = 15,
                            Cor = "#808080",
                            Nome = "Outros"
                        });
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Conta", b =>
                {
                    b.HasOne("projetomvc_johnpaulo0602.Models.InstituicaoFinanceira", "InstituicaoFinanceira")
                        .WithMany("Contas")
                        .HasForeignKey("InstituicaoFinanceiraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetomvc_johnpaulo0602.Models.Usuario", "Usuario")
                        .WithMany("Contas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstituicaoFinanceira");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Gasto", b =>
                {
                    b.HasOne("projetomvc_johnpaulo0602.Models.Categoria", "Categoria")
                        .WithMany("Gastos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetomvc_johnpaulo0602.Models.Conta", "Conta")
                        .WithMany("Gastos")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetomvc_johnpaulo0602.Models.Usuario", "Usuario")
                        .WithMany("Gastos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Conta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Categoria", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Conta", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.InstituicaoFinanceira", b =>
                {
                    b.Navigation("Contas");
                });

            modelBuilder.Entity("projetomvc_johnpaulo0602.Models.Usuario", b =>
                {
                    b.Navigation("Contas");

                    b.Navigation("Gastos");
                });
#pragma warning restore 612, 618
        }
    }
}
