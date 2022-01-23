using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HelloHtmx.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HelloHtmx.Pages.People
{
    public class EditRowModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public long PersonID { get; set; }

        public Person Person { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            using SqliteConnection conn = new(Program.CONN_STR);
            await conn.OpenAsync();
            object parms = new { PersonID };
            Person = (await conn.QueryAsync<Person>(
                sql: @"SELECT * FROM People WHERE PersonID = @PersonID",
                param: parms
            )).First();

            await Task.Delay(750);
            return Page();
        }
    }
}
