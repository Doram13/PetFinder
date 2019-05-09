using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetFinder.Core.Models;
using PetFinder.Data;
using PetFinder.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.InMemoryTester
{
    public class PostServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SavePostAsync_Shoul_Insert_One_Post_Into_Db()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var service = new PostService(context);
                    await service.SavePostAsync(new Post() { Title = "Test Post", Description = "Test Post decription", IsActive = true, PostDate = DateTime.Now });
                }

                using (var context = new ApplicationDbContext(options))
                {
                    Assert.That(context.Posts.Count(), Is.EqualTo(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
