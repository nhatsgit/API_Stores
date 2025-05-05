using API_Stores.Midlewares;
using API_Stores.Models;
using API_Stores.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddScoped<IProductService, S_Product>();

//Connect DB
builder.Services.AddDbContext<StoresDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoresDB")));
/*builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<StoresDbContext>()
    .AddDefaultTokenProviders();*/
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super_secret_key_12345"))
        };
    });

builder.Services.AddAuthorization();
//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
//
app.UseAuthentication();
//
app.MapControllers();



app.Run();
