using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BTLon.Models
{
    public partial class BTLonContext : DbContext
    {
        public BTLonContext()
        {
        }

        public BTLonContext(DbContextOptions<BTLonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<CaThi> CaThi { get; set; }
        public virtual DbSet<CaThiMonThi> CaThiMonThi { get; set; }
        public virtual DbSet<DiaDiem> DiaDiem { get; set; }
        public virtual DbSet<KyThi> KyThi { get; set; }
        public virtual DbSet<MonThi> MonThi { get; set; }
        public virtual DbSet<PhongThi> PhongThi { get; set; }
        public virtual DbSet<SvDiaDiem> SvDiaDiem { get; set; }
        public virtual DbSet<SvMonThiKiThi> SvMonThiKiThi { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=VUANHPC\\SQLEXPRESS;Initial Catalog=BTLon2;Integrated Security=True");
				//Data Source=VUANHPC\\SQLEXPRESS;Initial Catalog=BTLon2;Integrated Security=True
				//Data Source=VUANHLAPTOP\\SQLEXPRESS;Initial Catalog=BTLon2;Integrated Security=True
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CaThi>(entity =>
            {
                entity.Property(e => e.CaThiId)
                    .HasColumnName("CaThiID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CaThiIdFake)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CaThiName).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.KyThiId).HasColumnName("KyThiID");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.Stop).HasColumnType("datetime");

                entity.HasOne(d => d.KyThi)
                    .WithMany(p => p.CaThi)
                    .HasForeignKey(d => d.KyThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_CaThi_KyThi");
            });

            modelBuilder.Entity<CaThiMonThi>(entity =>
            {
                entity.HasKey(e => e.CaMtId);

                entity.ToTable("CaThi_MonThi");

                entity.Property(e => e.CaMtId)
                    .HasColumnName("CaMtID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CaThiId).HasColumnName("CaThiID");

                entity.Property(e => e.MonThiId).HasColumnName("MonThiID");

                entity.HasOne(d => d.CaThi)
                    .WithMany(p => p.CaThiMonThi)
                    .HasForeignKey(d => d.CaThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CaThi_MonThi_CaThi");

                entity.HasOne(d => d.MonThi)
                    .WithMany(p => p.CaThiMonThi)
                    .HasForeignKey(d => d.MonThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CaThi_MonThi_MonThi");
            });

            modelBuilder.Entity<DiaDiem>(entity =>
            {
                entity.Property(e => e.DiaDiemId)
                    .HasColumnName("DiaDiemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CaMtId).HasColumnName("CaMtID");

                entity.Property(e => e.PhongThiId).HasColumnName("PhongThiID");

                entity.HasOne(d => d.CaMt)
                    .WithMany(p => p.DiaDiem)
                    .HasForeignKey(d => d.CaMtId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DiaDiem_CaThi_MonThi");

                entity.HasOne(d => d.PhongThi)
                    .WithMany(p => p.DiaDiem)
                    .HasForeignKey(d => d.PhongThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DiaDiem_PhongThi");
            });

            modelBuilder.Entity<KyThi>(entity =>
            {
                entity.Property(e => e.KyThiId)
                    .HasColumnName("KyThiID")
                    .ValueGeneratedNever();

                entity.Property(e => e.KyThiIdFake)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.KyThiName).HasMaxLength(50);
            });

            modelBuilder.Entity<MonThi>(entity =>
            {
                entity.Property(e => e.MonThiId)
                    .HasColumnName("MonThiID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GiaoVien).HasMaxLength(50);

                entity.Property(e => e.MonThiIdFake)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MonThiName).HasMaxLength(50);
            });

            modelBuilder.Entity<PhongThi>(entity =>
            {
                entity.Property(e => e.PhongThiId)
                    .HasColumnName("PhongThiID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PhongThiName).HasMaxLength(50);
            });

            modelBuilder.Entity<SvDiaDiem>(entity =>
            {
				entity.HasKey(e => new { e.DiaDiemId, e.UserId });

				entity.ToTable("SV_DiaDiem");

                entity.Property(e => e.DiaDiemId).HasColumnName("DiaDiemID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.DiaDiem)
                    .WithMany(p => p.SvDiaDiem)
                    .HasForeignKey(d => d.DiaDiemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_SVDD_DiaDiem");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SvDiaDiem)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_SVDD_SV");
            });

            modelBuilder.Entity<SvMonThiKiThi>(entity =>
            {
				entity.HasKey(e => new { e.KyThiId, e.MonThiId, e.UserId });

				entity.ToTable("SV_MonThi_KiThi");

                entity.Property(e => e.KyThiId).HasColumnName("KyThiID");

                entity.Property(e => e.MonThiId).HasColumnName("MonThiID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.KyThi)
                    .WithMany(p => p.SvMonThiKiThi)
                    .HasForeignKey(d => d.KyThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SV_MonThi_KiThi_KyThi");

                entity.HasOne(d => d.MonThi)
                    .WithMany(p => p.SvMonThiKiThi)
                    .HasForeignKey(d => d.MonThiId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SV_MonThi_KiThi_MonThi");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SvMonThiKiThi)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SV_MonThi_KiThi_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.UserIdfake)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
