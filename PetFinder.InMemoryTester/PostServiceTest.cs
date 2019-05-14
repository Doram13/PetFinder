﻿using Microsoft.Data.Sqlite;
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
                await service.SavePostAsync(new Post() { Title = "First Test Post", Description = "First Test Post decription", IsActive = true, PostDate = DateTime.Now });
                await service.SavePostAsync(new Post() { Title = "Second Test Post", Description = "Second Test Post decription", IsActive = true, PostDate = DateTime.Now });
                await service.SavePostAsync(new Post() { Title = "Third Test Post", Description = "Third Test Post decription", IsActive = false, PostDate = DateTime.Now });
                await service.SavePostAsync(new Post() { Title = "Fourth Test Post", Description = "Fourth Test Post decription", IsActive = false, PostDate = DateTime.Now });
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

        // This Post fails because SavePostAsync changes state to true!!!
        [Test]
        public async Task GetAllActivePosts_Should_Return_Just_Active_Posts()
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
    }
}
