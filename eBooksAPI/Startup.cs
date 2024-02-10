using eBooksAPI.Data;
using eBooksAPI.Models;
using eBooksAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooksAPI
{
    public class Startup
    {
        string connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = configuration.GetConnectionString("ApiConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //dbContext configuration
            services.AddDbContext<appDbContext>(options => options.UseSqlServer(connectionString));
            //configure services
            services.AddTransient<bookService>();
            services.AddTransient<publisherService>();
            services.AddTransient<authorService>();
            //services.AddTransient<publisherService>();
            //for api versioning if need to use same controller names in diff namespaces
            //services.AddApiVersioning(options=> {
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //});

            //add identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<appDbContext>()
                .AddDefaultTokenProviders();
            //add authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //add jwt bearer 
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"])),

                    ValidIssuer=Configuration["JWT:Issuer"],
              
                   ValidateAudience=true,
                   ValidAudience=Configuration["JWT:Audience"]
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eBooksAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eBooksAPI v1"));
            }
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
