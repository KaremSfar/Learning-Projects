using Microsoft.AspNet.Identity.EntityFramework;
using PersonalLogger.Models.Fields;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace PersonalLogger.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<MyLog> MyLogs { get; set; }

        public DbSet<LogCategory> LogCategories { get; set; }

        public DbSet<FieldType> FieldTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}