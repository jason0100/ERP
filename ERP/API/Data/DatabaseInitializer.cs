using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(ErpDbContext context)
        {
            context.Database.EnsureCreated();


        }
    }
}
