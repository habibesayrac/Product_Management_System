using Microsoft.EntityFrameworkCore;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=PMSdb;user=root;password=root;Max Pool Size=200;Min Pool Size=10;Pooling=true");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Product>().HasKey(x => x.ProductId);
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<Order>().HasKey(x => x.OrderId);


            modelBuilder.Entity<UserProduct>().HasKey(ky => new { ky.UserId, ky.ProductId });

            modelBuilder.Entity<UserProduct>().HasOne(x => x.Users).WithMany(x => x.UserProducts).HasForeignKey(x => x.UserId).HasConstraintName("FK_UserProduct_UserId");
            modelBuilder.Entity<UserProduct>().HasOne(x => x.Products).WithMany(x => x.UserProducts).HasForeignKey(x => x.ProductId).HasConstraintName("FK_UserProduct_ProductId");


            modelBuilder.Entity<OrderProduct>().HasKey(ky => new { ky.OrderId, ky.ProductId });

            modelBuilder.Entity<OrderProduct>().HasOne(y => y.Orders).WithMany(y => y.OrderProducts).HasForeignKey(y => y.OrderId).HasConstraintName("FK_OrderProduct_OrderId");
            modelBuilder.Entity<OrderProduct>().HasOne(y => y.Products).WithMany(y => y.OrderProducts).HasForeignKey(y => y.ProductId).HasConstraintName("FK_OrderProduct_ProductId");
        }
    }
}