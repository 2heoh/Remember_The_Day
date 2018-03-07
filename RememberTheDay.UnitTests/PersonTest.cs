using System;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class PersonTest
    {
        [Test]
        public void WhenConvertingPersonObjectToString_ThanWeGetHisNameAndBirthDay()
        {
            var homer = new Person("Homer", "h.simpson@fox.com", new DateTime(1980, 1, 1));

            StringAssert.Contains("Homer 01.01.1980", homer.ToString());
        }
    }
}