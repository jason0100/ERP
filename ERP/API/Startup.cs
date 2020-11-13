using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Data.Interface;
using API.Data.Repository;
using API.Data.UnitOfWork;
using API.Helpers;
using API.Models.Expenditure;
using API.Models.Item;
using API.Models.Partner;
using API.Models.Project;
using API.Models.RSA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                 //.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);
                 .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                //c.ExampleFilters();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
                //c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();
                //c.OperationFilter<XmlCommentsEscapeFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });



            //====database context======
            services.AddDbContext<ErpDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("serverConnection")).UseLazyLoadingProxies());



            //==== end database context======

            //=======start Repository=====

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork<ErpDbContext>>();

            services.AddScoped<IProjectRepository<ProjectModel>, ProjectRepository>();
            services.AddScoped<IGenericRepository<PartnerModel>, GenericRepository<PartnerModel>>();
            services.AddScoped<Iproject_partnerRepository<project_partnerModel>, Project_partnerRepository>();
            services.AddScoped<IExpenditureRepository<ExpenditureModel>, ExpenditureRepository>();
            services.AddScoped<IGenericRepository<ItemModel>, GenericRepository<ItemModel>>();
            
            //services.AddScoped<IGenericRepository<project_partnerModel>, project_partnerRepository<>>();
            //services.AddScoped<IGenericRepository<ExpenditureModel, ExpenditureModel>, ExpenditureRepository>();

            //=======end Repository=====

            //=======start Helper=====
            services.AddScoped<IRSAHelper, RSAHelper>();

            //=======end Helper=====

            var key = Encoding.ASCII.GetBytes(Configuration["TokenKey"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    // 透過這項宣告，就可以從 "role" 取值，並可讓 [Authorize] 判斷角色
                    //RoleClaimType = JwtClaimTypes.Role,
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // 驗證 Token 的有效期間
                    ValidateLifetime = true,
                };

            });
            services.AddAuthorization();
            services.AddControllers();
            //                .AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            services.AddCors(options =>
            {
                // CorsPolicy 是自訂的 Policy 名稱
                options.AddPolicy("CorsPolicy", policy =>

                    policy
                          .WithOrigins("*")
                          //.WithOrigins(Configuration["web"], Configuration["backstage"])
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                //.AllowCredentials()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseOptions();
            app.UseCors("CorsPolicy");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP_api v1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
