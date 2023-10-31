﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleCommerce.Domain.Entities.Order;
using SimpleCommerce.Domain.Entities.Product;
using SimpleCommerce.Domain.Entities.User;
using System.Reflection;

namespace TransportGlobal.Infrastructure.Context
{
    public class SimpleCommerceDBContext : DbContext
    {
        #region User Bounded Context DbSets 
        public DbSet<UserEntity> Users { get; set; }
        #endregion

        #region Product Bounded Context DbSets 
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        #endregion

        #region Order Bounded Context DbSets
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }
        #endregion

        public SimpleCommerceDBContext(DbContextOptions<SimpleCommerceDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SetColumnTypes(modelBuilder.Model);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetColumnTypes(IMutableModel model)
        {
            foreach (IMutableEntityType entityType in model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal))
                    {
                        property.SetColumnType("decimal(18, 2)");
                    }
                }
            }
        }
    }
}