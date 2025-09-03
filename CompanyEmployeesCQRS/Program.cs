using CompanyEmployeesCQRS.Extensions;
using CompanyEmployeesCQRS.Presentation.ActionFilters;
using Contracts;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Logger File Configuration
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerServices();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
});

// Prevent the controller to return default response (this belongs to the [ApiController] attribute).
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Configuring for the Formats and as well as moving our controllers to the Presentation Layer.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; // Returens 406 if the server don't support the  request format.
}).AddXmlDataContractSerializerFormatters()
  .AddApplicationPart(typeof(CompanyEmployeesCQRS.Presentation.AssemblyReference).Assembly); // Configuring the controllers which are in Presentation layer.

var app = builder.Build();

// Configuring Exception Handler
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();