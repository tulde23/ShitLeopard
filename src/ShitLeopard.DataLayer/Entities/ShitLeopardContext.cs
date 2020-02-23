using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShitLeopard.DataLayer.Entities
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

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<CharacterTags> CharacterTags { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }
        public virtual DbSet<Script> Script { get; set; }
        public virtual DbSet<ScriptLine> ScriptLine { get; set; }
        public virtual DbSet<ScriptWord> ScriptWord { get; set; }
        public virtual DbSet<Season> Season { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                OnConfiguringPartial(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Aliases)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PlayedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CharacterTags>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Synopsis).IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Episode)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_Episode_Season");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.Property(e => e.Body)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
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

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.ScriptLine)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_ScriptLine_Character");

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

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Tags");

                entity.HasIndex(e => new { e.Name, e.Category })
                    .HasName("IX_Tags_1");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        partial void OnConfiguringPartial(DbContextOptionsBuilder optionsBuilder);
    }
}
