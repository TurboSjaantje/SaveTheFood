using Core.DomainServices;
using Infrastructure.TM_EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
        .AddScoped<SaveTheFoodSeedData>()
        .AddScoped<IdentitySeedData>()
        .AddScoped<IKantineRepository, KantineRepository>()
        .AddScoped<IProductRepository, ProductRepository>() 
        .AddScoped<IStudentRepository, StudentRepository>() 
        .AddScoped<IMedewerkerRepository, MedewerkerRepository>()
        .AddScoped<IPakketRepository, PakketRepository>()

        //Seed Kantines options
        .AddDbContext<SaveTheFoodDbContext>(opts =>
        {
            opts
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
                .EnableSensitiveDataLogging(true);
        })

        //Seed Identity options
        .AddDbContext<SecurityDbContext>(opts =>
        {
            opts
                .UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"))
                .EnableSensitiveDataLogging(true);
        })
        .AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<SecurityDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddAuthorization(policyBuilder =>
{
    policyBuilder.AddPolicy("OnlyPowerUsersAndUp", policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim("UserType", "poweruser"));

    policyBuilder.AddPolicy("OnlyRegularUsersAndUp", policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim("UserType", "poweruser", "regularuser"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    await SeedDatabase();
}
else
{
    await SeedDatabase();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

async Task SeedDatabase()
{
    using var scope = app.Services.CreateScope();

    var SaveTheFoodSeeder = scope.ServiceProvider.GetRequiredService<SaveTheFoodSeedData>();
    await SaveTheFoodSeeder.EnsurePopulated(true);

    var dbIdentitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeedData>();
    await dbIdentitySeeder.EnsurePopulated(true);
}