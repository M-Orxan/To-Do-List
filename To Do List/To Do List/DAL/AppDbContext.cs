using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   
        {

        }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
