using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StockAppli.Models;
using StockAppli.Models.Collections.Sessions;
using StockAppli.Models.Collections.Users;
using StockAppli_API.Extensions;
using StockAppli_API.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection((nameof(AppSettings))));

// Collection
builder.Services.AddSingleton<IUserCollection, UserCollection>();
builder.Services.AddSingleton<ISessionCollection, SessionCollection>();
//Services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

builder.Services.AddAutoMapper(typeof(Program));

// JWT
builder.Services.AddAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
