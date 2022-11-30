using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRestUniversidad.Data
{
    public partial class UniversidadContext : DbContext
    {
     

        public UniversidadContext(DbContextOptions<UniversidadContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<Career> Careers { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Recordinscriptionstudent> Recordinscriptionstudents { get; set; } = null!;
        public virtual DbSet<Recordinscriptionteacher> Recordinscriptionteachers { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            
            modelBuilder.Entity<Career>(entity =>
            {
                entity.HasKey(e => e.IdCareer)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdFacultyNavigation)
                    .WithMany(p => p.Careers)
                    .HasForeignKey(d => d.IdFaculty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Careers_fk0");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.IdFaculty)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRating)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ratings_fk0");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ratings_fk1");
            });

            modelBuilder.Entity<Recordinscriptionstudent>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Recordinscriptionstudents)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordInscriptionStudents_fk0");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Recordinscriptionstudents)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordInscriptionStudents_fk1");
            });

            modelBuilder.Entity<Recordinscriptionteacher>(entity =>
            {
                entity.HasKey(e => e.IdRecord)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Recordinscriptionteachers)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordInscriptionTeachers_fk0");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Recordinscriptionteachers)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordInscriptionTeachers_fk1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdCareerNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.IdCareer)
                    .HasConstraintName("Students_fk0");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubject)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.IdTeacher)
                    .HasName("PRIMARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
