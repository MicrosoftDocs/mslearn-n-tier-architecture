using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingData.Models
{
    public class VotingDBContext : DbContext
    {
        public static void CreateTablesIfNotExists(VotingDBContext dbContext)
        {
            using (var connection = dbContext.Database.GetDbConnection()) {
                connection.Open();    
                using (var command = connection.CreateCommand()) {
                    command.CommandText = @"
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'Counts')
  CREATE TABLE Counts(ID INT NOT NULL IDENTITY PRIMARY KEY, Candidate VARCHAR(32) NOT NULL, Count INT)";
                    var result = command.ExecuteNonQuery();
                }
            }
        }

        public VotingDBContext()
        {
        }

        public VotingDBContext(DbContextOptions<VotingDBContext> options)
            : base(options)
        {
        }

        public DbSet<Counts> Counts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counts>(entity =>
            {
                entity.Property(e => e.Candidate).IsRequired();
            });
        }
    }
}
