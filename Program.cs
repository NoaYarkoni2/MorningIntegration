using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MorningIntegration.Data;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using MorningIntegration.Services;
using MorningIntegration.Interface;



var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

//Bearer Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value
                       ?? throw new InvalidOperationException("Jwt:ApiSecret configuration is missing")))
    };
});

//builder.Services.AddHttpClient<ClientService>();
//builder.Services.AddTransient<IClientService>(sp =>
//{
//    var httpClient = sp.GetRequiredService<HttpClient>();
//    var apiKey = builder.Configuration["Jwt:ApiKey"];
//    var apiSecret = builder.Configuration["Jwt:ApiSecret"];
//    var tokenUrl = builder.Configuration["Jwt:TokenUrl"];
//    var clientUrl = builder.Configuration["Jwt:ClientUrl"];
//    return new ClientService(httpClient, apiKey, apiSecret, tokenUrl, clientUrl);
//});
// Add Authorization
//builder.Services.AddAuthorization();
//builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddHttpClient();
//builder.Services.AddHttpClient("greeninvoice", c =>
//{
//    c.BaseAddress = new Uri(confi"https://api.greeninvoice.co.il/api/v1");
//});
//builder.Services.AddHttpClient<IClientService, ClientService>();
builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["https://sandbox.d.greeninvoice.co.il/api/v1/"]);
});

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IClientService, ClientService>();
// Add services to the container.
builder.Services.AddControllers();
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//builder.Services.AddScoped<IAccountService, AccountService>();



app.Run();