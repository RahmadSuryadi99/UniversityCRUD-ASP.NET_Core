using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class IGLOUniversityContext : DbContext
    {
        public IGLOUniversityContext()
        {
        }

        public IGLOUniversityContext(DbContextOptions<IGLOUniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DistribusiMatakuliah> DistribusiMatakuliahs { get; set; }
        public virtual DbSet<Kela> Kelas { get; set; }
        public virtual DbSet<Mahasiswa> Mahasiswas { get; set; }
        public virtual DbSet<Matakuliah> Matakuliahs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-J9DU5J54;Database=IGLOUniversity;TrustServerCertificate = True; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DistribusiMatakuliah>(entity =>
            {
                entity.ToTable("DistribusiMatakuliah");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdKelasNavigation)
                    .WithMany(p => p.DistribusiMatakuliahs)
                    .HasForeignKey(d => d.IdKelas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistribusiMatakuliah_Kelas");

                entity.HasOne(d => d.IdMahasiswaNavigation)
                    .WithMany(p => p.DistribusiMatakuliahs)
                    .HasForeignKey(d => d.IdMahasiswa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistribusiMatakuliah_Mahasiswa");
            });

            modelBuilder.Entity<Kela>(entity =>
            {
                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Semester)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMatakuliahNavigation)
                    .WithMany(p => p.Kelas)
                    .HasForeignKey(d => d.IdMatakuliah)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kelas_Matakuliah");
            });

            modelBuilder.Entity<Mahasiswa>(entity =>
            {
                entity.ToTable("Mahasiswa");

                entity.Property(e => e.Alamat)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AsalSma)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AsalSMA");

                entity.Property(e => e.NamaBelakang)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NamaDepan)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NamaTengah)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nim)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NIM");

                entity.Property(e => e.NomorHp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NomorHP");
            });

            modelBuilder.Entity<Matakuliah>(entity =>
            {
                entity.ToTable("Matakuliah");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sks).HasColumnName("SKS");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.MahasiswaId).HasColumnName("MahasiswaID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Mahasiswa)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.MahasiswaId)
                    .HasConstraintName("FK_User_Mahasiswa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
