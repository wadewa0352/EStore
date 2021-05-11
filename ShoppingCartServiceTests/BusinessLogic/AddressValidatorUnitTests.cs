using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShoppingCartServiceTests.BusinessLogic
{
    public class AddressValidatorUnitTests
    {

        public static List<object[]> TestData()
        {
            return new List<object[]>
            {
                new object[] { new Address { Street = "1 Main St", City = "Anywhere", Country = "USA" }, true },
                new object[] { null, false },
                new object[] { new Address(), false },
                new object[] { new Address { City = "Anywhere", Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", City = "Anywhere" }, false },
                new object[] { new Address { Street = String.Empty, City = String.Empty, Country = String.Empty }, false },
                new object[] { new Address { Street = String.Empty, City = "Anywhere", Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", City = String.Empty, Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", City = "Anywhere", Country = String.Empty }, false },
                new object[] { new Address { Street = "", City = "", Country = "" }, false },
                new object[] { new Address { Street = "", City = "Anywhere", Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", City = "", Country = "USA" }, false },
                new object[] { new Address { Street = "1 Main St", City = "Anywhere", Country = "" }, false },
            };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void IsValid_ValidateAddresses(Address address, Boolean expectedResult)
        {
            var validator = new AddressValidator();

            Assert.Equal(expectedResult, validator.IsValid(address));

        }

    }
}
