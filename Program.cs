using Microsoft.EntityFrameworkCore;
using lapGen.persistance;
using lapGen.Utils.Interfaces;
using lapGen.Utils.Implimentations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages().AddRazorPagesOptions(options => {
//         options.RootDirectory = "/Pages";
//     });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataCon>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("connect")));
//add dbAssist
builder.Services.AddScoped<DataCon,DataCon>();
//add interface
builder.Services.AddScoped<ITest,test>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //builder.Services.AddScoped<ITest,test>(); // give real class
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //builder.Services.AddScoped<ITest,FakeTest>(); //give dum
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataCon>();
    //context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}

builder.Services.AddDistributedMemoryCache();



app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();
app.Run();
