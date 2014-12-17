using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Linq;
using Candy.Net.Tests.Utilities;
using NUnit.Framework;

namespace Candy.Net.Tests.Linq
{
    [TestFixture]
    internal class NumberExtensionsTestFixture : TestFixtureBase
    {
        [Test]
        public void To_Method_Should_Return_Range_Of_Numbers()
        {
            // Given
            var underTest = 1.To(10);

            // When
            var result = underTest.ToArray();

            // Then
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }
    }
}
