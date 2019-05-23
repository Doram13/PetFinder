using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetFinder.Core.Models;
using PetFinder.Data;
using PetFinder.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.InMemoryTester
{
    public class PostServiceTest
    {
        private SqliteConnection Connection;
        private DbContextOptions<ApplicationDbContext> Options;

        [SetUp]
        public async Task Setup()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(Connection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                var post1 = new Post()
                {
                    Title = "First Test Post",
                    Description = "First Test Post decription",
                    IsActive = true,
                    PostDate = DateTime.Now,
                    PostType = PostTypes.LOST,
                    PostedPet = new Pet()
                    {
                        AnimalType = AnimalTypes.DOG,
                        Name = "Mici",
                        PicturePath = @"\test\picture",
                        Tags = new Dictionary<Tag, bool>(),
                        SeenDetail = new SeenDetail()
                        {
                            Location = "Meter street",
                            SeenTime = DateTime.Now
                        }
                    }
                };
                var post2 = new Post()
                {
                    Title = "Second Test Post",
                    Description = "Second Test Post decription",
                    IsActive = false,
                    PostDate = DateTime.Now,
                    PostType = PostTypes.SEEN,
                    PostedPet = new Pet()
                    {
                        AnimalType = AnimalTypes.DOG,
                        Name = "Pici",
                        PicturePath = @"\test\picture",
                        Tags = new Dictionary<Tag, bool>(),
                        SeenDetail = new SeenDetail()
                        {
                            Location = "Lucky street",
                            SeenTime = DateTime.Now
                        }
                    }
                };
                var post3 = new Post()
                {
                    Title = "Third Test Post",
                    Description = "Third Test Post decription",
                    IsActive = false,
                    PostDate = DateTime.Now,
                    PostType = PostTypes.LOST,
                    PostedPet = new Pet()
                    {
                        AnimalType = AnimalTypes.CAT,
                        Name = "Mimi",
                        PicturePath = @"\test\picture",
                        Tags = new Dictionary<Tag, bool>(),
                        SeenDetail = new SeenDetail()
                        {
                            Location = "Mountain street",
                            SeenTime = DateTime.Now
                        }
                    }
                };

                var post4 =new Post()
                {
                    Title = "Fourth Test Post",
                    Description = "Fourth Test Post decription",
                    IsActive = true,
                    PostDate = DateTime.Now,
                    PostType = PostTypes.SEEN,
                    PostedPet = new Pet()
                    {
                        AnimalType = AnimalTypes.CAT,
                        Name = "Pipi",
                        PicturePath = @"\test\picture",
                        Tags = new Dictionary<Tag, bool>(),
                        SeenDetail = new SeenDetail()
                        {
                            Location = "Deep street",
                            SeenTime = DateTime.Now
                        }
                    }
                };

                foreach (Tag tag in (Tag[])Enum.GetValues(typeof(Tag)))
                {
                    post1.PostedPet.Tags.Add(tag, false);
                    post2.PostedPet.Tags.Add(tag, false);
                    post3.PostedPet.Tags.Add(tag, false);
                    post4.PostedPet.Tags.Add(tag, false);
                }

                await service.SavePostAsync(post1);
                await service.SavePostAsync(post2);
                await service.SavePostAsync(post3);
                await service.SavePostAsync(post4);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Connection.Close();
        }

        [Test]
        public async Task SavePostAsync_Should_Insert_One_Post_Into_Db()
        {
            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                await service.SavePostAsync(new Post() { Title = "SavePostAsync Test post", Description = "Decription", IsActive = true, PostDate = DateTime.Now });
            }

            // Assert
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.That(context.Posts.Count(), Is.EqualTo(5));
            }
        }

        [Test]
        public async Task GetAllActivePosts_Should_Return_Two_Long_List()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllActivePosts();
            }

            // Assert
            Assert.That(posts.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllActivePosts_Should_Return_Element_Active()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllActivePosts();
            }

            // Assert
            Assert.That(posts[0].IsActive, Is.EqualTo(true));
        }
        
        [Test]
        public async Task GetPostById_Should_Return_The_Right_Post()
        {
            Post posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetPostById(1);
            }

            // Assert
            Assert.That(posts.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllSeenPetPosts_Should_Return_One_Long_List()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllSeenPetPosts();
            }

            // Assert
            Assert.That(posts.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllSeenPetPosts_Should_Return_Seen_Type_Post()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllSeenPetPosts();
            }

            // Assert
            Assert.That(posts[0].PostType, Is.EqualTo(PostTypes.SEEN));
        }

        [Test]
        public async Task GetAllLostPetPosts_Should_Return_One_Long_List()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllLostPetPosts();
            }

            // Assert
            Assert.That(posts.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllLostPetPosts_Should_Return_Lost_Type_Post()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllLostPetPosts();
            }

            // Assert
            Assert.That(posts[0].PostType, Is.EqualTo(PostTypes.LOST));
        }

        [Test]
        public async Task GetAllPosts_Should_Return_Four_Long_List()
        {
            List<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllPosts();
            }

            // Assert
            Assert.That(posts.Count, Is.EqualTo(4));
        }

        [Test]
        public async Task GetAllPostWithSearchStringAsync_Should_Return_One_Long_List()
        {
            IEnumerable<Post> posts;

            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                posts = await service.GetAllPostWithSearchStringAsync("First");
            }

            // Assert
            Assert.That(posts.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAsync_Should_Remove_An_Item()
        {
            // Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PostService(context);
                await service.DeleteAsync(new Post()
                {
                    Id = 4,
                    Title = "Fourth Test Post",
                    Description = "Fourth Test Post decription",
                    IsActive = true,
                    PostDate = DateTime.Now,
                    PostType = PostTypes.SEEN,
                    PostedPet = new Pet()
                    {
                        AnimalType = AnimalTypes.CAT,
                        Name = "Pipi",
                        PicturePath = @"\test\picture",
                        Tags = new Dictionary<Tag, bool>(),
                        SeenDetail = new SeenDetail()
                        {
                            Location = "Deep street",
                            SeenTime = DateTime.Now
                        }
                    }
                });
            }

            // Assert
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.That(context.Posts.Count(), Is.EqualTo(3));
            }
        }
    }
}
