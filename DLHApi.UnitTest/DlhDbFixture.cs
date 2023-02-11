using DLHApi.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.UnitTest
{
    public class DlhDbFixture
    {
        private static string DlhDbConnectionString = $"Server={Environment.GetEnvironmentVariable("DlhDBServer")};Database={Environment.GetEnvironmentVariable("DlhDBName")};User Id={Environment.GetEnvironmentVariable("DlhDbUserId")};Password={Environment.GetEnvironmentVariable("DlhDbPassword")};TrustServerCertificate=True";
        private static string DlhAuditConnectionString = $"Server={Environment.GetEnvironmentVariable("AuditDBServer")};Database={Environment.GetEnvironmentVariable("AuditDBName")};User Id={Environment.GetEnvironmentVariable("AuditDbUserId")};Password={Environment.GetEnvironmentVariable("AuditDbPassword")};TrustServerCertificate=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public DlhDbFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateDlhContext())
                    {
                        //context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        //context.SaveChanges();
                    }

                    using (var context = CreateDlhAuditContext())
                    {
                        //context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        //context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public DlhdevDbContext CreateDlhContext()
            => new DlhdevDbContext(
                new DbContextOptionsBuilder<DlhdevDbContext>()
                    .UseSqlServer(DlhDbConnectionString)
                    .Options);

        public DlhdevAuditContext CreateDlhAuditContext()
            => new DlhdevAuditContext(
                new DbContextOptionsBuilder<DlhdevAuditContext>()
                    .UseSqlServer(DlhAuditConnectionString)
                    .Options);
    }
}
