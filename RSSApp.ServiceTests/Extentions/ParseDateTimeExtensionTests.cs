using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSApp.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.Service.Extentions.Tests
{
    [TestClass()]
    public class ParseDateTimeExtensionTests
    {
        [TestMethod()]
        public void ParseToDateTime_IncorectData_InvalidDataException()
        {
            // arrange

            string dateTime = "fsdfsdfdfs";

            // act
            // assert

            Assert.ThrowsException<InvalidDataException>(() => dateTime.ParseToDateTime());
        }

        [TestMethod()]
        public void ParseToDateTime_DateTimeString_DateTime()
        {
            // arrange

            string dateTimeString = DateTime.MaxValue.ToString();

            // act

            DateTime dateTime = dateTimeString.ParseToDateTime();

            // assert

            Assert.AreEqual(DateTime.MaxValue.ToShortDateString(), dateTime.ToShortDateString());
        }
    }
}