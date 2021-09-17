using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeController.Areas.Identity.Data;
using TimeController.Models;

namespace TimeController.Data
{
    public class TimeControllerDbContext : IdentityDbContext<TimeControllerUser>
    {
        public TimeControllerDbContext(DbContextOptions<TimeControllerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<History>().HasKey(u => new { u.UserID, u.Date});
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<History> history {get; set; }
}
}
