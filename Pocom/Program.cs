using Pocom.DAL;
using Microsoft.EntityFrameworkCore;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Services;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using Pocom.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IRepository<Post>, Repository<Post>>();
builder.Services.AddTransient<IPostService, PostService>();

builder.Services.AddDbContext<PocomContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin());
});


var app = builder.Build();

app.UseRouting();
app.MapGet("/", () => "Hello World!");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();