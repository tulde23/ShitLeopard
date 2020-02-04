﻿using Microsoft.EntityFrameworkCore;

namespace ShitLeopard.Entities
{
    public partial class ShitLeopardContext : DbContext
    {
        public ShitLeopardContext()
        {
        }

        public ShitLeopardContext(DbContextOptions<ShitLeopardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<Script> Script { get; set; }
        public virtual DbSet<ScriptLine> ScriptLine { get; set; }
        public virtual DbSet<ScriptWord> ScriptWord { get; set; }
        public virtual DbSet<Season> Season { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;User Id=sa;Password=Tulde30#;Database=ShitLeopard");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Episode)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_Episode_Season");
            });

            modelBuilder.Entity<Script>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Body).IsRequired();

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.Script)
                    .HasForeignKey(d => d.EpisodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Script_Episode");
            });

            modelBuilder.Entity<ScriptLine>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Body).IsRequired();

                entity.HasOne(d => d.Script)
                    .WithMany(p => p.ScriptLine)
                    .HasForeignKey(d => d.ScriptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScriptLine_Script");
            });

            modelBuilder.Entity<ScriptWord>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ScriptLine)
                    .WithMany(p => p.ScriptWord)
                    .HasForeignKey(d => d.ScriptLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScriptWord_ScriptLine");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}