using NUnit.Framework;

namespace Candy.Net.Tests.Utilities
{
    internal abstract class TestFixtureBase
    {
        [SetUp]
        public virtual void Setup() { }

        [TearDown]
        public virtual void Teardown() { }
    }
}