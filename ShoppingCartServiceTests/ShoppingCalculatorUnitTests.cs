using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using System.Collections.Generic;
using Xunit;

namespace ShoppingCartServiceTests
{
    public class ShoppingCalculatorUnitTests
    {
        private readonly Dictionary<ShippingMethod, double> shippingMethodMultiplier = new()
        {
            { ShippingMethod.Standard, 1.0 },
            { ShippingMethod.Expedited, 1.2 },
            { ShippingMethod.Priority, 2.0 },
            { ShippingMethod.Express, 2.5 },
        };

        #region " Travel cost tests by customer type and item quantity "

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerNoItemsRateSameCity_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerNoItemsRateSameCountry_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerNoItemsRateInternational_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerOneItemRateSameCity_IsOne()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCityRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerOneItemRateSameCountry_IsTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCountryRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerOneItemRateInternational_Is15()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(ShippingCalculator.InternationalShippingRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerMultipleItemsRateSameCity_IsTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCityRate * 2, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerMultipleItemsRateSameCountry_IsFour()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCountryRate * 2, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostStandardCustomerMultipleItemsRateInternational_Is30()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(ShippingCalculator.InternationalShippingRate * 2, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerNoItemsRateSameCity_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerNoItemsRateSameCountry_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerNoItemsRateInternational_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(0, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerOneItemRateSameCity_IsOne()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCityRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerOneItemRateSameCountry_IsTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCountryRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerOneItemRateInternational_Is15()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(ShippingCalculator.InternationalShippingRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerMultipleItemsRateSameCity_IsTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCityRate * 2, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerMultipleItemsRateSameCountry_IsFour()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            Assert.Equal(ShippingCalculator.SameCountryRate * 2, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_TravelCostPremiumCustomerMultipleItemsRateInternational_Is30()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            Assert.Equal(ShippingCalculator.InternationalShippingRate * 2, calculator.CalculateShippingCost(cart));
        }

        #endregion

        #region " Shipping method cost tests by customer type, shipping method, and item count "

        #region " Standard Customer "

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerStandardShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpeditedShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerPriorityShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpressShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerStandardShippingMethodOneItem_IsStandardCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpeditedShippingMethodOneItem_IsExpeditedCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerPriorityShippingMethodOneItem_IsPriorityCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpressShippingMethodOneItem_IsExpressCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerStandardShippingMethodTwoItems_IsStandardCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpeditedShippingMethodTwoItems_IsExpeditedCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerPriorityShippingMethodTwoItems_IsPriorityCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostStandardCustomerExpressShippingMethoTwoItemsd_IsExpressCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        #endregion

        #region " Premium Customer "

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerStandardShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpeditedShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerPriorityShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpressShippingMethodNoItems_IsZero()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 0}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 0;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerStandardShippingMethodOneItem_IsStandardCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpeditedShippingMethodOneItem_IsBaseCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerPriorityShippingMethodOneItem_IsBaseCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpressShippingMethodOneItem_IsExpressCost()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 1}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 1 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerStandardShippingMethodTwoItems_IsStandardCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere", Country = "USA", Street = "12 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpeditedShippingMethodTwoItems_IsBaseCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Expedited,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "Anywhere Else", Country = "USA", Street = "1 Main St" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerPriorityShippingMethodTwoItems_IsBaseCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Priority,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate;
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        [Fact]
        public void CalculateShippingCost_ShippingMethodCostPremiumCustomerExpressShippingMethodTwoItems_IsExpressCostTimesTwo()
        {
            var address = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            var calculator = new ShippingCalculator(address);

            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Express,
                Items = new List<Item>
                {
                    new() {Quantity = 2}
                },
                ShippingAddress = new Address { City = "London", Country = "UK", Street = "1 Downtown Abby Rd" }
            };

            var expectedRate = 2 * ShippingCalculator.SameCityRate * shippingMethodMultiplier[cart.ShippingMethod];
            Assert.Equal(expectedRate, calculator.CalculateShippingCost(cart));
        }

        #endregion

        #endregion

    }
}
