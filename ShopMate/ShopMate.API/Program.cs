
using BLL.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.BLL.Service.Implementation;
using ShopMate.BLL.Validation;
using ShopMate.DAL.Database;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;
using ShopMate.DAL.Repository.Implementation;
using System.Text;

namespace ShopMate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                   options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider
               )
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "jwt";
                option.DefaultChallengeScheme = "jwt";
            }).AddJwtBearer("jwt", options =>
            {
                var securitykeystring = builder.Configuration.GetSection("SecretKey").Value;
                var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring!);
                SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });




            builder.Services.AddScoped<IAccountService, AccountServiceImp>();
            builder.Services.AddScoped<IAccountRepo, AccountRepoImp>();
            builder.Services.AddScoped<ITokenService, TokenServiceImp>();

            builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
            builder.Services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordValidator>();

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddScoped<IEmailService, EmailService>();


            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });

            });



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
