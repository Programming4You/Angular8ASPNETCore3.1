using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Data;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Repository;


namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod(); 
                });
            });


            services.AddControllers().AddNewtonsoftJson();  

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddRazorPages();

            services.AddScoped<CustomerRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<ItemRepository>();

            services.AddDbContext<DbSettings>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddIdentity<AspNetUsers, IdentityRole>()   
            .AddEntityFrameworkStores<DbSettings>();

            services.AddRazorPages();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            services.AddCors();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "api/order");
            });
        }
    }
}
