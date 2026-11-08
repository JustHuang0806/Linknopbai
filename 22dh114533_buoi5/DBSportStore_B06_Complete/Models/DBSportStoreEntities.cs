using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DBSportStore.Models
{
    // EF6 context mapped to the existing DBSportStore SQL Server database.
    public partial class DBSportStoreEntities : DbContext
    {
        public DBSportStoreEntities() : base("name=DBSportStoreEntities")
        {
            Database.SetInitializer<DBSportStoreEntities>(null);
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderPro> OrderProes { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<UserAccount>().HasRequired(x => x.Role).WithMany(x => x.UserAccounts).HasForeignKey(x => x.RoleID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Customer>().HasOptional(x => x.UserAccount).WithMany().HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().HasRequired(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderPro>().HasRequired(x => x.Customer).WithMany(x => x.OrderProes).HasForeignKey(x => x.CustomerID).WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderDetail>().HasRequired(x => x.OrderPro).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderID).WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderDetail>().HasRequired(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
            modelBuilder.Entity<OrderPro>().Property(x => x.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderPro>().Property(x => x.OrderDate).HasColumnType("datetime2");
            modelBuilder.Entity<UserAccount>().Property(x => x.CreatedAt).HasColumnType("datetime2");
            base.OnModelCreating(modelBuilder);
        }
    }
}
