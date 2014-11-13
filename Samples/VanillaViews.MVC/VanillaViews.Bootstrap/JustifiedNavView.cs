using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VanillaViews.Bootstrap
{

    /// <summary>
    /// see http://getbootstrap.com/examples/justified-nav/
    /// </summary>
    public class JustifiedNavView
    {
        public class Link
        {
            public string Url;
            public string Text;
            public bool IsActive;
            public Link() { }
            public Link(string url, string text, bool isActive = false)
            {
                Url = url; Text = text; IsActive = isActive;
            }
        }
        public List<Link> List { get; set; }

        public string li(Link link)
        {
            var li = "<li>";
            if (link.IsActive) li = @" <li class=""active"">";
            return string.Format(li + @"<a href=""{0}"">{1}</a></li>" + Environment.NewLine, link.Url, link.Text);
        }

        public JustifiedNavView(params Link[] list)
        {
            List = list.ToList();
        }

        public override string ToString()
        {
            return string.Concat(
                @"<div role=""navigation"">
          <ul class=""nav nav-justified"">",
            string.Concat(List.Select(s => li(s))),
            @"</ul>
        </div>");
        }
    }
}
