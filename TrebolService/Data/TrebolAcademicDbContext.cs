using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TrebolService.Models;

namespace TrebolService.Data;

public partial class TrebolAcademicDbContext : DbContext
{
    public TrebolAcademicDbContext()
    {
    }

    public TrebolAcademicDbContext(DbContextOptions<TrebolAcademicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Afinidade> Afinidades { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Grimorio> Grimorios { get; set; }

    public virtual DbSet<GrimorioAsignado> GrimorioAsignados { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string cadena = "";
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
        IConfiguration _configuration = builder.Build();
        cadena = _configuration.GetConnectionString("Local");

        SqlConnectionStringBuilder a = new SqlConnectionStringBuilder(cadena);
        optionsBuilder.UseSqlServer(a.ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Afinidade>(entity =>
        {
            entity.ToTable("afinidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaOp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fechaOp");
            entity.Property(e => e.UsuarioOp).HasColumnName("usuarioOp");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.ToTable("estudiantes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaOp)
                .HasColumnType("date")
                .HasColumnName("fechaOp");
            entity.Property(e => e.IdAfinidad).HasColumnName("idAfinidad");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioOp).HasColumnName("usuarioOp");
        });

        modelBuilder.Entity<Grimorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nivelGrimorios");

            entity.ToTable("grimorios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaOp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fechaOp");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumHojas).HasColumnName("numHojas");
            entity.Property(e => e.UsuarioOp).HasColumnName("usuarioOp");
        });

        modelBuilder.Entity<GrimorioAsignado>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);

            entity.ToTable("grimorioAsignado");

            entity.HasIndex(e => e.Id, "IX_grimorioAsignado");

            entity.Property(e => e.IdEstudiante).ValueGeneratedNever();
            entity.Property(e => e.FechaUpdate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdEstudianteNavigation).WithOne(p => p.GrimorioAsignado)
                .HasForeignKey<GrimorioAsignado>(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_grimorioAsignado_estudiantes");

            entity.HasOne(d => d.IdGrimorioNavigation).WithMany(p => p.GrimorioAsignados)
                .HasForeignKey(d => d.IdGrimorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_grimorioAsignado_grimorios");
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Fechaop)
                .HasColumnType("datetime")
                .HasColumnName("fechaop");
            entity.Property(e => e.IdAfinidad).HasColumnName("idAfinidad");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Motivo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("motivo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Status solicitud  PENDIENTE - ACEPTADA")
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
