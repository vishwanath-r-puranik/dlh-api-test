using System;
using System.Collections.Generic;
using DLHAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DLHAPI.Data;

public partial class DlhdevAuditContext : DbContext
{
    public DlhdevAuditContext()
    {
    }

    public DlhdevAuditContext(DbContextOptions<DlhdevAuditContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DlhRequest> DlhRequests { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LGS9143;Database=DLHDevAudit;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DlhRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DlhReque__3214EC0713586FB1");

            entity.ToTable("DlhRequest");

            entity.Property(e => e.Mvid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MVid");
            entity.Property(e => e.ReqStatus)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
