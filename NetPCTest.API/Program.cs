using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetPCTest.Data;
using NetPCTest.Data.Entities;
using NetPCTest.Domain.Abstract;
using NetPCTest.Domain.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = secretKey
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestForum", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbCntx = new AppDbContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
    List<UserEntity> users = new List<UserEntity>();
    List<ContactEntity> contacts = new List<ContactEntity>();
    //await dbCntx.Database.EnsureDeletedAsync();
    //await dbCntx.Database.EnsureCreatedAsync();
    if (!dbCntx.Users.Any())
    {
        users.Add(new UserEntity()
        {
            Id = Guid.Parse("3ADD6086-7B89-4E6D-B16D-433D15CBA7AA"),
            Login = "Zdzislaw",
            PasswordHash = "haslo"
        });
        users.Add(new UserEntity()
        {
            Id = Guid.Parse("C51EB20A-2313-4B45-BFDA-3E5696A72599"),
            Login = "Staszek",
            PasswordHash = "haslo"
        });
        users.Add(new UserEntity()
        {
            Id = Guid.Parse("CA6868B3-C7E7-47B7-B41D-43C2BDBE6369"),
            Login = "Leszek",
            PasswordHash = "haslo"
        });
        users.Add(new UserEntity()
        {
            Id = Guid.Parse("0E6C8A63-7E5C-4258-8DE0-ECD4B7DC39F3"),
            Login = "Andrzej",
            PasswordHash = "haslo"
        });
        dbCntx.Users.AddRange(users);
        dbCntx.SaveChanges();
    }

    if (!dbCntx.Contacts.Any())
    {
        contacts.Add(new ContactEntity()
        {
            Id = Guid.Parse("F46DC1B6-56DA-4D4B-AB9F-C4295B4109D6"),
            Firstname = "Luffy",
            Surname = "Monkey D.",
            Email = "onepiece@gmail.com",
            BirthDate = DateTime.Now.AddYears(-26),
            PhoneNumber = "123456789",
            UserId = Guid.Parse("C51EB20A-2313-4B45-BFDA-3E5696A72599"),
            Category = "Job"
        });
        contacts.Add(new ContactEntity()
        {
            Id = Guid.Parse("873BB197-5E19-46EE-A349-FE034C029A01"),
            Firstname = "One",
            Surname = "The Ashen",
            Email = "darksouls@gmail.com",
            BirthDate = DateTime.Now.AddYears(-46),
            PhoneNumber = "987654321",
            UserId = Guid.Parse("C51EB20A-2313-4B45-BFDA-3E5696A72599"),
            Category = "Job"
        });
        contacts.Add(new ContactEntity()
        {
            Id = Guid.Parse("{61E16A97-CEC9-469A-A81A-8EA49E247077}"),
            Firstname = "Asta",
            Surname = "Black",
            Email = "blackclover@gmail.com",
            BirthDate = DateTime.Now.AddDays(-6348),
            PhoneNumber = "194827593",
            UserId = Guid.Parse("CA6868B3-C7E7-47B7-B41D-43C2BDBE6369"),
            Category = "Job"
        });
        contacts.Add(new ContactEntity()
        {
            Id = Guid.Parse("18E751FA-1601-4D69-9A13-2137E7301D33"),
            Firstname = "Geralt",
            Surname = "Z Rivii",
            Email = "wiedzmin@gmail.com",
            BirthDate = DateTime.Now.AddMonths(-254),
            PhoneNumber = "940659463",
            UserId = Guid.Parse("CA6868B3-C7E7-47B7-B41D-43C2BDBE6369"),
            Category = "Job"
        });
        contacts.Add(new ContactEntity()
        {
            Id = Guid.Parse("DE6BA2FE-E944-4B01-92DE-25762D5DC6CE"),
            Firstname = "Goku",
            Surname = "Son",
            Email = "dragonball@gmail.com",
            BirthDate = DateTime.Now.AddDays(-11543),
            PhoneNumber = "583958365",
            UserId = Guid.Parse("CA6868B3-C7E7-47B7-B41D-43C2BDBE6369"),
            Category = "Job"
        });
        dbCntx.Users.AddRange(users);
        dbCntx.Contacts.AddRange(contacts);
        dbCntx.SaveChanges();
    }
}
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

app.Run();
