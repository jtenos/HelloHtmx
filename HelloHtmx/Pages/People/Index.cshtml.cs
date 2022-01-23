using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HelloHtmx.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HelloHtmx.Pages.People;

public class IndexModel
    : PageModel
{
    public IEnumerable<Person> People { get; set; } = default!;

    public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
    {
        using SqliteConnection conn = new(Program.CONN_STR);
        await conn.OpenAsync(cancellationToken);
        People = await conn.QueryAsync<Person>(@"SELECT * FROM People;");
        return Page();
    }
}
