using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ntgroup.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Microsoft.Net.Http.Headers;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.APIs.Contracts;
using ntgroup.APIs;
using ntgroup.Services.Interfaces;
using ntgroup.Services;

var builder = WebApplication.CreateBuilder(args);

// API: Connect to Mysql server
builder.Services.AddDbContext<ntgroupDbContext>(
    opt =>
    {
        opt.UseMySql(builder.Configuration.GetConnectionString("Default"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
        /*
            Fix error: The instance of entity type cannot be tracked because another instance 
                                                with the same key value for { 'ID'} is already being tracked.
            Các truy vấn trên Repository chỉ được xem, không thể cập nhật. Để cập nhật/thêm mới/xóa: 
            - Update: context.Entry<Entities>(_model).State = EntityState.Modified;
            - Add: context.Entry<Entities>(_model).State = EntityState.Added;
            - Delete: context.Entry<Entities>(_model).State = EntityState.Deleted;
        */
        opt.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }, ServiceLifetime.Transient
);

// API: Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<ntgroupDbContext>()
        .AddDefaultTokenProviders();
// UI: Tăng kích thước bộ nhớ đệm
builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// UI: Add MudBlazor
builder.Services.AddMudServices();

// UI: Register Client Factory
builder.Services.AddHttpClient("IdentityBlazorCoreAPIServer", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration["API:localhost"] ??
                                throw new InvalidOperationException("Can't found [Secret Key] in appsettings.json !"));
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestntgroupAPI");
    });

// UI: Get httpClient API default
builder.Services.AddScoped(
    defaultClient => new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["API:localhost"] ??
                                throw new InvalidOperationException("Can't found [Secret Key] in appsettings.json !"))
    });

// API: Add SwaggerGen (dotnet add package Swashbuckle.AspNetCore)
builder.Services.AddSwaggerGen(
    opt =>
    {
        //Init project: CRUD category,order,orderdetail,..., AugCenterModel
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Taxi Nam Thang Manager API", Version = "v1" });
        opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")"
        });

        //Add filter to block case authorize: Swashbuckle.AspNetCore.Filters
        opt.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);

// API: Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// API: Add Jwt, Gooogle Authentication
builder.Services.AddAuthentication(authenticationOptions => {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                    authenticationOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(jwtBearerOptions => {
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                            ValidAudience = builder.Configuration["JWT:ValidAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]
                                                ?? throw new InvalidOperationException("Can't found [Secret Key] in appsettings.json !"))
                            ),
                            ValidateIssuer = false,
                            ValidateAudience = false
                    };
                });

// API: Register APIs
builder.Services.AddScoped<IAuthServer, AuthServer>();
builder.Services.AddScoped<ISpreadsConfigServer, SpreadsConfigServer>();

// UI: Register Services
builder.Services.AddScoped<ISheetContractService, SheetContractService>();
builder.Services.AddScoped<ISheetTimepieceService, SheetTimepieceService>();
builder.Services.AddScoped<ISheetShiftworkService, SheetShiftworkService>();
builder.Services.AddScoped<ISheetRegisterContractService, SheetRegisterContractService>();

var app = builder.Build();

// UI: Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else // API: Add run Swagger UI: https://localhost:5110/swagger/index.html
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(
        opt =>
        {
            opt.SwaggerEndpoint($"/swagger/v1/swagger.json", "Taxi Nam Thang Manager API V1");
        }
    );
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// API: Add Authoz and Authen
app.UseAuthentication();
app.UseAuthorization();

// API: Add controllers
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();