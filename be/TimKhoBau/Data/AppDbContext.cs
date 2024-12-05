using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using TimKhoBau.Model.KhoBau;

namespace TimKhoBau.Data
{
    public class AppDbContext: DbContext
    {

        public DbSet<KhobauEntity> khobauEntities { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

    }
}
