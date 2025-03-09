using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Models;

public partial class PizzaShopContext : DbContext
{
    public PizzaShopContext()
    {
    }

    public PizzaShopContext(DbContextOptions<PizzaShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerwaiting> Customerwaitings { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Favouriteitem> Favouriteitems { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Kot> Kots { get; set; }

    public virtual DbSet<Modifier> Modifiers { get; set; }

    public virtual DbSet<Modifiergroup> Modifiergroups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Ordertax> Ordertaxes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Useraddress> Useraddresses { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PizzaShop;Username=postgres;Password=Tatva@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Stateid).HasColumnName("stateid");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Stateid)
                .HasConstraintName("stateid");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Customeremail).HasColumnName("customeremail");
            entity.Property(e => e.Customername)
                .HasColumnType("character varying")
                .HasColumnName("customername");
            entity.Property(e => e.Customerphone)
                .HasMaxLength(12)
                .HasColumnName("customerphone");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastorderid).HasColumnName("lastorderid");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");

            entity.HasOne(d => d.Lastorder).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Lastorderid)
                .HasConstraintName("last_order_id");
        });

        modelBuilder.Entity<Customerwaiting>(entity =>
        {
            entity.HasKey(e => e.Tokenid).HasName("Waiting_pkey");

            entity.ToTable("customerwaiting");

            entity.Property(e => e.Tokenid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tokenid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Noofpeople).HasColumnName("noofpeople");
            entity.Property(e => e.Waitingtime).HasColumnName("waitingtime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerwaitings)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_id");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Deptname)
                .HasColumnType("character varying")
                .HasColumnName("deptname");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");

            entity.HasOne(d => d.ModifiedbyNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Modifiedby)
                .HasConstraintName("ModifiedBy");
        });

        modelBuilder.Entity<Favouriteitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Favourite_Items_pkey");

            entity.ToTable("favouriteitems");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Itemid).HasColumnName("itemid");

            entity.HasOne(d => d.Category).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("category_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.Itemid)
                .HasConstraintName("item_id");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ratings_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Ratings_rate_id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Ambience).HasColumnName("ambience");
            entity.Property(e => e.Comment)
                .HasColumnType("character varying")
                .HasColumnName("comment");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Food).HasColumnName("food");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Service).HasColumnName("service");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Totalamount).HasColumnName("totalamount");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderid");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Itemid).HasName("Item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.Itemid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("itemid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Defaulttax).HasColumnName("defaulttax");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Isavailable).HasColumnName("isavailable");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Isfavourite).HasColumnName("isfavourite");
            entity.Property(e => e.ItemImage)
                .HasColumnType("character varying")
                .HasColumnName("item_image");
            entity.Property(e => e.Itemname)
                .HasColumnType("character varying")
                .HasColumnName("itemname");
            entity.Property(e => e.Itemquantity).HasColumnName("itemquantity");
            entity.Property(e => e.Itemrate)
                .HasColumnType("money")
                .HasColumnName("itemrate");
            entity.Property(e => e.Itemtype)
                .HasColumnType("character varying")
                .HasColumnName("itemtype");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modified_date");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifierid).HasColumnName("modifierid");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Items)
                .HasForeignKey(d => d.Modifierid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifierid");
        });

        modelBuilder.Entity<Kot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Kot_pkey");

            entity.ToTable("kot");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Isready).HasColumnName("isready");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Itemquantity).HasColumnName("itemquantity");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Tableid).HasColumnName("tableid");

            entity.HasOne(d => d.Item).WithMany(p => p.Kots)
                .HasForeignKey(d => d.Itemid)
                .HasConstraintName("item_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Kots)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("order_id");

            entity.HasOne(d => d.Table).WithMany(p => p.Kots)
                .HasForeignKey(d => d.Tableid)
                .HasConstraintName("table_id");
        });

        modelBuilder.Entity<Modifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Modifier_pkey");

            entity.ToTable("modifier");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Modifierdescription)
                .HasColumnType("character varying")
                .HasColumnName("modifierdescription");
            entity.Property(e => e.Modifiergroupid).HasColumnName("modifiergroupid");
            entity.Property(e => e.Modifiername)
                .HasColumnType("character varying")
                .HasColumnName("modifiername");
            entity.Property(e => e.Modifierquantity).HasColumnName("modifierquantity");
            entity.Property(e => e.Modifierrate).HasColumnName("modifierrate");
            entity.Property(e => e.Unit)
                .HasColumnType("character varying")
                .HasColumnName("unit");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Modifiers)
                .HasForeignKey(d => d.Modifiergroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifiergroupid");
        });

        modelBuilder.Entity<Modifiergroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Modifier_Group_pkey");

            entity.ToTable("modifiergroup");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Modifiergroupname)
                .HasColumnType("character varying")
                .HasColumnName("modifiergroupname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("order");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Modifierid).HasColumnName("modifierid");
            entity.Property(e => e.Orderdate)
                .HasColumnType("timestamp without time zone[]")
                .HasColumnName("orderdate");
            entity.Property(e => e.Orderduration).HasColumnName("orderduration");
            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Totalamount).HasColumnName("totalamount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerid");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Modifierid)
                .HasConstraintName("modifierid");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Paymentid)
                .HasConstraintName("paymentid");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_Details_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Itemquantity).HasColumnName("itemquantity");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Preparedquantity).HasColumnName("preparedquantity");

            entity.HasOne(d => d.Item).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("itemid");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderid");
        });

        modelBuilder.Entity<Ordertax>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordertax_pkey");

            entity.ToTable("ordertax");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Taxid).HasColumnName("taxid");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertaxes)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderid");

            entity.HasOne(d => d.Tax).WithMany(p => p.Ordertaxes)
                .HasForeignKey(d => d.Taxid)
                .HasConstraintName("taxid");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("Payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Paymentid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("paymentid");
            entity.Property(e => e.Cardno)
                .HasColumnType("character varying")
                .HasColumnName("cardno");
            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Paymentmode)
                .HasColumnType("character varying")
                .HasColumnName("paymentmode");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Totalamount).HasColumnName("totalamount");
            entity.Property(e => e.Upiid).HasColumnName("upiid");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Invoiceid)
                .HasConstraintName("invoiceid");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("Permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Permissionid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("permissionid");
            entity.Property(e => e.Candelete).HasColumnName("candelete");
            entity.Property(e => e.Canedit).HasColumnName("canedit");
            entity.Property(e => e.Canview).HasColumnName("canview");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone[]")
                .HasColumnName("createddate");
            entity.Property(e => e.Deptid).HasColumnName("deptid");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone[]")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Dept).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.Deptid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departmentid");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roleid");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("Role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Roleid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("roleid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Rolename)
                .HasColumnType("character varying")
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Sectionid).HasName("Section_pkey");

            entity.ToTable("section");

            entity.Property(e => e.Sectionid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sectionid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Sectionname)
                .HasColumnType("character varying")
                .HasColumnName("sectionname");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("state_pkey");

            entity.ToTable("state");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("countryid");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Tableid).HasName("Table_pkey");

            entity.ToTable("table");

            entity.Property(e => e.Tableid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tableid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Tablename)
                .HasColumnType("character varying")
                .HasColumnName("tablename");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tables)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerid");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.Sectionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sectionid");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.Taxid).HasName("Tax_pkey");

            entity.ToTable("tax");

            entity.Property(e => e.Taxid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("taxid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdefault).HasColumnName("isdefault");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Isenable).HasColumnName("isenable");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Taxname)
                .HasColumnType("character varying")
                .HasColumnName("taxname");
            entity.Property(e => e.Taxtype)
                .HasColumnType("character varying")
                .HasColumnName("taxtype");
            entity.Property(e => e.Taxvalue).HasColumnName("taxvalue");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("User_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userid");
            entity.Property(e => e.Addressid).HasColumnName("addressid");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Phoneno)
                .HasMaxLength(12)
                .HasColumnName("phoneno");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userimg)
                .HasColumnType("character varying")
                .HasColumnName("userimg");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.Addressid)
                .HasConstraintName("addressid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("role");
        });

        modelBuilder.Entity<Useraddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserAddress_pkey");

            entity.ToTable("useraddress");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Pincode)
                .HasColumnType("character varying")
                .HasColumnName("pincode");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Useraddresses)
                .HasForeignKey(d => d.City)
                .HasConstraintName("city");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Useraddresses)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("country");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.UseraddressCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedbyNavigation).WithMany(p => p.UseraddressModifiedbyNavigations)
                .HasForeignKey(d => d.Modifiedby)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.Useraddresses)
                .HasForeignKey(d => d.State)
                .HasConstraintName("state");
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userlogin_pkey");

            entity.ToTable("userlogin");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(7L, null, null, null, null, null)
                .HasColumnName("id");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Hashpassword)
                .HasColumnType("character varying")
                .HasColumnName("hashpassword");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Token)
                .HasColumnType("character varying")
                .HasColumnName("token");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Role).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("roleid");

            entity.HasOne(d => d.User).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
