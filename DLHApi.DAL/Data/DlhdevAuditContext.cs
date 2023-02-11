using DLHApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLHApi.DAL.Data;

public partial class DlhdevAuditContext : DbContext
{
   

    public DlhdevAuditContext(DbContextOptions<DlhdevAuditContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DlhRequestAudit> DlhRequestAudits { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DlhRequestAudit>(entity =>
        {
         entity.HasKey(e => e.RequestId);

            entity.ToTable("DlhRequestAudit");

            entity.Property(e => e.RequestId)
               .HasMaxLength(20)
               .HasColumnName("RequestId");
            entity.Property(e => e.Mvid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MVid");
            entity.Property(e => e.RecordStatus)
                .HasMaxLength(50)
                .HasColumnName("RecordStatus");
            entity.Property(e => e.MOVESTxServiceNo)
                .HasMaxLength(50)
                .HasColumnName("MOVESTxServiceNo");
            entity.Property(e => e.ROADSUserID)
                 .HasMaxLength(50)
                 .HasColumnName("ROADSUserID");
            entity.Property(e => e.MOVESSessionID)
                .HasMaxLength(50)
                .HasColumnName("MOVESSessionID");
            entity.Property(e => e.RequestDateTimeStamp).HasColumnType("datetime");
            entity.Property(e => e.PaymentDateTimeStamp).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
