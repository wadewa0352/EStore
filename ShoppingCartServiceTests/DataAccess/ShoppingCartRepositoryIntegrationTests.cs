using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using MongoDB.Driver;
using ShoppingCartService.Config;
using ShoppingCartService.DataAccess;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartServiceTests.Builders;
using ShoppingCartServiceTests.Fixtures;
using Xunit;

namespace ShoppingCartServiceTests.DataAccess
{
    [Collection("Dockerized MongoDB collection")]
    public class ShoppingCartRepositoryIntegrationTests : IDisposable

    {
        private readonly ShoppingCartDatabaseSettings _databaseSettings;
        private const string Invalid_ID = "507f191e810c19729de860ea";

        public ShoppingCartRepositoryIntegrationTests(DockerMongoFixture fixture)
        {
            _databaseSettings = fixture.GetDatabaseSettings();
        }

        [Fact]
        public void FindAll_NoCartsInDB_ReturnEmptyList()
        {
            // Add test code
        }

        [Fact]
        public void FindAll_HasTwoCartsInDB_ReturnAllCarts()
        {
            // Add test code
        }

        [Fact]
        public void GetById_hasThreeCartsInDB_returnReturnOnlyCartWithCorrectId()
        {

        }

        [Fact]
        public void GetById_CartNotFound_ReturnNull()
        {

        }

        [Fact]
        public void Update_CartNotFound_DoNotFail()
        {

        }

        [Fact]
        public void Update_CartFound_UpdateValue()
        {

        }

        [Fact]
        public void Remove_CartFound_RemoveFromDb()
        {

        }

        [Fact]
        public void RemoveById_CartFound_RemoveFromDb()
        {

        }

        public void Dispose()
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            client.DropDatabase(_databaseSettings.DatabaseName);
        }
    }
}