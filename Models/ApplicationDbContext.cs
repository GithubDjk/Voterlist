using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoterListApp.Models
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Citizen> Citizens { get; set; }

    }
}
