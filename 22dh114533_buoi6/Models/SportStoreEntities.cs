using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBSportStore.Models
{
    [Table("Role", Schema = "dbo")]
    public partial class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int RoleID { get; set; }
        [Required, StringLength(30)] public string RoleName { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; } = new HashSet<UserAccount>();
    }

    [Table("UserAccount", Schema = "dbo")]
    public partial class UserAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int UserID { get; set; }
        [Required, StringLength(50)] public string UserName { get; set; }
        [Required, StringLength(255)] public string PasswordHash { get; set; }
        [Required, StringLength(100)] public string PasswordSalt { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Role Role { get; set; }
    }

    [Table("Customer", Schema = "dbo")]
    public partial class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int CustomerID { get; set; }
        public int? UserID { get; set; }
        [Required, StringLength(100)] public string FullName { get; set; }
        [StringLength(20)] public string Phone { get; set; }
        [StringLength(100)] public string Email { get; set; }
        [StringLength(250)] public string Address { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<OrderPro> OrderProes { get; set; } = new HashSet<OrderPro>();
    }

    [Table("Category", Schema = "dbo")]
    public partial class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int CategoryID { get; set; }
        [Required, StringLength(100)] public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }

    [Table("Product", Schema = "dbo")]
    public partial class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ProductID { get; set; }
        [Required, StringLength(150)] public string ProductName { get; set; }
        [StringLength(500)] public string Description { get; set; }
        public int CategoryID { get; set; }
        [Column(TypeName = "decimal"), Range(0, double.MaxValue)] public decimal Price { get; set; }
        [StringLength(255)] public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }

    [Table("OrderPro", Schema = "dbo")]
    public partial class OrderPro
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int OrderID { get; set; }
        [Column(TypeName = "datetime2")] public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        [Required, StringLength(250)] public string ShippingAddress { get; set; }
        [Column(TypeName = "decimal")] public decimal TotalAmount { get; set; }
        [Required, StringLength(30)] public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }

    [Table("OrderDetail", Schema = "dbo")]
    public partial class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal")] public decimal UnitPrice { get; set; }
        public virtual OrderPro OrderPro { get; set; }
        public virtual Product Product { get; set; }
    }
}
