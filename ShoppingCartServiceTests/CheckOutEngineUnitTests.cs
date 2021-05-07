using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingCartServiceTests
{

    public class CheckOutEngineUnitTests
    {
        private readonly IMapper _mapper;

        public static List<object[]> TestData()
        {
            return new List<object[]>
            {
                new object[] { CustomerType.Standard, new List<Item> { new Item { ProductId = "1", Price = 2, Quantity = 2 } }},
                new object[] { CustomerType.Standard, new List<Item> { 
                    new Item { ProductId = "1", Price = 2, Quantity = 2 },
                    new Item { ProductId = "2", Price = 2, Quantity = 2 } }},
                new object[] { CustomerType.Premium, new List<Item> { new Item { ProductId = "1", Price = 2, Quantity = 2 } }},
                new object[] { CustomerType.Premium, new List<Item> {
                    new Item { ProductId = "1", Price = 2, Quantity = 2 },
                    new Item { ProductId = "2", Price = 2, Quantity = 2 } }},
            };            
        }

        public CheckOutEngineUnitTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Theory]
        [InlineData(CustomerType.Standard, 0)]
        [InlineData(CustomerType.Premium, 10)]
        public void CalculateTotals_TestCustomerDiscount(CustomerType customerType, int expectedResult)
        {
            var address = GetAddress();

            var checkOutEngine = new CheckOutEngine(new ShippingCalculator(address), _mapper);

            var cart = new Cart
            {
                CustomerType = customerType,
                Items = new() { new Item { ProductId = "1", Price = 1, Quantity = 1 } },
                ShippingAddress = address
            };

            var result = checkOutEngine.CalculateTotals(cart);

            Assert.Equal(expectedResult, result.CustomerDiscount);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateTotals_TotalItemCostIsCalculatedCorrectly(CustomerType customerType, List<Item> items)
        {
            var origin = GetAddress();

            var destination = GetAddress(street: "12 Main St");

            var target = new CheckOutEngine(new ShippingCalculator(origin), _mapper);

            var cart = CreateCart(customerType, destination, items);

            var result = target.CalculateTotals(cart);

            Assert.Equal(GetExpectedTotalCost(items, result.ShippingCost, customerType), result.Total);
        }

        // Factory Methods
        private double GetExpectedTotalCost(List<Item> items, double shippingCost, CustomerType customerType)
        {
            var itemCost = items.Sum(item => item.Price * item.Quantity);

            return (itemCost + shippingCost) * (customerType == CustomerType.Premium ? .9 : 1);

        }

        private Address GetAddress(string street = "1 Main St", string city = "Anywhere", string country = "USA")
        {
            return new Address()
            {
                Street = street,
                City = city,
                Country = country
            };
        }   
        
        private Cart CreateCart(CustomerType customerType, Address shippingAddress, List<Item> items)
        {
            return new Cart
            {
                CustomerType = customerType,
                Items = items,
                ShippingAddress = shippingAddress
            };
        }

    }


}
