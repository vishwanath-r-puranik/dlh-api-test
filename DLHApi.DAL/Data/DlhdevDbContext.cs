using DLHApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLHApi.DAL.Data;

public partial class DlhdevDbContext : DbContext
{
    

    public DlhdevDbContext(DbContextOptions<DlhdevDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }


    public virtual DbSet<DlhistoryInfo> DlhistoryInfos { get; set; }

    public virtual DbSet<Licence> Licences { get; set; }

    public virtual DbSet<LicenceClass> LicenceClasses { get; set; }

    public virtual DbSet<LicenceCondition> LicenceConditions { get; set; }

    public virtual DbSet<LicenceDetail> LicenceDetails { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId)
                .HasName("PK_CLIENTS")
                .IsClustered(false);

            entity.ToTable("Clients", "DLH");

            entity.HasIndex(e => e.Mvid, "Reference_Client_Licence_MVID_FK");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Mvid)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("MVID");

            entity.HasOne(d => d.Mv).WithMany(p => p.Clients)
                .HasPrincipalKey(p => p.Mvid)
                .HasForeignKey(d => d.Mvid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_LICENCE_MVID");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.CondId)
                .HasName("PK_CONDITION")
                .IsClustered(false);

            entity.ToTable("Condition", "DLH");

            entity.Property(e => e.CondId)
                .ValueGeneratedNever()
                .HasColumnName("CondID");
            entity.Property(e => e.Condition1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Condition");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

       

        modelBuilder.Entity<DlhistoryInfo>(entity =>
        {
            entity.HasKey(e => e.DlhisInfoId)
                .HasName("PK_DLHISTORYINFO")
                .IsClustered(false);

            entity.ToTable("DLHistoryInfo", "DLH");

            entity.HasIndex(e => e.LicClassId, "Reference_Product_LICCLASS_FK");

            entity.HasIndex(e => e.Mvid, "Reference_Product_Licence_MVID_FK");

            entity.Property(e => e.DlhisInfoId)
                .ValueGeneratedNever()
                .HasColumnName("DLHisInfoID");
            entity.Property(e => e.IssueDate).HasColumnType("date");
            entity.Property(e => e.LicClassId).HasColumnName("LicClassID");
            entity.Property(e => e.Mvid)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("MVID");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.LicClass).WithMany(p => p.DlhistoryInfos)
                .HasForeignKey(d => d.LicClassId)
                .HasConstraintName("FK_Product_LICCLASS");

            entity.HasOne(d => d.Mv).WithMany(p => p.DlhistoryInfos)
                .HasPrincipalKey(p => p.Mvid)
                .HasForeignKey(d => d.Mvid)
                .HasConstraintName("FK_Product_CLIENT_MVID");
        });

        modelBuilder.Entity<Licence>(entity =>
        {
            entity.HasKey(e => e.LicenceId)
                .HasName("PK_LICENCE")
                .IsClustered(false);

            entity.ToTable("Licence", "DLH");

            entity.HasIndex(e => e.Mvid, "AK_KEY_MVID").IsUnique();

            entity.Property(e => e.LicenceId)
                .ValueGeneratedNever()
                .HasColumnName("LicenceID");
            entity.Property(e => e.LicNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Mvid)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("MVID");
        });

        modelBuilder.Entity<LicenceClass>(entity =>
        {
            entity.HasKey(e => e.LicClassId)
                .HasName("PK_LICENCECLASS")
                .IsClustered(false);

            entity.ToTable("LicenceClass", "DLH");

            entity.HasIndex(e => e.LicClass, "Reference_Product_Licence_MVID_FK");

            entity.Property(e => e.LicClassId)
                .ValueGeneratedNever()
                .HasColumnName("LicClassID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LicClass)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LicenceCondition>(entity =>
        {
            entity.HasKey(e => e.LicCondId)
                .HasName("PK_LICENCECONDITION")
                .IsClustered(false);

            entity.ToTable("LicenceCondition", "DLH");

            entity.HasIndex(e => e.CondId, "Reference_LicCond_Condition_FK");

            entity.HasIndex(e => e.LicCond, "Reference_LicCond_LicDetails_FK");

            entity.Property(e => e.LicCondId)
                .ValueGeneratedNever()
                .HasColumnName("LicCondID");
            entity.Property(e => e.CondId).HasColumnName("CondID");

            entity.HasOne(d => d.Cond).WithMany(p => p.LicenceConditions)
                .HasForeignKey(d => d.CondId)
                .HasConstraintName("FK_LicCond_COND");

            entity.HasOne(d => d.LicCondNavigation).WithMany(p => p.LicenceConditions)
                .HasPrincipalKey(p => p.LicCond)
                .HasForeignKey(d => d.LicCond)
                .HasConstraintName("FK_LicCond_LICDETAILS");
        });

        modelBuilder.Entity<LicenceDetail>(entity =>
        {
            entity.HasKey(e => e.LicDetailId)
                .HasName("PK_LICENCEDETAILS")
                .IsClustered(false);

            entity.ToTable("LicenceDetails", "DLH");

            entity.HasIndex(e => e.LicCond, "AK_KEY_LICCOND").IsUnique();

            entity.HasIndex(e => e.LicClassId, "Reference_LicDetails_LICCLASS_FK");

            entity.HasIndex(e => e.Mvid, "Reference_LicDetails_Licence_MVID_FK");

            entity.Property(e => e.LicDetailId)
                .ValueGeneratedNever()
                .HasColumnName("LicDetailID");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.GdlexitDate)
                .HasColumnType("date")
                .HasColumnName("GDLExitDate");
            entity.Property(e => e.Gdlstatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("GDLStatus");
            entity.Property(e => e.IssueDate).HasColumnType("date");
            entity.Property(e => e.LicClassId).HasColumnName("LicClassID");
            entity.Property(e => e.Mvid)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("MVID");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.LicClass).WithMany(p => p.LicenceDetails)
                .HasForeignKey(d => d.LicClassId)
                .HasConstraintName("FK_LicDetails_LICCLASS");

            entity.HasOne(d => d.Mv).WithMany(p => p.LicenceDetails)
                .HasPrincipalKey(p => p.Mvid)
                .HasForeignKey(d => d.Mvid)
                .HasConstraintName("FK_LicDetails_LICENCE_MVID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}