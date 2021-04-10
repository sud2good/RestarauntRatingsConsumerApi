using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntRatingsConsumerApi.Context
{
    public class RatingsDbContext : DbContext
    {
        public RatingsDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<RatingsModel> Employees { get; set; }


    }
}
