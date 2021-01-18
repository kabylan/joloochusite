using joloochusite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using joloochusite.Data;

[assembly: HostingStartup(typeof(joloochusite.Areas.Identity.IdentityHostingStartup))]
namespace joloochusite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {


            });
        }
    }
}
