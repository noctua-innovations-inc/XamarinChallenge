using CodeX.Core.Extensions;
using CodeX.Core.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace CodeX.Core.Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private bool ValidProperty<T>(object entity, Expression<Func<T>> expression, out IList<ValidationResult> results)
        {
            var name = ((MemberExpression)expression.Body).Member.Name;
            var value = expression.Compile()();

            var validationContext = new ValidationContext(entity, null, null)
            {
                MemberName = name
            };

            results = new List<ValidationResult>();
            return Validator.TryValidateProperty(value, validationContext, results);
        }

        [Test]
        public void Validation_SpecialCharacter_FailsDueToSpecialCharacterPresence()
        {
            // Arrange
            var entity = new CzAccount()
            {
                NameFamily = "John!",
            };

            // Act
            var valid = ValidProperty(entity, () => entity.NameFamily, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "We have a special character in the last (family) name");
        }

        [Test]
        public void Validation_SpecialCharacter_Succeeds()
        {
            // Arrange
            var entity = new CzAccount()
            {
                NameFamily = "John",
            };

            // Act
            var valid = ValidProperty(entity, () => entity.NameFamily, out var results);

            // Assert
            valid
                .Should()
                .BeTrue(because: "We don't have any special characters in the last (family) name");
        }

        [Test]
        public void Validation_PhoneNumber_Fails()
        {
            // Arrange
            var entity = new CzAccount()
            {
                PhoneMain = "(416) 555-228",
            };

            // Act
            var valid = ValidProperty(entity, () => entity.PhoneMain, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "We don't have a properly formatted phone number");
        }

        [Test]
        public void Validation_PhoneNumber_ValidPhoneNumbersWithVariousFormatting()
        {
            CzTransformer
                .FormatPhoneNumber("4165552323")
                .Should()
                .Match("(416) 555-2323");

            CzTransformer
                .FormatPhoneNumber("(416)-555-2323")
                .Should()
                .Match("(416) 555-2323");

            CzTransformer
                .FormatPhoneNumber("416.555.2323")
                .Should()
                .Match("(416) 555-2323");

            CzTransformer
                .FormatPhoneNumber("(416) 555-2323")
                .Should()
                .Match("(416) 555-2323");
        }

        [Test]
        public void Validation_PhoneNumber_InvalidPhoneNumbersWithVariousFormatting()
        {
            string phoneNumber;

            phoneNumber = "41655523";
            CzTransformer
                .FormatPhoneNumber(phoneNumber)
                .Should()
                .Match(phoneNumber);

            phoneNumber = "41655523233";
            CzTransformer
                .FormatPhoneNumber(phoneNumber)
                .Should()
                .Match(phoneNumber);

            phoneNumber = "(416) 555-2323 Ext. 33";
            CzTransformer
                .FormatPhoneNumber(phoneNumber)
                .Should()
                .Match(phoneNumber);

            phoneNumber = "416.555.2323 x 33";
            CzTransformer
                .FormatPhoneNumber(phoneNumber)
                .Should()
                .Match(phoneNumber);
        }

        [Test]
        public void Validation_PhoneNumber_NullHandling()
        {
            CzTransformer
                .FormatPhoneNumber(null)
                .Should()
                .BeNull();

            CzTransformer
                .FormatPhoneNumber(null, 0, null)
                .Should()
                .BeNull();

            CzTransformer
                .FormatPhoneNumber(null, 0, null, null)
                .Should()
                .BeNull();
        }

        [Test]
        public void Validation_PhoneNumber_Succeeds()
        {
            // Arrange
            var entity = new CzAccount()
            {
                PhoneMain = "4165552288",
            };

            // Act
            var valid = ValidProperty(entity, () => entity.PhoneMain, out var results);

            // Assert
            valid
                .Should()
                .BeTrue(because: "We have a properly formatted phone number");
        }

        [Test]
        public void Validation_Password_LengthTooShort()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "2Short"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Password too short");
        }

        [Test]
        public void Validation_Password_LengthTooLong()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "TooLongForFifteenCharacters"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Password too long");
        }

        [Test]
        public void Validation_Password_MissingUppercase()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "abcdefg123"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Password missing uppercase letter");
        }

        [Test]
        public void Validation_Password_MissingLowercase()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "ABCDEFG123"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Password missing lowercase letter");
        }

        [Test]
        public void Validation_Password_IsInvalidDueToRepetitionViaAttribute()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "AbcdAbcdAbcd"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Password has repetition");
        }

        [Test]
        public void Validation_Password_IsValid()
        {
            // Arrange
            var entity = new CzAccount()
            {
                Password = "AbcdeFghij"
            };

            // Act
            var valid = ValidProperty(entity, () => entity.Password, out var results);

            // Assert
            valid
                .Should()
                .BeTrue(because: "Password passes business requirements");
        }

        [Test]
        public void Validation_Password_IsInvalidDueToRepetition()
        {
            // Act & Assert
            PasswordValidationAttribute
                .HasRepetitiveSequence("abcabc")
                .Should()
                .BeTrue(because: "Per business example");

            PasswordValidationAttribute
                .HasRepetitiveSequence("111")
                .Should()
                .BeTrue(because: "Per business example");

            PasswordValidationAttribute
                .HasRepetitiveSequence("12ab12ab")
                .Should()
                .BeTrue(because: "Per business example");

            PasswordValidationAttribute
                .HasRepetitiveSequence("123123123Aa")
                .Should()
                .BeTrue(because: "There is a repeated sequence");

            PasswordValidationAttribute
                .HasRepetitiveSequence("anythings11")
                .Should()
                .BeTrue(because: "There is a repeated sequence");
        }

        [Test]
        public void Validation_Password_InvalidRepetition()
        {
            // Arrange, Act & Assert
            PasswordValidationAttribute
                .HasRepetitiveSequence("abcABC")
                .Should()
                .BeFalse(because: "The repetition differs by case-sensativity");
        }


        [Test]
        public void Validation_HasRepetitiveSequence_NullEmptyAndOneCharacterSequence()
        {
            PasswordValidationAttribute
                .HasRepetitiveSequence(null)
                .Should()
                .BeFalse(because: "Null cannot have repetition");

            PasswordValidationAttribute
                .HasRepetitiveSequence("")
                .Should()
                .BeFalse(because: "Empty string cannot have repetition");
            
            PasswordValidationAttribute
                .HasRepetitiveSequence("a")
                .Should()
                .BeFalse(because: "A single character sequence cannot have repetition");
        }

        [Test]
        public void Validation_ServiceDate_InvalidDueToPastDate()
        {
            // Arrange
            var entity = new CzAccount()
            {
                ServiceDateStart = DateTime.Now.Date.Subtract(TimeSpan.FromDays(2))
            };

            // Act
            var valid = ValidProperty(entity, () => entity.ServiceDateStart, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Past Service Start Date is not allowed");
        }

        [Test]
        public void Validation_ServiceDate_InvalidDueToFutureDate()
        {
            // Arrange
            var entity = new CzAccount()
            {
                ServiceDateStart = DateTime.Now.Date.AddDays(31)
            };

            // Act
            var valid = ValidProperty(entity, () => entity.ServiceDateStart, out var results);

            // Assert
            valid
                .Should()
                .BeFalse(because: "Service Start Date more than 30 days into the future is not allowed");
        }

        [Test]
        public void Validation_ServiceDate_IsInvalid()
        {
            // Arrange
            var entity = new CzAccount()
            {
                ServiceDateStart = DateTime.Now
            };

            // Act
            var valid = ValidProperty(entity, () => entity.ServiceDateStart, out var results);

            // Assert
            valid
                .Should()
                .BeTrue(because: "Service Start Date is same as creation date");
        }
    }
}