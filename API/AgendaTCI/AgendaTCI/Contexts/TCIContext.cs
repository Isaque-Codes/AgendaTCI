using System;
using System.Collections.Generic;
using AgendaTCI.Domains;
using Microsoft.EntityFrameworkCore;

namespace AgendaTCI.Contexts;

public partial class TCIContext : DbContext
{
    public TCIContext()
    {
    }

    public TCIContext(DbContextOptions<TCIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agenda> Agenda { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agenda>(entity =>
        {
            entity.HasKey(e => e.PacienteID);

            entity.ToTable(tb => tb.HasTrigger("TR_Agenda_AtualizaDataAlteracao"));

            entity.HasIndex(e => e.CPF, "UQ_Agenda_CPF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ_Agenda_Email").IsUnique();

            entity.Property(e => e.PacienteID).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Apelido).HasMaxLength(50);
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DataCadastro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DataUltimaAlteracao).HasPrecision(0);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(75);
            entity.Property(e => e.Telefone)
                .HasMaxLength(13)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
