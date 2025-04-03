using TourTravelApi_Creation.Data;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(fv =>fv.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<DestinationRepository>();
builder.Services.AddScoped<PackageRepository>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<ItineraryRepository>();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<TransportationRepository>();
builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<OfferRepository>();
//builder.Services.AddScoped<ContactUsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Console.WriteLine("JWT Key: " + builder.Configuration["Jwt:Key"]);
Console.WriteLine("JWT Issuer: " + builder.Configuration["Jwt:Issuer"]);
Console.WriteLine("JWT Audience: " + builder.Configuration["Jwt:Audience"]);

app.UseAuthentication();
app.UseAuthorization();

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
