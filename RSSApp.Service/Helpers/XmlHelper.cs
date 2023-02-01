using RSSApp.Service.Extentions;
using RSSApp.Service.Services;
using System.Xml.Linq;

namespace RSSApp.Service.Helpers
{
    public static class XmlHelper
    {
        public static XElement GetXElementByLink(string link)
        {
            var xDoc = GetXDocByURL(link);
            return xDoc.Element("rss").Element("channel");
        }
        private static XDocument GetXDocByURL(string url)
        {
            url.CheckForNull();

            
            return GetXmlResponseByURL(url);          
        }
        private static XDocument GetXmlResponseByURL(string url)
        {
            var xmlString = ReciveRequsetHelper.ReciveToRequest(url);

            var xmlDoc = XDocument.Parse(xmlString);

            xmlDoc.CheckForNull();

            return xmlDoc;
        }
    }
}
