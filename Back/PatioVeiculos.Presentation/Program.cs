using Microsoft.EntityFrameworkCore;
using PatioVeiculos.Application.Helpers;
using PatioVeiculos.Application.Services;
using PatioVeiculos.Application.Services.Interfaces;
using PatioVeiculos.Infrastructure.Commands;
using PatioVeiculos.Infrastructure.Commands.Interfaces;
using PatioVeiculos.Infrastructure.Contexts;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PatioVeiculosContext>(options =>
{
	options.UseSqlite(builder.Configuration
	                         .GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(PatioVeiculosProfile).Assembly);

builder.Services.AddScoped<IVeiculosCommands, VeiculosCommands>();
builder.Services.AddScoped<IVeiculosServices, VeiculosServices>();

builder.Services.AddScoped<IMovimentacoesCommands, MovimentacoesCommands>();
builder.Services.AddScoped<IMovimentacoesServices, MovimentacoesServices>();

builder.Services.AddScoped<IRelatoriosCommands, RelatoriosCommands>();
builder.Services.AddScoped<IRelatoriosServices, RelatoriosServices>();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//        .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
           );

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();