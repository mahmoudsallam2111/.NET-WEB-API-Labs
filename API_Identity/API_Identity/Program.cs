using API_Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Default
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Database configuration
var connetionstring = builder.Configuration.GetConnectionString("connction_string");

builder.Services.AddDbContext<SchoolContext>(
    options => options.UseSqlServer(connetionstring));
#endregion


#region configure usermanager
builder.Services.AddIdentity<Student, IdentityRole>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    options.User.RequireUniqueEmail = true;


}).
    AddEntityFrameworkStores<SchoolContext>();

#endregion                                

#region Configure Authentication scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "myscheme";
    options.DefaultChallengeScheme = "myscheme"; 
   
}).AddJwtBearer("myscheme" , options =>
{
    var secretKeyString = builder.Configuration.GetValue<string>("SecretKey");
    var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString ?? "");
    SymmetricSecurityKey? secretKey = new SymmetricSecurityKey(secretKeyInBytes);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = secretKey,
        ValidateIssuer = false,                          /// who send token
        ValidateAudience = false                        // who  receeive token

    }; 
}
) ;


#endregion

#region Authorization Configuration

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminsonly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin").
        RequireClaim(ClaimTypes.NameIdentifier);     // this field must be exist regardless of value
    });

    options.AddPolicy("AdminsAndUsers", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin" , "User").
        RequireClaim(ClaimTypes.NameIdentifier);     // this field must be exist regardless of value
    });
}); 

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
