using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Candy.Core;
using Candy.Net.Tests.Utilities;
using NUnit.Framework;

namespace Candy.Net.Tests.Core
{
    [TestFixture]
    internal class OptionTestFixture : TestFixtureBase
    {
        [Test(Description = "Option should be able to contain value")]
        public void Option_Should_Be_Able_To_Contain_Value()
        {
            // Given
            Option<Int32> underTest = Option.Some(10);

            // When
            var result = new {
                HasValue = underTest.HasValue,
                Value = underTest.Value
            };

            // Then
            Assert.That(result, Is.EqualTo(new {
                HasValue = true,
                Value = 10
            }));
        }

        [Test(Description = "Option should be able to contain None values")]
        public void Option_Should_Be_Able_To_Contain_None_Values()
        {
            // Given
            Option<String> underTest = Option.None;

            // When
            var result = underTest;

            // Then
            Assert.That(result.HasValue, Is.False);
            Assert.That(underTest == Option.None, Is.True);
        }

        [Test(Description = "Option should throw Exception if it has no value")]
        public void Option_Throws_Exception_If_It_Has_No_Value()
        {
            // Given
            Option<Int32> underTest = Option.None;

            // When
            TestDelegate result = () => {
                var value = underTest.Value;
            };

            // Then
            Assert.That(result, Throws.InstanceOf<InvalidOperationException>());
        }

        [Test(Description = "Option should be able to compare their values")]
        public void Option_Should_Be_Able_To_Compare_Their_Values()
        {
            // Given
            var underTest = Option.Some(Option.Some("Hello World!"));

            // When
            var result = new {
                IsEqual = underTest == Option.Some(Option.Some("Hello World!")),
                IsNotEqual = underTest != Option.Some(Option.Some("Hello World!"))
            };

            // Then
            Assert.That(result.IsEqual, Is.True);
            Assert.That(result.IsNotEqual, Is.False);
        }

        [Test(Description = "Option should be able to transform its value")]
        public void Option_Should_Be_Able_To_Transfrom_Its_Value()
        {
            // Given
            var underTest = Option.Some(15)
                                  .Map(value => value + 5)
                                  .Map(value => 2 * value);

            // When
            var result = underTest.Value;

            // Then
            Assert.That(result, Is.EqualTo(40));
        }

        [Test(Description = "Option.Equals should return true, because their value are equal")]
        [TestCase(13, 13)]
        [TestCase("Hi", "Hi")]
        public void Option_Equals_Should_Return_True_Because_Their_Value_Are_Equal(Object a, Object b)
        {
            // Given
            Assume.That(a, Is.EqualTo(b));

            var underTest = Option.Some(a);

            // When
            var result = underTest.Equals(Option.Some(b));

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Option.Equals should return false because given parameters are different")]
        [TestCase(1, 2)]
        [TestCase("a", "b")]
        public void Option_Equals_Should_Return_False_Because_Given_Parameters_Are_Different(Object a, Object b)
        {
            // Given
            Assume.That(a, Is.Not.EqualTo(b));
            var underTest = Option.Some(a);

            // When
            var result = underTest.Equals(b);

            // Then
            Assert.That(result, Is.False);
        }

        [Test(Description = "Option.Equals should return true, because Option.None is equal to Option.None")]
        public void Option_Equals_Should_Return_True_Because_None_Is_Equal_To_None()
        {
            // Given
            var underTest = Option.None;

            // When
            var result = underTest.Equals(Option.None);

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Operator == of Option should return true, because Option.None is equal to Option.None")]
        public void Option_Equals_Operator_Should_Return_True_Because_None_Is_Equal_To_None()
        {
            // Given
            var underTest = Option.None;

            // When
            var result = underTest == Option.None;

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Option should be able to be filtered by value")]
        public void Option_Should_Be_Able_To_Be_Filtered_By_Value()
        {
            // Given
            var underTest = Option.Some(15);

            // When
            var result = new {
                PositiveValue = underTest.Filter(value => value > 0),
                NegativeValue = underTest.Filter(value => value < 0)
            };

            // Then
            Assert.That(result.PositiveValue == Option.Some(15), Is.True);
            Assert.That(result.NegativeValue == Option.None, Is.True);
        }
    }
}
