using System.Text;
using System.Text.Json.Serialization;
using Coupons.Services.Auth;
using Coupons.Data;
using Coupons.Services.MarketingUsers;
using Coupons.Services.MarketplaceUsers;
using Coupons.Services.Products;
using Coupons.Services.Purchases;
using Coupons.Services.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Coupons;
using Coupons.Services.Redemptions;
using Coupons.Services.Validations;
using Coupons.Services.Slack;


// We create a WebApplicationBuilder object, which will help us configure and build our web application
var builder = WebApplication.CreateBuilder(args);

/* We add some essential services to our service container:
   - AddEndpointsApiExplorer: Allows us to explore the endpoints of our API
   - AddSwaggerGen: Generates Swagger documentation for our API (very useful for other developers to understand how to use it)
   - AddControllers: Enable the use of controllers in our web application
*/

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuration Slack
builder.Services.AddHttpClient();
builder.Services.AddSingleton(sp=>new SlackService(
    sp.GetRequiredService<IHttpClientFactory>().CreateClient(),builder.Configuration["Slack:WebhookUrl"]));

// dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection 
builder.Services.AddAutoMapper(typeof(Program));

// We configure the database context to use MySQL
// Read the connection string from the configuration file and specify the MySQL server version
builder.Services.AddDbContext<CouponsContext>(
    option => option.UseMySql(
    builder.Configuration.GetConnectionString("MySql"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.2")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// We register the repository services for authentication, coupons and redemption in our dependency injection container
// This allows us to inject these services into other parts of our application
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IMarketingUserService, MarketingUserService>();
builder.Services.AddScoped<IMarketplaceUserService, MarketplaceUserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRedemptionService, RedemptionService>();
builder.Services.AddScoped<ICouponRedemptionValidator, CouponRedemptionValidator>();

//We configure the JWT authentication middleware
builder.Services.AddAuthentication(options =>
{
    // We configure the default authentication scheme for JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // We configure the JWT validation parameters
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

// We build our web application using the configuration we have provided
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// We configure the HTTP request pipeline
// UseHttpsRedirection: Redirect HTTP requests to HTTPS (more secure)
// UseAuthorization: Enable authorization in our application (to control who can access what)
// MapControllers: Map our controllers to the corresponding routes
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

