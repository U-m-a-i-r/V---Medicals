using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using V___Medicals.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System;
using V___Medicals.Models;
using V___Medicals.Services;
using V___Medicals.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

/*builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();*/
builder.Services.AddIdentity<User, IdentityRole<string>>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "AppDefinedAuthentication";
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
            .AddPolicyScheme("AppDefinedAuthentication", "AppDefinedAuthentication", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    // since all my api will be starting with /api, modify this condition as per your need.
                    if (context.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCulture))
                        return JwtBearerDefaults.AuthenticationScheme;
                    else
                        return IdentityConstants.ApplicationScheme;
                };
            }).AddCookie(option=> { 
                option.LoginPath = "/Identity/Account/Login";
            })
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
    };
});
builder.Services.Configure<AuthenticationOptions>(options => options.DefaultScheme = "AppDefinedAuthentication");
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddApiVersioning(config =>
{
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});
builder.Services.AddHttpClient();
//builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "VMedicals API",
        Description = "API for retrieving VMedcials Data"
    });

    s.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new List<string>()
                    }
                });
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCookiePolicy();

// it should be UseAuthentication() UseRouting() UseAuthorization() for aspnetcore3.1
app.UseRouting();
app.UseCors("AdeptCorsPolicy");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
app.UseSwagger()
   .UseStatusCodePages();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "apis/swagger";

});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.MapRazorPages();

app.Run();
