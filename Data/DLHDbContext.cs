using DLHAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DLHAPI.Data
{
    public class DLHDbContext : DbContext
    {
        public DLHDbContext(DbContextOptions<DLHDbContext> options) : base(options)
        {
        }

        public DbSet<DlhModel> DlhModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DlhModel>().HasData(
            //                    new DlhModel
            //                    {
            //                        Id = 11,
            //                        Name ="Test Name1",
            //                        Dob = DateTime.Now,
            //                        Address = "York Street, New jersy",
            //                        DateOfIssue = DateTime.Now,
            //                        DateOfExpire = DateTime.Now,
            //                    },
            //                    new DlhModel
            //                    {
            //                        Id = 1,
            //                        Name = "Test Name2",
            //                        Dob = DateTime.Now,
            //                        Address = "Washinton Dc",
            //                        DateOfIssue = DateTime.Now,
            //                        DateOfExpire = DateTime.Now,
            //                    },
            //                    new DlhModel
            //                    {
            //                        Id = 3,
            //                        Name = "Test Name3",
            //                        Dob = DateTime.Now,
            //                        Address = "Brooks Street",
            //                        DateOfIssue = DateTime.Now,
            //                        DateOfExpire = DateTime.Now,
            //                    }

            //    );
        }
    }
}
