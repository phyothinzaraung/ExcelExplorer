using Microsoft.EntityFrameworkCore;
using ExcelExplorerApp.Models;

namespace ExcelExplorerApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<Item> Items {get; set;}
    }
}

