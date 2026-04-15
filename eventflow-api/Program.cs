using Eventflow.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

//Authentication
 var jwt = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["issuer"],
        ValidAudience = jwt["audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8
            .GetBytes(jwt["SecretKey"]!))
    };
});
builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddScoped<AuthService>();
//Later when we add more services just remove comments

//builder.Services.AddScoped<EventService>();
//builder.Services.AddScoped<TicketService>();
//builder.Services.AddScoped<NotificationService>();
//builder.Services.AddScoped<AdminService>();

builder.Services.AddScoped<JwtHelper>();
//builder.Services.AddSignalR(); //Also later ba3den.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
options.AddPolicy("ReactApp", policy =>
policy.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()));

var app = builder.Build();

//middleware for global exception handling, we will create a custom middleware class for this later
//app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("ReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//for real time feautures
//app.MapHub<EventFlowHub>("/hubs/eventflow");

/* Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

*/
app.Run();
