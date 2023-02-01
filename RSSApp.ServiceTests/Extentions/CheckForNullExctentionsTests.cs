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
    public class CheckForNullExctentionsTests
    {
        [TestMethod()]
        public void CheckForNull_EmptyString_ArgumentNullEcxeption()
        {
            // arrange
            
            string text = "";

            // act
            // assert

            Assert.ThrowsException<ArgumentNullException>(() => text.CheckForNull());
        }

        [TestMethod()]
        public void CheckForNull_ObjectNull_ArgumentNullEcxeption()
        {
            // arrange

            object obj = null;

            // act
            // assert

            Assert.ThrowsException<ArgumentNullException>(() => obj.CheckForNull());
        }
    }
}