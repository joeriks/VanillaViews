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

            // I'm using the CsQuery library for this which means I can get a piece of the HTML with
            // jquery style selectors:
            var expected = CsQuery.CQ.CreateFromUrl(@"http://getbootstrap.com/examples/justified-nav/")["div [role='navigation']"].RenderSelection();

            // Then create the class and instantiate it

            var view = new JustifiedNavView(
                new JustifiedNavView.Link { IsActive = true, Url = "#", Text = "Home" },
                new JustifiedNavView.Link { Url = "#", Text = "Projects" },
                new JustifiedNavView.Link { Url = "#", Text = "Services" },
                new JustifiedNavView.Link { Url = "#", Text = "Downloads" },
                new JustifiedNavView.Link { Url = "#", Text = "About" },
                new JustifiedNavView.Link { Url = "#", Text = "Contact" }
            );

            // In my case ToString renders the view
            
            var actual = view.ToString();
            
            // Now run (or keep running) the test until you finished with the code, 
            // are happy with it and the test pass

            // We pass the HTML through a reindenter to compare the tags and not the indentation
            Assert.AreEqual(PrettyXml.Reindent(expected), PrettyXml.Reindent(actual));


        }
    }
}
