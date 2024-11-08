using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace server.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Metric> Metrics { get; set; }

    public virtual DbSet<MetricsLineOfCodeInterval> MetricsLineOfCodeIntervals { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectMetric> ProjectMetrics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserKey> UserKeys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\Capitan\\Desktop\\KITP-4-course\\TRPO\\TRPO_Sharapova_Lab7\\GertrudeSimpleDemo\\gertrudeDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<History>(entity =>
        {
            entity.ToTable("History");

            entity.Property(e => e.HistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("HistoryID");
            entity.Property(e => e.Project1Id).HasColumnName("Project1ID");
            entity.Property(e => e.Project2Id).HasColumnName("Project2ID");

            entity.HasOne(d => d.Project1).WithMany(p => p.HistoryProject1s)
                .HasForeignKey(d => d.Project1Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Project2).WithMany(p => p.HistoryProject2s)
                .HasForeignKey(d => d.Project2Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("LanguageID");
        });

        modelBuilder.Entity<Metric>(entity =>
        {
            entity.HasKey(e => e.MetricsId);

            entity.Property(e => e.MetricsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MetricsID");
            entity.Property(e => e.IsIgnored).HasColumnType("BOOLEAN");
            entity.Property(e => e.MetricTypeId).HasColumnName("MetricTypeID");
        });

        modelBuilder.Entity<MetricsLineOfCodeInterval>(entity =>
        {
            entity.ToTable("MetricsLineOfCodeInterval");

            entity.Property(e => e.MetricsLineOfCodeIntervalId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MetricsLineOfCodeIntervalID");
            entity.Property(e => e.LineOfCodeIntervalId).HasColumnName("LineOfCodeIntervalID");
            entity.Property(e => e.MetricId).HasColumnName("MetricID");

            entity.HasOne(d => d.Metric).WithMany(p => p.MetricsLineOfCodeIntervals)
                .HasForeignKey(d => d.MetricId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.ProjectId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProjectID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.MetricsId).HasColumnName("MetricsID");

            entity.HasOne(d => d.Language).WithMany(p => p.Projects)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Metrics).WithMany(p => p.Projects)
                .HasForeignKey(d => d.MetricsId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ProjectMetric>(entity =>
        {
            entity.HasKey(e => e.ProjectMetricsId);

            entity.Property(e => e.ProjectMetricsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProjectMetricsID");
            entity.Property(e => e.MetricsId).HasColumnName("MetricsID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Metrics).WithMany(p => p.ProjectMetrics)
                .HasForeignKey(d => d.MetricsId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMetrics)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<UserKey>(entity =>
        {
            entity.ToTable("UserKey");

            entity.Property(e => e.UserKeyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserKeyID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
