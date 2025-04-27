
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
using ShopMate.DAL.Database.Seeding;
using ShopMate.DAL.Repository.Abstraction;
using ShopMate.DAL.Repository.Implementation;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopMate.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

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

            builder.Services.AddScoped<IOrderService, OrderServiceImp>();
            builder.Services.AddScoped<IOrderRepo, OrderRepoImp>();

            builder.Services.AddScoped<IContactMessageService, ContactMessageServiceImp>();
            builder.Services.AddScoped<IContactMessageRepo, ContactMessageRepoImp>();

            builder.Services.AddScoped<IProductReviewService, ProductReviewServiceImp>();
            builder.Services.AddScoped<IProductReviewRepo, ProductReviewRepoImp>();

            builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
            builder.Services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordValidator>();
            builder.Services.AddScoped<IValidator<UpdateProfileDto>, UpdateProfileValidator>();
            builder.Services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordValidator>();

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IFileService, FileServiceImp>();


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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await DbSeeder.SeedAsync(context, userManager);
            }

            app.Run();
        }
    }
}
