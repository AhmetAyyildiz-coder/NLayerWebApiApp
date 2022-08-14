using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Services.Mapping;
using NLayer.Services.Services;
using NLayer.Services.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepostiory<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

//product service instance
builder.Services.AddScoped(typeof(IProductRepository),typeof(ProductRepository));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));

//Category service Ýnstance
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

//automapper added
builder.Services.AddAutoMapper(typeof(MapProfile));

//added notFoundFilter
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//db context eklenildi ve diðer katmanda olan dbcontext nesnemizi migrations yapýlýrken nasýl kullandýðýmýz
//gösterildi.
builder.Services.AddDbContext<NLayer.Repository.AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
        opt =>
        {
            opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
        });
});

//Fluent Validation Eklentisi
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers(options =>
//filter Eklenildi.
{
    options.Filters.Add(new ValidateFilterAttribute());
}).
AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidation>());

//Bunu diyerek default olan filtrer mekanizmasýný devre dýþý býraktýk ve custom bizim yazdýðýmýz
//filter mekanizmasý eklenildi.
builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);


#pragma warning restore CS0618 // Type or member is obsolete
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UserCustomerException();
app.UseAuthorization();

app.MapControllers();

app.Run();
