using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Config;
using ShoppingCartService.Controllers;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.DataAccess;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using ShoppingCartServiceTests.Builders;
using ShoppingCartServiceTests.Fixtures;
using Xunit;
using static ShoppingCartServiceTests.Builders.ItemBuilder;
using static ShoppingCartServiceTests.Builders.AddressBuilder;

namespace ShoppingCartServiceTests.Controllers
{
    [Collection("Dockerized MongoDB collection")]
    public class ShoppingCartControllerIntegrationTests : IDisposable
    {
        private readonly ShoppingCartDatabaseSettings _databaseSettings;
        private readonly IMapper _mapper;

        public ShoppingCartControllerIntegrationTests(DockerMongoFixture fixture)
        {
            _databaseSettings = fixture.GetDatabaseSettings();

            _mapper = fixture.Mapper;
        }

        [Fact]
        public void GetAll_HasOneCart_returnAllShoppingCartsInformation()
        {
            var repository = new ShoppingCartRepository(_databaseSettings);

            var cart = new CartBuilder()
                .WithId(null)
                .WithCustomerId("1")
                .WithItems(new List<Item> { CreateItem() })
                .Build();
            repository.Create(cart);


            var target = CreateShoppingCartController(repository);

            var actual = target.GetAll();

            var cartItem = cart.Items[0];
            var expected =
                new ShoppingCartDto
                {
                    Id = cart.Id,
                    CustomerId = cart.CustomerId,
                    CustomerType = cart.CustomerType,
                    ShippingAddress = cart.ShippingAddress,
                    ShippingMethod = cart.ShippingMethod,
                    Items = new List<ItemDto>
                    {
                        new(ProductId: cartItem.ProductId,
                            ProductName: cartItem.ProductName,
                            Price: cartItem.Price,
                            Quantity: cartItem.Quantity
                        )
                    }
                };

            Assert.Equal(expected, actual.Single());
        }

        [Fact]
        public void FindById_HasOneCartWithSameId_returnAllShoppingCartsInformation()
        {

        }


        [Fact]
        public void FindById_ItemNotFound_returnNotFoundResult()
        {

        }

        [Fact]
        public void CalculateTotals_ShoppingCartNotFound_ReturnNotFound()
        {

        }

        [Fact]
        public void CalculateTotals_ShippingCartFound_ReturnTotals()
        {

        }

        [Fact]
        public void Create_ValidData_SaveShoppingCartToDB()
        {

        }

        [Fact]
        public void Create_DuplicateItem_ReturnBadRequestResult()
        {

        }

        public static List<object[]> InvalidAddresses()
        {
            return new()
            {
                new object[] { null },
                new object[] { CreateAddress(country: null) },
                new object[] { CreateAddress(city: null) },
                new object[] { CreateAddress(street: null) },
            };
        }

        [Theory]
        [MemberData(nameof(InvalidAddresses))]
        public void Create_InValidAddress_ReturnBadRequestResult(Address address)
        {

        }

        [Fact]
        public void Delete_ValidData_RemoveShoppingCartToDB()
        {

        }

        private ShoppingCartController CreateShoppingCartController(ShoppingCartRepository repository)
        {
            return new(
                new ShoppingCartManager(repository, new AddressValidator(), _mapper,
                    new CheckOutEngine(new ShippingCalculator(), _mapper)), new NullLogger<ShoppingCartController>());
        }

        public void Dispose()
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            client.DropDatabase(_databaseSettings.DatabaseName);
        }
    }
}