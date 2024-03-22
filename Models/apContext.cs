using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentoOborotWpfApp.Models
{
    public partial class apContext : DbContext
    {
        public apContext()
        {
        }

        public apContext(DbContextOptions<apContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dirdoc> Dirdocs { get; set; } = null!;
        public virtual DbSet<Directory> Directories { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Notifsend> Notifsends { get; set; } = null!;
        public virtual DbSet<Notifstatus> Notifstatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sending> Sendings { get; set; } = null!;
        public virtual DbSet<Sendstatus> Sendstatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ap;Username=root;Password=root");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("pol", new[] { "Male", "Female", "Undefined" });

            modelBuilder.Entity<Dirdoc>(entity =>
            {
                entity.ToTable("dirdoc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkDir).HasColumnName("fk_dir");

                entity.Property(e => e.FkDoc).HasColumnName("fk_doc");

                entity.HasOne(d => d.FkDirNavigation)
                    .WithMany(p => p.Dirdocs)
                    .HasForeignKey(d => d.FkDir)
                    .HasConstraintName("dirdoc_fk_dir_fkey");

                entity.HasOne(d => d.FkDocNavigation)
                    .WithMany(p => p.Dirdocs)
                    .HasForeignKey(d => d.FkDoc)
                    .HasConstraintName("dirdoc_fk_doc_fkey");
            });

            modelBuilder.Entity<Directory>(entity =>
            {
                entity.ToTable("directory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DirName)
                    .HasMaxLength(100)
                    .HasColumnName("dir_name");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocByte).HasColumnName("doc_byte");

                entity.Property(e => e.DocName)
                    .HasMaxLength(120)
                    .HasColumnName("doc_name");
            });

            modelBuilder.Entity<Notifsend>(entity =>
            {
                entity.ToTable("notifsend");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Comments)
                    .HasMaxLength(400)
                    .HasColumnName("comments");

                entity.Property(e => e.FkRecUser).HasColumnName("fk_rec_user");

                entity.Property(e => e.FkSend).HasColumnName("fk_send");

                entity.Property(e => e.FkSendUser).HasColumnName("fk_send_user");

                entity.Property(e => e.FkStatusNotif).HasColumnName("fk_status_notif");

                entity.HasOne(d => d.FkRecUserNavigation)
                    .WithMany(p => p.NotifsendFkRecUserNavigations)
                    .HasForeignKey(d => d.FkRecUser)
                    .HasConstraintName("notifsend_fk_rec_user_fkey");

                entity.HasOne(d => d.FkSendNavigation)
                    .WithMany(p => p.Notifsends)
                    .HasForeignKey(d => d.FkSend)
                    .HasConstraintName("notifsend_fk_send_fkey");

                entity.HasOne(d => d.FkSendUserNavigation)
                    .WithMany(p => p.NotifsendFkSendUserNavigations)
                    .HasForeignKey(d => d.FkSendUser)
                    .HasConstraintName("notifsend_fk_send_user_fkey");

                entity.HasOne(d => d.FkStatusNotifNavigation)
                    .WithMany(p => p.Notifsends)
                    .HasForeignKey(d => d.FkStatusNotif)
                    .HasConstraintName("notifsend_fk_status_notif_fkey");
            });

            modelBuilder.Entity<Notifstatus>(entity =>
            {
                entity.ToTable("notifstatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NotifstatusName)
                    .HasMaxLength(120)
                    .HasColumnName("notifstatus_name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Sending>(entity =>
            {
                entity.ToTable("sending");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentSend)
                    .HasMaxLength(400)
                    .HasColumnName("comment_send");

                entity.Property(e => e.FkDoc).HasColumnName("fk_doc");

                entity.Property(e => e.FkRecUser).HasColumnName("fk_rec_user");

                entity.Property(e => e.FkSendUser).HasColumnName("fk_send_user");

                entity.Property(e => e.FkStatus).HasColumnName("fk_status");


                entity.HasOne(d => d.FkDocNavigation)
                    .WithMany(p => p.Sendings)
                    .HasForeignKey(d => d.FkDoc)
                    .HasConstraintName("sending_fk_doc_fkey");

                entity.HasOne(d => d.FkRecUserNavigation)
                    .WithMany(p => p.SendingFkRecUserNavigations)
                    .HasForeignKey(d => d.FkRecUser)
                    .HasConstraintName("sending_fk_rec_user_fkey");

                entity.HasOne(d => d.FkSendUserNavigation)
                    .WithMany(p => p.SendingFkSendUserNavigations)
                    .HasForeignKey(d => d.FkSendUser)
                    .HasConstraintName("sending_fk_send_user_fkey");

                entity.HasOne(d => d.FkStatusNavigation)
                    .WithMany(p => p.Sendings)
                    .HasForeignKey(d => d.FkStatus)
                    .HasConstraintName("sending_fk_status_fkey");
            });

            modelBuilder.Entity<Sendstatus>(entity =>
            {
                entity.ToTable("sendstatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SendstatusName)
                    .HasMaxLength(120)
                    .HasColumnName("sendstatus_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "users_login_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .HasMaxLength(120)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(120)
                    .HasColumnName("firstname");

                entity.Property(e => e.FkRole).HasColumnName("fk_role");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(120)
                    .HasColumnName("lastname");

                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .HasColumnName("login");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(120)
                    .HasColumnName("middlename");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.HasOne(d => d.FkRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkRole)
                    .HasConstraintName("users_fk_role_fkey");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
