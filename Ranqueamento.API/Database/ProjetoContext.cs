using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ranqueamento.API.Configuracao;
using Ranqueamento.API.Model;

namespace Ranqueamento.API.Models
{
    public partial class ProjetoContext : DbContext
    {
        public ProjetoContext()
        {
        }

        public ProjetoContext(DbContextOptions<ProjetoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Familia> Familia { get; set; } = null!;
        public virtual DbSet<Pessoa> Pessoas { get; set; } = null!;
        public virtual DbSet<ConfiguracaoDependente> ConfiguracaoDependentes { get; set; } = null!;
        public virtual DbSet<ConfiguracaoRenda> ConfiguracaoRenda { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Projeto;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfiguracaoDependente>(entity =>
            {
                entity.HasKey(e => new { e.Pontuacao, e.DependenteMinimo });

                entity.ToTable("ConfiguracaoDependente");
            });

            modelBuilder.Entity<ConfiguracaoRenda>(entity =>
            {
                entity.HasKey(e => new { e.Pontuacao, e.RendaMaxima });

                entity.Property(e => e.RendaMaxima).HasColumnType("decimal(7, 2)");
            });

            modelBuilder.Entity<Familia>(entity =>
            {
                entity.Property(e => e.Conjuge)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cpf)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RendaTotal).HasColumnType("decimal(7, 2)");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("Pessoa");

                entity.HasIndex(e => e.FamiliaId, "IX_Pessoa_FamiliaId");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Familia)
                    .WithMany(p => p.Dependentes)
                    .HasForeignKey(d => d.FamiliaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pessoa_Familia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
