using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Core;
using NUnit.Framework;

namespace Candy.Net.Tests.Core
{
    [TestFixture]
    internal class TryTestFixture
    {
        [Test(Description = "Try.Success should contain actual value")]
        public void Success_Should_Contain_Actual_Value()
        {
            // Given
            var underTest = Try.Success(10);

            // When
            var result = (from x in underTest
                          select x + 1);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Success<Int32>>());
            Assert.That(result.Get(), Is.EqualTo(11));
        }

        [Test(Description = "Try.Failure should contain exception of result")]
        public void Failure_Should_Contain_Exception_Of_Result()
        {
            // Given
            var underTest = Try.Failure<Int32>(new NotImplementedException());

            // When
            var result = from x in underTest
                         select x + 1;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Failure<Int32>>());
            Assert.That(result.GetOrDefault(), Is.EqualTo(0));
        }
    }
}
