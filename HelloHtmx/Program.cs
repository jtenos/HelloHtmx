/*
 If you haven't already, run this to install the SCSS compiler:

npm install -g sass
 */

using Dapper;
using HelloHtmx.DataModels;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(option =>
{
    option.LowercaseUrls = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

await CreateDatabaseAsync();

app.Run();

async Task CreateDatabaseAsync()
{
    using SqliteConnection conn = new(Program.CONN_STR);
    await conn.OpenAsync();

    Console.WriteLine("Creating table");
    await conn.ExecuteAsync(@"
        CREATE TABLE IF NOT EXISTS People (
            PersonID INTEGER NOT NULL PRIMARY KEY
            ,Name TEXT NOT NULL
            ,Age INTEGER NOT NULL
        );
        DELETE FROM People;
    ");

    Console.WriteLine("Adding people");
    for (int i = 0; i < 100; ++i)
    {
        Console.Write(".");
        Person p = Person.Generate();
        object parms = new { p.Name, p.Age };
        await conn.ExecuteAsync(
            @"INSERT INTO People (Name, Age) VALUES (@Name, @Age);",
            parms);
    }
    Console.WriteLine();
}

partial class Program
{
    public const string CONN_STR = "Data Source=/tmp/hello-htmx.db";
}
