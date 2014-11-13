using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VanillaViews.Views.Tests
{
    [TestClass]
    public class IndexViewTest
    {
        [TestMethod]
        public void IndexViewTest()
        {
            var view = new VanillaViews.MVC.Views.IndexViewVanilla{
                Layout = new Func<string, string>(s=>"<html>" + s + "</html>"),
                Header = "Header",
                Text = "Text"
            };

            var expected = "<html><h1>Header</h1><p>Text</p></html>";

            var result = view.ToString();
            
            Assert.AreEqual(expected, result);


        }
    }
}
