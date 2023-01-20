using Core.DomainServices;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Infrastructure.TM_EF;
using Microsoft.EntityFrameworkCore;
using WebAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SaveTheFoodDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services
    .AddScoped<SaveTheFoodSeedData>()
    .AddScoped<IPakketRepository, PakketRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IStudentRepository, StudentRepository>()
    .AddScoped<PakketQuery>();

builder.Services
    .AddGraphQL(
        pakket => SchemaBuilder.New().AddServices(pakket)
            .AddType<PakketGraphQLTypes>()
            .AddQueryType<PakketQuery>()
            .Create());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UsePlayground(new PlaygroundOptions
    {
        QueryPath = "/api",
        Path = "/playground"
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapGraphQL("/api");
app.MapControllers();


app.Run();