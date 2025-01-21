using AlliedSolutionsGlobalSdnBhd.Services;

var builder = WebApplication.CreateBuilder(args);


/*builder.Services.AddTransient(typeof(AccessDatabaseService<>), provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("AccessDbConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'AccessDbConnection' is missing or empty.");
    }
    // Assuming you know the type you want to use here, e.g., AccessDatabaseService<MyEntity>
    return Activator.CreateInstance(typeof(AccessDatabaseService<>).MakeGenericType(typeof(MyEntity)), connectionString);
});*/


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient(typeof(AccessDatabaseService<>));
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
