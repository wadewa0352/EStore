using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingCartServiceTests.BusinessLogic
{
    public class ShoppingCalculatorUnitTests
    {
        public const double SameCityRate = 1.0;
        public const double SameCountryRate = 2.0;
        public const double InternationalShippingRate = 15.0;

        private readonly Dictionary<ShippingMethod, double> shippingMethodMultiplier = new()
        {
            { ShippingMethod.Standard, 1.0 },
            { ShippingMethod.Expedited, 1.2 },
            { ShippingMethod.Priority, 2.0 },
            { ShippingMethod.Express, 2.5 },
        };

        public static List<object[]> TestData()
        {
            return new List<object[]>
            {
                new object[] { ShippingMethod.Standard, new List<Item>() },
                new object[] { ShippingMethod.Expedited, new List<Item>() },
                new object[] { ShippingMethod.Priority, new List<Item>() },
                new object[] { ShippingMethod.Express, new List<Item>() },
                new object[] { ShippingMethod.Standard, new List<Item>() { new() { Quantity = 1, Price = 1 } } },
                new object[] { ShippingMethod.Expedited, new List<Item>() { new() { Quantity = 1, Price = 1 } } },
                new object[] { ShippingMethod.Priority, new List<Item>() { new() { Quantity = 1, Price = 1 } } },
                new object[] { ShippingMethod.Express, new List<Item>() { new() { Quantity = 1, Price = 1 } } },
                new object[] { ShippingMethod.Standard, new List<Item>() { new() { Quantity = 1, Price = 1 } , new() { Quantity = 2, Price = 2} } },
                new object[] { ShippingMethod.Expedited, new List<Item>() { new() { Quantity = 1, Price = 1 }, new() { Quantity = 2, Price = 2 } } },
                new object[] { ShippingMethod.Priority, new List<Item>() { new() { Quantity = 1, Price = 1 }, new() { Quantity = 2, Price = 2 } } },
                new object[] { ShippingMethod.Express, new List<Item>() { new() { Quantity = 1, Price = 1 }, new() { Quantity = 2, Price = 2 } } },
            };

        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_StandardCustomerTravelCostSameCity(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Standard, GetAddress(), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Standard, shippingMethod, SameCityRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_PremiumCustomerTravelCostSameCity(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Premium, GetAddress(), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Premium, shippingMethod, SameCityRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_StandardCustomerTravelCostSameCountry(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Standard, GetAddress(city: "Anywhere Else"), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Standard, shippingMethod, SameCountryRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_PremiumCustomerTravelCostSameCountry(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Premium, GetAddress(city: "Anywhere Else"), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Premium, shippingMethod, SameCountryRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_StandardCustomerTravelCostInternational(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Standard, GetAddress("1 Cockney Lane", "London", "UK"), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Standard, shippingMethod, InternationalShippingRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateShippingCost_PremiumCustomerTravelCostInternational(ShippingMethod shippingMethod, List<Item> items)
        {

            var calculator = new ShippingCalculator(GetAddress());
            var cart = CreateCart(CustomerType.Premium, GetAddress("1 Cockney Lane", "London", "UK"), items, shippingMethod);

            var expectedResult = GetExpectedShippingCost(items, CustomerType.Premium, shippingMethod, InternationalShippingRate);

            Assert.Equal(expectedResult, calculator.CalculateShippingCost(cart));
        }

        // Factory methods
        private double GetExpectedShippingCost(List<Item> items, CustomerType customerType, ShippingMethod shippingMethod, double addressRate)
        {
            var itemCost = items.Sum(item => item.Price * item.Quantity);
            var baseCost = items.Sum(item => item.Quantity) * addressRate;

            if (customerType == CustomerType.Premium)
            {
                if (shippingMethod == ShippingMethod.Priority || shippingMethod == ShippingMethod.Expedited)
                {
                    return baseCost;
                }
            }

            return baseCost * shippingMethodMultiplier[shippingMethod];

        }

        private static Address GetAddress(string street = "1 Main St", string city = "Anywhere", string country = "USA")
        {
            return new Address()
            {
                Street = street,
                City = city,
                Country = country
            };
        }

        private Cart CreateCart(CustomerType customerType, Address shippingAddress, List<Item> items, ShippingMethod shippingMethod)
        {
            return new Cart
            {
                CustomerType = customerType,
                Items = items,
                ShippingAddress = shippingAddress,
                ShippingMethod = shippingMethod
            };
        }

    }
}
