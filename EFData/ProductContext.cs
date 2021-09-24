using EFModel;
using Kubernetes.AppConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFData
{
    public class ProductContext:DbContext
    {
        readonly IAppSettingManager _appSettingManager;

        public ProductContext(IAppSettingManager appSettingManager, DbContextOptions<ProductContext> options) : base(options)
        {
            _appSettingManager = appSettingManager;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettingManager.SqlConnectionString);
        }
    }
}
