using Gestion_Devis_Facture.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Gestion_Devis_Facture.Model;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.Extensions.DependencyInjection;
using Gestion_Devis_Facture.Data;


namespace Gestion_Devis_Facture
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                });

            builder.Services.AddDbContext<InvoiceDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("InvoiceConnection");
                options.UseSqlServer(connectionString);
                
            });
            builder.Services.AddDbContext<QuoteDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceConnection")));

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are only sent over HTTPS
            });
            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 44325; // Default HTTPS port for development
            });


            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
           
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<InvoicePdfService>();
            builder.Services.AddScoped<IQuoteService, QuoteService>();
            builder.Services.AddScoped<QuotePdfService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
          
            app.UseEndpoints(endpoints => {endpoints.MapControllers(); });
            app.Run();
        }
    }
}