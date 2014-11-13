using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VanillaViews.Bootstrap.Tests
{
    [TestClass]
    public class JustifiedNavTests
    {

        [TestMethod]
        public void JustifiedNavTest()
        {
            // First we need the HTML prototype - in this case from an URL (wooo)
            // (could be from a local file path aswell, for those not as daring...)

            // I'm using the CsQuery library for this which 
            var expected = CsQuery.CQ.CreateFromUrl(@"http://getbootstrap.com/examples/justified-nav/")["div [role='navigation']"].RenderSelection();

            // Then create the dynamic part 

            var view = new JustifiedNavView(
                new JustifiedNavView.Link { IsActive = true, Url = "#", Text = "Home" },
                new JustifiedNavView.Link { Url = "#", Text = "Projects" },
                new JustifiedNavView.Link { Url = "#", Text = "Services" },
                new JustifiedNavView.Link { Url = "#", Text = "Downloads" },
                new JustifiedNavView.Link { Url = "#", Text = "About" },
                new JustifiedNavView.Link { Url = "#", Text = "Contact" }
            );

            // Next create a code stub and call it
            
            var actual = view.ToString();
            
            // Now .. run (or keep running) until you written the code, are happy with it and test pass

            // We run the HTML through a reindenter to compare the tags and not the indentation
            Assert.AreEqual(PrettyXml.Reindent(expected), PrettyXml.Reindent(actual));


        }
    }
}
