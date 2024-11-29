
using Microsoft.EntityFrameworkCore;
using Translator.Data;
using Translator.Interfaces;
using Translator.Repositories;
using Translator.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<TranslationService>();
builder.Services.AddScoped<ITranslatorRepository, TranslatorRepository>();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))); 

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();