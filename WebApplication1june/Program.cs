using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", info: new OpenApiInfo
    {
        Version = "v1",
        Title = "TestApi",
        Description = "My first api",
        TermsOfService = new Uri(uriString:"https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "joe",
            Email = "joe@example.com",
            Url = new Uri(uriString:"https://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "My License",
            Url = new Uri(uriString:"https://www.google.com")
        },
    });
    options.AddSecurityDefinition(name:"Bearer", securityScheme:new OpenApiSecurityScheme
    {
        Name = "UseAuthorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your Jwt Key"
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
               },
               Name = "Bearer",
               In = ParameterLocation.Header
           },
           new List<string>()
        }
    });
});

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