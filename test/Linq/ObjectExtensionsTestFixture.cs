using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Linq;
using Candy.Net.Tests.Mocks;
using NUnit.Framework;

namespace Candy.Net.Tests.Linq
{
    [TestFixture]
    internal class ObjectExtensionsTestFixture
    {
        [Test(Description = "ObjectExtensions.Safe method should return object itself if it is not null")]
        public void Safe_Should_Return_Object_Itself_If_It_Is_Not_Null()
        {
            // Given
            var underTest = new Person();

            // When
            var result = underTest.Safe();

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test(Description = "ObjectExtensions.Safe method should return a new object if it is null")]
        public void Safe_Should_Return_A_New_Object_If_It_Is_Null()
        {
            // Given
            Person underTest = null;

            // When
            var result = underTest.Safe();
            result.Name = "John Doe";

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John Doe"));
        }

        [Test(Description = "ObjectExtensions.With method should return expression itself")]
        public void With_Should_Return_Expression_Itself()
        {
            // Given
            var underTest = new Person();

            // When
            var result = underTest.With(person =>
            {
                person.Name = "John Doe";
                person.Age = 24;
            });

            // Then
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Age, Is.EqualTo(24));
        }


        [Test(Description = "ObjectExtesions.Init method should instantiate and initialize object if it is null")]
        public void Init_Should_Instantiate_And_Initialize_Object_If_It_Is_Null()
        {
            // Given
            Person underTest = null;

            // When
            var result = underTest.Init(person => {
                person.Name = "John Doe";
                person.Age = 24;
            });

            // Then
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Age, Is.EqualTo(24));
        }

    }
}
