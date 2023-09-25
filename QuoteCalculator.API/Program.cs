using AutoMapper;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.API.Models;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer("name=ConnectionStrings:ApplicationDbContext"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

#endregion

#region Services

builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductFeeService, ProductFeeService>();
builder.Services.AddTransient<ILoanApplicationService, LoanApplicationService>();

#endregion

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


// Auto Mapper Configurations
//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});

//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {

            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
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

app.UseCors();

app.Run();
