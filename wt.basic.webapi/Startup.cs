using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.webapi.Extension.ErrorHandler;
using wt.basic.service.Common;
using wt.lib;
using Microsoft.AspNetCore.Http.Features;

namespace wt.basic.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //wt.lib.Office.PPtConvert.TransferPPT2ImgReturnList("\\\\Mac\\Home\\Desktop\\Insight.ppt",
            //    "\\\\Mac\\Home\\Documents\\project\\wt.basic.platform\\wt.basic.webapi\\");

            services.Configure<wtConfig>(Configuration);

            //http access policy
            services.AddCors(options =>
            {
                options.AddPolicy("*", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            //services.AddControllers();

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressInferBindingSourcesForParameters = false;//默认启用模型绑定推断
                });

            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate().AddNegotiate("NTLM", options => { });

            var token = Configuration.Get<wtConfig>().token;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearOptions =>
            {
                jwtBearOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.SecretKey)),

                    ValidateIssuer = true,
                    ValidIssuer = token.Issuer,

                    ValidateAudience = true,
                    ValidAudience = token.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(token.ClockSkew)
                };
            });


            var serverVersion = new MySqlServerVersion(new Version(5, 7, 36));
            services.AddDbContext<BasicContext>(optionsBuilder =>
            {
                //optionsBuilder.UseMySQL("server=10.119.108.234;database=test123;user=root;password=root;");
                //optionsBuilder.UseMySQL(Configuration.GetConnectionString("DefaultConnection"), serverVersion);
                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"), serverVersion);
                //optionsBuilder.UseLazyLoadingProxies();
            });

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            #region .net core2 Autofac

            //var builder = new ContainerBuilder();

            //builder.Populate(services);

            //ConfigureContainer(builder);

            //ILifetimeScope autofacContainer = builder.Build();

            //return new AutofacServiceProvider(autofacContainer);

            #endregion

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<wtJsonResult>().AsSelf().InstancePerLifetimeScope();

            var assembly = Assembly.Load("wt.basic.service");
            var assemblyAccess = Assembly.Load("wt.basic.dataAccess");

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf()
                .InstancePerLifetimeScope();//Request LiftTime


            builder.RegisterAssemblyTypes(assemblyAccess)
                .Where(t => t.Name.EndsWith("Repository") && t.IsInterface == false)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //静态服务器物理路径
            string fileUploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StaticFiles");
            string address = Configuration.Get<wtConfig>().fileserver?.address;
            if (!string.IsNullOrWhiteSpace(address))
                fileUploadPath = address;

            if (!Directory.Exists(fileUploadPath))
                Directory.CreateDirectory(fileUploadPath);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(fileUploadPath),
                RequestPath = "/StaticFiles"
            });

            //var token = Configuration.Get<wtConfig>().token;

            app.UseErrorHandling();//ȫ���쳣����

            app.UseCors("*");

            app.UseRouting();//match url to specific endpoint SetEndpoint

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        var enp = context.GetEndpoint();
            //        Console.WriteLine($"enp:{(enp == null ? "NULL" : $"{enp.DisplayName}")}");
            //        await next(context);
            //    };
            //});

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Webapi 特性路由
                endpoints.MapControllers();//URL GetEndpoint
            });

            //app.UseWelcomePage();

            //TODO: 2������Library
        }
    }
}
