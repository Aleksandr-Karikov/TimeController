using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeController.Areas.Identity.Data;
using TimeController.Data;

[assembly: HostingStartup(typeof(TimeController.Areas.Identity.IdentityHostingStartup))]
namespace TimeController.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TimeControllerDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TimeControllerDbContextConnection")));

                services.AddDefaultIdentity<TimeControllerUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<TimeControllerDbContext>();
            });
        }
    }
}