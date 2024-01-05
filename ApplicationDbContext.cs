using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVApi
{
    public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
}
    
}