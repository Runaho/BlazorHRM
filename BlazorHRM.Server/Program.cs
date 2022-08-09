using System.Reflection;
using BlazorHRM.Server.Models;
using BlazorHRM.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.SetIsOriginAllowed(origin => true);
        });
});


string mySqlConnectionStr = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HRM minimal api",
        Description = "An ASP.NET Core Web API for HRM Base"
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employees", (IEmployeeRepository employeeRepository) => employeeRepository.GetAllEmployees());
app.MapGet("/employee", (int id, IEmployeeRepository employeeRepository) => employeeRepository.GetEmployeeById(id));

app.MapGet("/Countries", (ICountryRepository countryRepository) => countryRepository.GetAllCountries());
app.MapGet("/Country", (int id, ICountryRepository countryRepository) => countryRepository.GetCountryById(id));

app.MapGet("/jobCategories", (IJobCategoryRepository jobCategoryRepository) => jobCategoryRepository.GetAllJobCategories());
app.MapGet("/jobCategory", (int id, IJobCategoryRepository jobCategoryRepository) => jobCategoryRepository.GetJobCategoryById(id));
app.MapDelete("/jobCategory", (int id, IJobCategoryRepository jobCategoryRepository) => jobCategoryRepository.DeleteJobCategory(id));
app.MapPost("/jobCategory", (JobCategory JobCategory, IJobCategoryRepository jobCategoryRepository) => jobCategoryRepository.AddJobCategory(JobCategory));
app.MapPut("/jobCategory", (JobCategory JobCategory, IJobCategoryRepository jobCategoryRepository) => jobCategoryRepository.UpdateJobCategory(JobCategory));

app.MapPut("/employee", (Employee employee, IEmployeeRepository employeeRepository) => employeeRepository.UpdateEmployee(employee));
app.MapPost("/employee", (Employee employee, IEmployeeRepository employeeRepository) => employeeRepository.AddEmployee(employee));
app.MapDelete("/employee", (int id, IEmployeeRepository employeeRepository) => employeeRepository.DeleteEmployee(id));

app.UseCors();

app.Run();
