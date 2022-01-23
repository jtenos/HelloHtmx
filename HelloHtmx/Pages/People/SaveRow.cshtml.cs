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
    public class SaveRowModel : PageModel
    {
        [BindProperty]
        public Person Person { get; set; } = default!;

        public async Task OnPost()
        {
            using SqliteConnection conn = new(Program.CONN_STR);
            await conn.OpenAsync();
            object parms = new { Person.PersonID, Person.Name, Person.Age };
            await conn.ExecuteAsync(
                sql: @"
                    UPDATE People
                    SET Name = @Name, Age = @Age
                    WHERE PersonID = @PersonID;
                ",
                param: parms
            );
            await Task.Delay(750);
        }
    }
}
