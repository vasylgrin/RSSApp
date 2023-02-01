using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RSSApp.Service.Helpers.Tests
{
    [TestClass()]
    public class XmlHelperTests
    {    
        [TestMethod()]
        public void GetXElementByLink_Link_XElement()
        {
            // arrange

            const string Link = @"https://www.radiosvoboda.org/api/zrqiteuuir";

            // act

            var xElm = XmlHelper.GetXElementByLink(Link);

            // assert

            Assert.IsNotNull(xElm);
        }
    }
}