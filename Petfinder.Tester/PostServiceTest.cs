using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using PetFinder.Core.Models;
using PetFinder.Data;
using PetFinder.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class PostServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SavePostAsync_Should_Add_One_Post()
        {
            var mockSet = Substitute.For<DbSet<Post>>();
            var mockContext = Substitute.For<ApplicationDbContext>();
            mockContext.Posts.Returns(mockSet);

            var service = new PostService(mockContext);

            // Act
            service.SavePostAsync(new Post {
                Description ="Test Post's description",
                IsActive =true, PostDate=DateTime.Now,
                Title ="Test Post",
                PostType = PostTypes.SEEN});

            // Assert
            mockSet.Received(1).Add(Arg.Any<Post>());
            mockContext.Received(1).SaveChangesAsync();
        }

        [Test]
        public void GetAllPosts_Should_Return_All_Posts()
        {
            var mockPosts = new List<Post>()
            {
                new Post {Title = "First test post", Description = "First test post desc.", PostDate = DateTime.Now, IsActive = true},
                new Post {Title = "Second test post", Description = "Second test post desc.", PostDate = DateTime.Now, IsActive = true}
            }.AsQueryable();

            // Create a mock DbSet exposing for both DbSet and Iqueryable interfaces for setup
            var mockSet = Substitute.For<DbSet<Post>, IQueryable<Post>>();

            // setup all IQueryable methods using what you have from "data"
            ((IQueryable<Post>)mockSet).Provider.Returns(mockPosts.Provider);
            ((IQueryable<Post>)mockSet).Expression.Returns(mockPosts.Expression);
            ((IQueryable<Post>)mockSet).ElementType.Returns(mockPosts.ElementType);
            ((IQueryable<Post>)mockSet).GetEnumerator().Returns(mockPosts.GetEnumerator());

            // do the wiring between DbContext and DbSet
            var mockContext = Substitute.For<ApplicationDbContext>();
            mockContext.Posts.Returns(mockSet);
            var service = new PostService(mockContext);

            // Act
            var posts = service.GetAllPosts();

            // Assert
            Assert.That(posts.Count(), Is.EqualTo(2));
            Assert.That(posts[0].Title, Is.EqualTo("First test post"));
            Assert.That(posts[1].Title, Is.EqualTo("Second test post"));
        }

        [Test]
        public void GetAllActivePosts_Should_Return_Just_Active_Posts()
        {
            var mockPosts = new List<Post>()
            {
                new Post {Title = "First test post", Description = "First test post desc.", PostDate = DateTime.Now, IsActive = true},
                new Post {Title = "Second test post", Description = "Second test post desc.", PostDate = DateTime.Now, IsActive = false}
            }.AsQueryable();

            // Create a mock DbSet exposing for both DbSet and Iqueryable interfaces for setup
            var mockSet = Substitute.For<DbSet<Post>, IQueryable<Post>>();

            // setup all IQueryable methods using what you have from "data"
            ((IQueryable<Post>)mockSet).Provider.Returns(mockPosts.Provider);
            ((IQueryable<Post>)mockSet).Expression.Returns(mockPosts.Expression);
            ((IQueryable<Post>)mockSet).ElementType.Returns(mockPosts.ElementType);
            ((IQueryable<Post>)mockSet).GetEnumerator().Returns(mockPosts.GetEnumerator());

            // do the wiring between DbContext and DbSet
            var mockContext = Substitute.For<ApplicationDbContext>();
            mockContext.Posts.Returns(mockSet);
            var service = new PostService(mockContext);

            // Act
            var activePosts = service.GetAllActivePosts();

            // Assert
            Assert.That(activePosts.Count(), Is.EqualTo(1));
            Assert.That(activePosts[0].Title, Is.EqualTo("First test post"));
            Assert.That(activePosts[0].IsActive, Is.EqualTo(true));
        }
    }
}