using System;
using Microsoft.EntityFrameworkCore;

namespace DockerFunTimes.Features.Fun
{
    public class TerribleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class TerribleEntityContext: DbContext
    {
        public TerribleEntityContext(DbContextOptions<TerribleEntityContext> options)
            : base(options)
        { }

        public DbSet<TerribleEntity> Foo { get; set; }
    }
}