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
            // create the data

            var view = new JustifiedNav(
                new JustifiedNav.Link{ IsActive=true,Url="#",Text="Home" },
                new JustifiedNav.Link{ Url="#",Text="Projects" },
                new JustifiedNav.Link{ Url="#",Text="Services" },
                new JustifiedNav.Link{ Url="#",Text="Downloads" },
                new JustifiedNav.Link{ Url="#",Text="About" },
                new JustifiedNav.Link{ Url="#",Text="Contact" }
            );

            // render view and reindent it
            
            var actual = PrettyXml.Reindent(view.ToString());

            // get sample from bootstrap site - using CsQuery - and reindent it

            // alternatively use locally saved file

            var expected = PrettyXml.Reindent(CsQuery.CQ.CreateFromUrl(@"http://getbootstrap.com/examples/justified-nav/")["div [role='navigation']"].RenderSelection());

            // now create the actual view ... until tests pass

            Assert.AreEqual(expected, actual);


        }
    }
}
