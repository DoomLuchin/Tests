using System;
using System.Collections.Generic;
using LogsAllianceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LogsAllianceApi.Data;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Historico> Historicos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=192.168.17.35,11435;Database=logsAlliance;Trusted_Connection=True;TrustServerCertificate=True;user id=UsrPruebAlliance;password=jaemxVAN.V12;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Historico>(entity =>
        {
            entity.ToTable("Historico", "dbo");

            entity.HasIndex(e => e.FechaCreacion, "idx_Historico_fechaCreacion").HasFillFactor(85);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Evento)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("evento");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdRelacionGuid).HasColumnName("idRelacionGuid");
            entity.Property(e => e.IdRelacionVarchar)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("idRelacionVarchar");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("idUsuario");
            entity.Property(e => e.Mensaje)
                .IsUnicode(false)
                .HasColumnName("mensaje");
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
