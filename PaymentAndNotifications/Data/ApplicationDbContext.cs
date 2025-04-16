using Microsoft.EntityFrameworkCore;
using PaymentAndNotifications.Models;

namespace PaymentAndNotifications.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet for Notifications
        public DbSet<Notification> Notifications { get; set; }

        // DbSet for Payments
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for Notification entity
            ConfigureNotificationEntity(modelBuilder);

            // Apply configurations for Payment entity
            ConfigurePaymentEntity(modelBuilder);
        }

        // Configuration for Notification Entity
        private void ConfigureNotificationEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationID); // Primary Key
                entity.Property(n => n.Type)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(n => n.Message)
                      .IsRequired();
                entity.Property(n => n.Status)
                      .IsRequired()
                      .HasMaxLength(10);
                entity.Property(n => n.Timestamp)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()"); // Default to current UTC timestamp
            });
        }

        // Configuration for Payment Entity
        private void ConfigurePaymentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.PaymentID); // Primary Key
                entity.Property(p => p.Amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)"); // Specify precision for monetary values
                entity.Property(p => p.Currency)
                      .IsRequired()
                      .HasMaxLength(10);
                entity.Property(p => p.Status)
                      .IsRequired();
                entity.Property(p => p.CreatedAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()"); // Default to current UTC timestamp
            });
        }
    }
}