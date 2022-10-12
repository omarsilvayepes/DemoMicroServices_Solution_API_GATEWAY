using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductWebApi.Models;

namespace ProductWebApi
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            try
            { 
                var dataBaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(dataBaseCreator != null)
                {
                    // Create Database if cannot connect
                    if(!dataBaseCreator.CanConnect()) dataBaseCreator.Create();

                    //creates Tables if no tables exist
                    if (!dataBaseCreator.HasTables()) dataBaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
