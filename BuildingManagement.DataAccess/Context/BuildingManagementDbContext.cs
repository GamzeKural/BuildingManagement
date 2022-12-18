using BuildingManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BuildingManagement.DataAccess.Context
{
    public partial class BuildingManagementDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BuildingManagementDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BuildingManagementDbContext(DbContextOptions<BuildingManagementDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Role> ROLES { get; set; }
        public virtual DbSet<User> USERS { get; set; }
        public virtual DbSet<Apartment> APARTMENTS { get; set; }
        public virtual DbSet<Dues> DUESES { get; set; }
        public virtual DbSet<Message> MESSAGES { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName).HasMaxLength(100);
                entity.Property(x => x.LastName).HasMaxLength(100);
                entity.Property(x => x.IdentityNumber).HasMaxLength(11);
                entity.Property(x => x.Mail).HasMaxLength(150);
                entity.Property(x => x.UserName).HasMaxLength(100);
                entity.Property(x => x.Password).HasMaxLength(100);
                entity.Property(x => x.Phone).HasMaxLength(11);
                entity.Property(x => x.CarInfo).HasMaxLength(50);
                entity.Property(x => x.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Block).HasMaxLength(10);
                entity.Property(x => x.Status).HasMaxLength(10);
                entity.Property(x => x.Type).HasMaxLength(10);
                entity.Property(x => x.FloorLocation).HasMaxLength(10);
                entity.Property(x => x.Number).HasMaxLength(10);
                entity.Property(x => x.UserId);
                entity.Property(x => x.ApartmentInfo).HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Dues>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Type).HasMaxLength(50);
                entity.Property(x => x.Description).HasMaxLength(300);
                entity.Property(x => x.Price).HasMaxLength(15);
                entity.Property(x => x.ApartmentId);
                entity.Property(x => x.CreatedDate);
                entity.Property(x => x.LastPaymentDate);
                entity.Property(x => x.IsPaid);
                entity.Property(x => x.PaidDate).IsRequired(false);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Dueses)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.SenderId).HasColumnName("SenderId");
                entity.Property(x => x.ReceiverId).HasColumnName("ReceiverId");
                entity.Property(x => x.MessageBody).HasMaxLength(500);
                entity.Property(x => x.IsRead);
                entity.Property(x => x.SendDate);

                entity.HasOne(d => d.SenderUser)
                    .WithMany(p => p.SenderMessages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ReceiverUser)
                    .WithMany(p => p.ReceiverMessages)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "Owner"
                },
                new Role
                {
                    Id = 3,
                    Name = "Tenant"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id=1,
                    FirstName="Admin",
                    LastName="Admin",
                    IdentityNumber="12345678901",
                    Mail="admin@gmail.com",
                    UserName="admin",
                    Password="123456",
                    Phone="05554443322",
                    CarInfo="Yok",
                    RoleId=1
                });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
