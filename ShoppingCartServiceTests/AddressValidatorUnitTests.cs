using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
using System;
using Xunit;

namespace ShoppingCartServiceTests
{
    public class AddressValidatorUnitTests
    {
        [Fact]
        public void IsValid_AddressWithNoInvalidFields_IsTrue()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = "USA"
            };

            Assert.True(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_NullAddress_IsFalse()
        {
            var validator = new AddressValidator();

            Assert.False(validator.IsValid(null));
        }

        [Fact]
        public void IsValid_AddressWithAllNullFields_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address();

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithNullStreet_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                City = "Anywhere",
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithNullCity_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithNullCountry_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere"
            };

            Assert.False(validator.IsValid(validAddress));
        }

        [Fact]
        public void IsValid_AddressWithAllEmptyFields_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = String.Empty,
                City = String.Empty,
                Country = String.Empty
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithEmptyStreet_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = String.Empty,
                City = "Anywhere",
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithEmptyCity_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = String.Empty,
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithEmptyCountry_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = String.Empty
            };

            Assert.False(validator.IsValid(validAddress));
        }

        [Fact]
        public void IsValid_AddressWithAllBlankFields_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "",
                City = "",
                Country = ""
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithBlankStreet_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "",
                City = "Anywhere",
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithBlankCity_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = "",
                Country = "USA"
            };

            Assert.False(validator.IsValid(validAddress));

        }

        [Fact]
        public void IsValid_AddressWithBlankCountry_IsFalse()
        {
            var validator = new AddressValidator();
            var validAddress = new Address()
            {
                Street = "1 Main St",
                City = "Anywhere",
                Country = ""
            };

            Assert.False(validator.IsValid(validAddress));
        }
    }
}
