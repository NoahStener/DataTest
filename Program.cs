using DataTest.Data;
using DataTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DataManipulator>();
builder.Services.AddScoped<EnergyAnalysisService>();
builder.Services.AddScoped<MyCsvReader>();
builder.Services.AddScoped<ApplianceCostCalculator>();


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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