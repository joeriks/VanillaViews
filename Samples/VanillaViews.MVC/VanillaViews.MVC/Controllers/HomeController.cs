using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanillaViews.MVC.Views;

namespace VanillaViews.MVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // here's our controller action
        // it's simply returning a string
        // since html is ... a string ;)
        //
        // we use a combined view + viewmodel because they should be 
        // tightly coupled
        //
        public string Index()
        {
            //
            // get some data from wherever
            //
            var someData = new { Header = "foo", Text = "bar" };

            //
            // instantiate the view
            // 
            var view = new IndexViewVanilla
            {
                Header = someData.Header,
                Text = someData.Text
            };

            //
            // make the view use a layout - with a simple func statement
            //
            view.Layout = new Func<string, string>(s => new LayoutView(s).ToString());

            //
            // "render" the view
            //
            return view.ToString();
        }
    }
}