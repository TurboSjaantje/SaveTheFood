using Core.DomainServices;
using GraphQL.Server.Ui.Playground;
using Infrastructure.TM_EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI;
using WebAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services
    .AddScoped<SaveTheFoodSeedData>()
    .AddScoped<IPakketRepository, PakketRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<PakketQuery>()
    .AddScoped<PakketMutation>()
    .AddScoped<ProductQuery>()
    .AddScoped<ProductMutation>()
    .AddGraphQL(c => SchemaBuilder.New().AddServices(c)
        .AddType<PakketGraphQLTypes>()
        .AddQueryType<PakketQuery>()
        .AddMutationType<PakketMutation>()
        .Create())
    .AddGraphQL(c => SchemaBuilder.New().AddServices(c)
        .AddType<ProductGraphQLTypes>()
        .AddQueryType<ProductQuery>()
        .AddMutationType<ProductMutation>()
        .Create())

    //Seed Kantines options
    .AddDbContext<SaveTheFoodDbContext>(opts =>
    {
        opts
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
            .EnableSensitiveDataLogging(true);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
    //await SeedDatabase();
}
else
{
    //await SeedDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGraphQL("/api");
app.MapControllers();

app.Run();

async Task SeedDatabase()
{
    using var scope = app.Services.CreateScope();

    var SaveTheFoodSeeder = scope.ServiceProvider.GetRequiredService<SaveTheFoodSeedData>();
    await SaveTheFoodSeeder.EnsurePopulated(true);
}