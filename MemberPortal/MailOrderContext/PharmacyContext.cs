using MemberPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MemberPortal.MailOrderContext
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options)
        {

        }
        public DbSet<Drug> DrugDetails { get; set; }
        
    }
}