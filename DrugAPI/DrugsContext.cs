using DrugAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugAPI
{
    public class DrugsContext : DbContext
    {
        public DrugsContext(DbContextOptions<DrugsContext> options) : base(options)
        {

        }

        public DbSet<DrugDetails> Drug_Details { get; set; }

        public DbSet<DrugLocation> Drug_Location { get; set; }
    }
}
