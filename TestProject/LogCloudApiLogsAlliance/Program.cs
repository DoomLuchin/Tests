//using CommonsResources.Helpers.Errors;
using LogsAllianceApi.Authorization;
using LogsAllianceApi.Commons;
using LogsAllianceApi.Data;
using LogsAllianceApi.Resource;
using LogsAllianceApi.Services.Interfaces;
using LogsAllianceApi.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AllianceCloudConnection");
builder.Services.AddSingleton<IAuthorizationHandler, LagAuthorization>();
builder.Services.AddDbContext<ApiContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ISearchFilterHistorico, ServiceSearchFilterHistorico>();
builder.Services.AddScoped<IRepositoryHistorico, ServiceRepositoryHistorico>();

#region OpenIdDict
var guestPolicy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .RequireClaim("scope", AutorizacionSSO.ScopeProveedores)
                      .RequireClaim("scope", "lagDevScope")
                      .RequireClaim("scope", AutorizacionSSO.ScopeCliente360)
                      .RequireClaim("scope", AutorizacionSSO.ScopeAlliance)
                     .Build();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});
builder.Services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    string conexion = Configuration["URL:IdentitySSO"];
                    options.SetIssuer(conexion);
                    options.AddAudiences(AutorizacionSSO.AudienceProveedores, AutorizacionSSO.AudienceAlliance);

                    // Configure the validation handler to use introspection and register the client
                    // credentials used when communicating with the remote introspection endpoint.
                    options.UseIntrospection()
                            .SetClientId(AutorizacionSSO.ClientIdIntrospection)
                            .SetClientSecret(AutorizacionSSO.PasswordIntrospection);
                    //options.UseIntrospection()
                    //        .SetClientId("LagDevApi")
                    //        .SetClientSecret(Scopes.Password);


                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthorizationToken", policyUser =>
    {
        policyUser.Requirements.Add(new RequireScope());
    });
});
builder.Services.AddScoped<IAuthorizationHandler, RequireScopeHandler>();

#endregion

var app = builder.Build();
//app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
