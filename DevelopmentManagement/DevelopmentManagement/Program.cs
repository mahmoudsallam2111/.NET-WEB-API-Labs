using DevelopmentManagement.BL;
using DevelopmentManagement.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Database configuration
var connetionstring = builder.Configuration.GetConnectionString("connction_string");

builder.Services.AddDbContext<DevelopementContext>(
    options => options.UseSqlServer(connetionstring));

// by default AddDbContext is scoped , as its not prefered that two or more request access the db at the same time
// this is to prevent concurrency
#endregion

#region Repos

builder.Services.AddScoped<ITicketRepository,TicketRepository>();
builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();


#endregion

#region ManagerInjection

builder.Services.AddScoped<ITicketManager, TicketManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();

#endregion



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

app.Run();
