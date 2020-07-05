using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ZipProject.Model
{
    public partial class zip_dbContext : DbContext
    {
        public zip_dbContext()
        {
        }

        public zip_dbContext(DbContextOptions<zip_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountModel> AccountModel { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.HasKey(e => e.AccountOwner);

                entity.Property(e => e.AccountOwner)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountOwnerNavigation)
                    .WithOne(p => p.AccountModel)
                    .HasForeignKey<AccountModel>(d => d.AccountOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountModel_UserModel");
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.EmailAddress);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
