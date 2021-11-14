using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kubernetes.DataReaderMapper;
using EFData;
using Kubernetes.AppConfiguration;
using Kubernetes.Services;
using Kubernetes.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

using Web.MiddleWares;
using Microsoft.AspNetCore.Authentication;
using Web.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Web
{
    public class Startup
    {
        readonly string _corsPolicy = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
             // .MinimumLevel.Debug()
              //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
              //{                  
              //    MinimumLogEventLevel = LogEventLevel.Warning,
              //    AutoRegisterTemplate = true
              //})
              .CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                     );
            });
            ConfigureJwtAuthService(services);
            services.AddControllers();                 



            services.AddTransient<IAppSettingManager, AppSettingManager>();
            services.AddTransient<IDataReaderMapper, DataReaderMapper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IRegistrationRepository, RegistrationRepository>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMaps, Maps>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartService, CartService>();
            
            //Add Microsoft.entityFrameworkcore.tools
            //Add
            //services.AddDbContext<AppDbContext>
            // services.AddDbContext<ProductContext>(options => options.UseSqlServer(Configuration["SQL:ConnectionString"]));
            // services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration["SQL:ConnectionString"]), ServiceLifetime.Transient);            

            //var symmetricKeyAsBase64 = "A503F474-4F32-4DDE-A1F2-3635716A0A04";
            //var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            //var signingKey = new SymmetricSecurityKey(keyByteArray);



           
            



            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    facebookOptions.AccessDeniedPath = "/user";
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tutorial API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        { 
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/api/Error");
            }

            else
            {
                app.UseExceptionHandler("/api/Error");
            }

            app.UseStaticFiles(); //put css javascripts, images , etc... into the pipeline.
                                  //app.UseHttpsRedirection(); // use this middleware to force the request to redirect to https.
                                  //app.UseAuthentication();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseRouting();
            app.UseCors("CorsPolicy");

          

           
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();
            app.UseToken();

            //use convention route which uses routing table
            //app.UseEndpoints(endpoints => {
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}"
            //    );

            // });


            //Use attribute route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                         
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kubenetes Web API");
            });            
            
        }

        #region ConfigureJwtAuthService
        private void ConfigureJwtAuthService(IServiceCollection services)
        {
            var tokenConfig = Configuration.GetSection("Token");
            var symmetricKeyAsBase64 = tokenConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = tokenConfig["Iss"],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = tokenConfig["Aud"],

                // Validate the token expiry  
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,              
             
               
            };

#if USE_CUSTOM_SCHEMA
            //Use Authorize attribute but using our custom AuthFilter instead of using JWTBearer default schema.
            services.AddAuthentication("AuthFilter")
                .AddScheme<AuthenticationSchemeOptions, AuthFilter>("AuthFilter", null);


#endif
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })

            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("hasReportAccess",
                    policy => policy
                        .RequireClaim("accesses", "report")
                        .RequireClaim(ClaimTypes.Role, "Administrator")
                        .RequireRole("test"));
            });
        }

#endregion
    }




}
