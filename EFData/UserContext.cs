using EFModel;
using Kubernetes.AppConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFData
{
    public class UserContext : DbContext
    {
        readonly IAppSettingManager _appSettingManager;

        public UserContext(IAppSettingManager appSettingManager, DbContextOptions<UserContext> options) : base(options)
        {
            _appSettingManager = appSettingManager;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettingManager.SqlConnectionString);
        }
    }
}
