using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ComicHoarder.Infrastructure.Models;

namespace ComicHoarder.Infrastructure;

public partial class CHContext : DbContext
{
    public CHContext()
    {
    }

    public CHContext(DbContextOptions<CHContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CollectedIssueEntity> CollectedIssues { get; set; }

    public virtual DbSet<ComicEventReadingOrderWithComicPath> ComicEventReadingOrderWithComicPaths { get; set; }

    public virtual DbSet<ComicEventsWithIssueCount> ComicEventsWithIssueCounts { get; set; }

    public virtual DbSet<ComicIssue> ComicIssues { get; set; }

    public virtual DbSet<ComicIssuesToCollect> ComicIssuesToCollects { get; set; }

    public virtual DbSet<ComicIssuesToCollectCountByPublisher> ComicIssuesToCollectCountByPublishers { get; set; }

    public virtual DbSet<ComicIssuesToCollectWithLink> ComicIssuesToCollectWithLinks { get; set; }

    public virtual DbSet<EventEntity> Events { get; set; }

    public virtual DbSet<EventIssueEntity> EventIssues { get; set; }

    public virtual DbSet<EventIssueTypeEntity> EventIssueTypes { get; set; }

    public virtual DbSet<EventTypeEntity> EventTypes { get; set; }

    public virtual DbSet<IssueEntity> Issues { get; set; }

    public virtual DbSet<IssueFormatEntity> IssueFormats { get; set; }

    public virtual DbSet<PublisherEntity> Publishers { get; set; }

    public virtual DbSet<SettingEntity> Settings { get; set; }

    public virtual DbSet<VolumeEntity> Volumes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.155.101;Database=ComicHoarder;Uid=sa;Pwd=Harobikes@33;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollectedIssueEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_issueissue");

            entity.ToTable("CollectedIssue");

            entity.HasOne(d => d.Child).WithMany(p => p.CollectedIssueChildren)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_child");

            entity.HasOne(d => d.Parent).WithMany(p => p.CollectedIssueParents)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_parent");
        });

        modelBuilder.Entity<ComicEventReadingOrderWithComicPath>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicEventReadingOrderWithComicPath");

            entity.Property(e => e.Filename)
                .HasMaxLength(506)
                .HasColumnName("filename");
            entity.Property(e => e.Issue)
                .HasMaxLength(33)
                .IsUnicode(false);
            entity.Property(e => e.VolumeName).HasMaxLength(100);
        });

        modelBuilder.Entity<ComicEventsWithIssueCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicEventsWithIssueCount");

            entity.Property(e => e.PublisherName).HasMaxLength(100);
        });

        modelBuilder.Entity<ComicIssue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicIssues");

            entity.Property(e => e.IssueName).HasMaxLength(100);
            entity.Property(e => e.IssueNumberSuffix)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PublisherName).HasMaxLength(100);
            entity.Property(e => e.VolumeName).HasMaxLength(100);
        });

        modelBuilder.Entity<ComicIssuesToCollect>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicIssuesToCollect");

            entity.Property(e => e.IssueName).HasMaxLength(100);
            entity.Property(e => e.IssueNumberSuffix)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PublisherName).HasMaxLength(100);
            entity.Property(e => e.Volume).HasMaxLength(100);
        });

        modelBuilder.Entity<ComicIssuesToCollectCountByPublisher>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicIssuesToCollectCountByPublisher");

            entity.Property(e => e.Publisher).HasMaxLength(100);
        });

        modelBuilder.Entity<ComicIssuesToCollectWithLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ComicIssuesToCollectWithLinks");

            entity.Property(e => e.IssueName).HasMaxLength(100);
            entity.Property(e => e.IssueNumberSuffix)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(58)
                .IsUnicode(false);
            entity.Property(e => e.PublisherName).HasMaxLength(100);
            entity.Property(e => e.Volume).HasMaxLength(100);
        });

        modelBuilder.Entity<EventEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_event");

            entity.ToTable("Event");

            entity.Property(e => e.Enabled).HasDefaultValue(true);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Events)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_event_publisher");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_eventtype");
        });

        modelBuilder.Entity<EventIssueEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_eventcomic");

            entity.ToTable("EventIssue");

            entity.HasOne(d => d.Event).WithMany(p => p.EventIssues)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_event");

            entity.HasOne(d => d.Issue).WithMany(p => p.EventIssues)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comic");
        });

        modelBuilder.Entity<EventIssueTypeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_eventissuetype");

            entity.ToTable("EventIssueType");
        });

        modelBuilder.Entity<EventTypeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_eventtype");

            entity.ToTable("EventType");
        });

        modelBuilder.Entity<IssueEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_issue");

            entity.ToTable("Issue");

            entity.HasIndex(e => e.Collected, "IX_Issue_Collected");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IssueNumberSuffix)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Reprint).HasDefaultValue(false);

            entity.HasOne(d => d.Format).WithMany(p => p.Issues)
                .HasForeignKey(d => d.FormatId)
                .HasConstraintName("issue_issueformat");

            entity.HasOne(d => d.Volume).WithMany(p => p.Issues)
                .HasForeignKey(d => d.VolumeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_volume");
        });

        modelBuilder.Entity<IssueFormatEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_issueformat");

            entity.ToTable("IssueFormat");
        });

        modelBuilder.Entity<PublisherEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_publisher");

            entity.ToTable("Publisher");

            entity.HasIndex(e => e.Id, "IX_Publisher_Id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SettingEntity>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<VolumeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_volume");

            entity.ToTable("Volume");

            entity.HasIndex(e => e.Id, "IX_Volume_Id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateLastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Volumes)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_publisher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
