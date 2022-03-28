using GuessWord.BusinessLogic;
using GuessWord.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace GuessWord.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Application dependencies
            services
                .AddBusinessLogicDependencies()
                .AddDataAccessDependencies(_configuration);


            // Api configuration
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Auth configuration
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("admin", builder =>
                    {
                        builder.RequireClaim(ClaimTypes.Role, "admin");
                    });
                    options.AddPolicy("user", builder =>
                    {
                        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "user")
                                                 || x.User.HasClaim(ClaimTypes.Role, "admin"));
                    });
                })
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                    };
                });

            // Swagger Configurarion
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "Guess Word Api",
                    Version = "v1.0"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                     }
                });
            });


        }

        public void Configure(IApplicationBuilder builder)
        {
            builder
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("../swagger/v1.0/swagger.json", "Api v1.0");
                    options.RoutePrefix = "docs";
                })
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("Ok");
                    });
                });
        }
    }
}
