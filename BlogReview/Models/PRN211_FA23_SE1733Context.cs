using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogReview.Models
{
    public partial class PRN211_FA23_SE1733Context : DbContext
    {
        public PRN211_FA23_SE1733Context()
        {
        }

        public PRN211_FA23_SE1733Context(DbContextOptions<PRN211_FA23_SE1733Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogHe173248> BlogHe173248s { get; set; } = null!;
        public virtual DbSet<BlogStatusHe173248> BlogStatusHe173248s { get; set; } = null!;
        public virtual DbSet<CommentHe173248> CommentHe173248s { get; set; } = null!;
        public virtual DbSet<LocaBlogHe173248> LocaBlogHe173248s { get; set; } = null!;
        public virtual DbSet<LocationHe173248> LocationHe173248s { get; set; } = null!;
        public virtual DbSet<MainConBlogHe173248> MainConBlogHe173248s { get; set; } = null!;
        public virtual DbSet<MainContentHe173248> MainContentHe173248s { get; set; } = null!;
        public virtual DbSet<NotificationHe173248> NotificationHe173248s { get; set; } = null!;
        public virtual DbSet<RateHe173248> RateHe173248s { get; set; } = null!;
        public virtual DbSet<RoleHe173248> RoleHe173248s { get; set; } = null!;
        public virtual DbSet<TagDraftHe173248> TagDraftHe173248s { get; set; } = null!;
        public virtual DbSet<UserHe173248> UserHe173248s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PRN211_FA23_SE1733;User ID=sa;Password=12345678;Encrypt=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogHe173248>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("BlogHE173248_pk");

                entity.ToTable("BlogHE173248");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.AverRate).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiOn).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.BlogHe173248s)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Blog_BlogStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BlogHe173248s)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BlogHE173248_UserHE173248");
            });

            modelBuilder.Entity<BlogStatusHe173248>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("BlogStatusHE173248_pk");

                entity.ToTable("BlogStatusHE173248");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<CommentHe173248>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("CommentHE173248_pk");

                entity.ToTable("CommentHE173248");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.CommentHe173248s)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Blog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentHe173248s)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_User");
            });

            modelBuilder.Entity<LocaBlogHe173248>(entity =>
            {
                entity.HasKey(e => e.LocalBlogId)
                    .HasName("LocaBlogHE173248_pk");

                entity.ToTable("LocaBlogHE173248");

                entity.Property(e => e.LocalBlogId).HasColumnName("LocalBlogID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.LocaId).HasColumnName("LocaID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.LocaBlogHe173248s)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LocaBlog_Blog");

                entity.HasOne(d => d.Loca)
                    .WithMany(p => p.LocaBlogHe173248s)
                    .HasForeignKey(d => d.LocaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LocaBlog_Location");
            });

            modelBuilder.Entity<LocationHe173248>(entity =>
            {
                entity.HasKey(e => e.LocaId)
                    .HasName("LocationHE173248_pk");

                entity.ToTable("LocationHE173248");

                entity.Property(e => e.LocaId).HasColumnName("LocaID");
            });

            modelBuilder.Entity<MainConBlogHe173248>(entity =>
            {
                entity.HasKey(e => e.MainConBlogId)
                    .HasName("MainConBlogHE173248_pk");

                entity.ToTable("MainConBlogHE173248");

                entity.Property(e => e.MainConBlogId).HasColumnName("MainConBlogID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.MainConId).HasColumnName("MainConID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.MainConBlogHe173248s)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MainConBlog_Blog");

                entity.HasOne(d => d.MainCon)
                    .WithMany(p => p.MainConBlogHe173248s)
                    .HasForeignKey(d => d.MainConId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MainConBlog_MainContent");
            });

            modelBuilder.Entity<MainContentHe173248>(entity =>
            {
                entity.HasKey(e => e.MainConId)
                    .HasName("MainContentHE173248_pk");

                entity.ToTable("MainContentHE173248");

                entity.Property(e => e.MainConId).HasColumnName("MainConID");
            });

            modelBuilder.Entity<NotificationHe173248>(entity =>
            {
                entity.HasKey(e => e.NotifId)
                    .HasName("NotificationHE173248_pk");

                entity.ToTable("NotificationHE173248");

                entity.Property(e => e.NotifId).HasColumnName("NotifID");

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NotificationHe173248s)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Notification_User");
            });

            modelBuilder.Entity<RateHe173248>(entity =>
            {
                entity.HasKey(e => e.RateId)
                    .HasName("RateHE173248_pk");

                entity.ToTable("RateHE173248");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.RateHe173248s)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rate_Blog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RateHe173248s)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rate_User");
            });

            modelBuilder.Entity<RoleHe173248>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("RoleHE173248_pk");

                entity.ToTable("RoleHE173248");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(25);
            });

            modelBuilder.Entity<TagDraftHe173248>(entity =>
            {
                entity.HasKey(e => e.TagDraftId)
                    .HasName("TagDraftHE173248_pk");

                entity.ToTable("TagDraftHE173248");

                entity.Property(e => e.TagDraftId).HasColumnName("TagDraftID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TagDraftHe173248s)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TagDraftHE173248_BlogHE173248");
            });

            modelBuilder.Entity<UserHe173248>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("UserHE173248_pk");

                entity.ToTable("UserHE173248");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserHe173248s)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
