using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit;

namespace WritUnitTests
{

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void SanityCheck()
        {
            var thing = "test";
            NUnit.Framework.Assert.AreEqual("test", thing);
        }
    }
}
