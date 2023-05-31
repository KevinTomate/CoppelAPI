using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CoppelAPI.Models;

namespace CoppelAPI.Repositories;

public partial class CoppelContext : DbContext
{
    public CoppelContext()
    {
    }

    public CoppelContext(DbContextOptions<CoppelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Denunciadato> Denunciadatos { get; set; }

    public virtual DbSet<Denuncium> Denuncia { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=coppel;username=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comentario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comentario1)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Folio).HasColumnName("folio");
        });

        modelBuilder.Entity<Denunciadato>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PRIMARY");

            entity.ToTable("denunciadatos");

            entity.Property(e => e.Folio)
                .ValueGeneratedNever()
                .HasColumnName("folio");
            entity.Property(e => e.CorreoElectronico)
                .HasColumnType("text")
                .HasColumnName("correoElectronico");
            entity.Property(e => e.NombreCompleto)
                .HasColumnType("text")
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.Telefono)
                .HasColumnType("text")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Denuncium>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PRIMARY");

            entity.ToTable("denuncia");

            entity.HasIndex(e => e.IdEmpresa, "fkDenunciaEmpresa_idx");

            entity.HasIndex(e => e.IdEstado, "fkDenunciaEstado_idx");

            entity.HasIndex(e => e.IdEstatus, "fkDenunciaEstatus_idx");

            entity.HasIndex(e => e.IdPais, "fkDenunciaPais_idx");

            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.Anonima).HasColumnName("anonima");
            entity.Property(e => e.Clave)
                .HasColumnType("text")
                .HasColumnName("clave");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(45)
                .HasColumnName("correoElectronico");
            entity.Property(e => e.Detalle)
                .HasMaxLength(45)
                .HasColumnName("detalle");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.IdEstatus).HasColumnName("idEstatus");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(45)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NumeroCentro).HasColumnName("numeroCentro");
            entity.Property(e => e.Telefono)
                .HasMaxLength(45)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("fkDenunciaEmpresa");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("fkDenunciaEstado");

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.IdEstatus)
                .HasConstraintName("fkDenunciaEstatus");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("fkDenunciaPais");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empresa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado");

            entity.HasIndex(e => e.IdPais, "fkEstadoPais_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Estados)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkEstadoPais");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pais");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
