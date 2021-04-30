using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using ShoppingCartService.Models;
using System;
using Xunit;

namespace ShoppingCartServiceTests
{
    public class CheckOutEngineUnitTests
    {
        private readonly IMapper _mapper;

        public CheckOutEngineUnitTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void CalculateTotals_StandardCustomerDiscount_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var checkOutEngine = new CheckOutEngine(new ShippingCalculator(address), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                Items = new() { new Item { ProductId = "1", Price = 1, Quantity = 1 } },
                ShippingAddress = address
            };

            var result = checkOutEngine.CalculateTotals(cart);

            Assert.Equal(0, result.CustomerDiscount);
        }

        [Fact]
        public void CalculateTotals_StandardCustomerItemCostForOneItemWithQuantityOfTwo_IsFour()
        {
            var origin = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var destination = new Address()
            {
                Street = "12 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var target = new CheckOutEngine(new ShippingCalculator(origin), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                Items = new() { new Item { ProductId = "1", Price = 2, Quantity = 2 } },
                ShippingAddress = destination
            };

            var result = target.CalculateTotals(cart);

            Assert.Equal(4, result.Total - result.ShippingCost);
        }

        [Fact]
        public void CalculateTotals_StandardCustomerItemCostForTwoItemsWithQuantityOfTwo_IsEight()
        {
            var origin = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var destination = new Address()
            {
                Street = "12 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var target = new CheckOutEngine(new ShippingCalculator(origin), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                Items = new()
                {
                    new Item { ProductId = "1", Price = 2, Quantity = 2 },
                    new Item { ProductId = "2", Price = 2, Quantity = 2 }
                },
                ShippingAddress = destination
            };

            var result = target.CalculateTotals(cart);

            Assert.Equal(8 , result.Total - result.ShippingCost);
        }

        [Fact]
        public void CalculateTotals_PremiumCustomer_HasCustomerDiscount()
        {
            var address = new Address { Country = "country", City = "city", Street = "street" };

            var target = new CheckOutEngine(new ShippingCalculator(address), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                Items = new() { new Item { ProductId = "1", Price = 1, Quantity = 1 } },
                ShippingAddress = address
            };

            var result = target.CalculateTotals(cart);

            Assert.NotEqual(0, result.CustomerDiscount);
        }

        [Fact]
        public void CalculateTotals_PremiumCustomerItemCostForOneItemWithQuantityOfTwo_HasDiscountApplied()
        {
            var origin = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var destination = new Address()
            {
                Street = "12 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var target = new CheckOutEngine(new ShippingCalculator(origin), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                Items = new() { new Item { ProductId = "1", Price = 2, Quantity = 2 } },
                ShippingAddress = destination
            };

            var result = target.CalculateTotals(cart);

            Assert.Equal((4 * .9), result.Total - result.ShippingCost);
        }

        [Fact]
        public void CalculateTotals_PremiumCustomerItemCostForTwoItemsWithQuantityOfTwo_HasDiscountApplied()
        {
            var origin = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var destination = new Address()
            {
                Street = "12 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var target = new CheckOutEngine(new ShippingCalculator(origin), _mapper);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                Items = new()
                {
                    new Item { ProductId = "1", Price = 2, Quantity = 2 },
                    new Item { ProductId = "2", Price = 2, Quantity = 2 }
                },
                ShippingAddress = destination
            };

            var result = target.CalculateTotals(cart);

            Assert.Equal((8 * .9), result.Total - result.ShippingCost);
        }

    }
}
