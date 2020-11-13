using API.Models.Expenditure;
using API.Models.Item;
using API.Models.Partner;
using API.Models.Project;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Data
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
        {
        }

        public DbSet<ProjectModel> project { get; set; }
        public DbSet<PartnerModel> partner { get; set; }
        public DbSet<project_partnerModel> project_partner { get; set; }
        public DbSet<ExpenditureModel> expenditure { get; set; }
        public DbSet<ItemModel> item { get; set; }

    }
}