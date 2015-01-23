using System.Data.Entity;

namespace Lisa.Sample.WebApi
{
    public class SampleContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }
}