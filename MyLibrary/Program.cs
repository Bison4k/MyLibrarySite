using Microsoft.EntityFrameworkCore;
using MyLibrary.DataBase;
using MyLibrary.DataBase.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<LibraryContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryContext")));
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();
app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/books");
    return Task.CompletedTask;
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();