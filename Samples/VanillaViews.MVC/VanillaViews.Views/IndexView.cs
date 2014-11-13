using System;
using System.Linq;
namespace VanillaViews.MVC.Views
{
    public class IndexViewVanilla
    {
        //
        // Viewmodel properties (of fields)
        //
        public string Header;
        public string Text;
        public string[] ListItems;

        //
        // Vanilla layout definition
        //
        public Func<string, string> Layout;

        //
        // private Helpers / Components - this is one vanilla way of mixing values and html tag structure 
        //
        string header(string header) { return string.Format("<h1>{0}</h1>", header); }
        string text(string text) { return string.Format("<p>{0}</p>", text); }
        string li(string text) { return string.Format("<li>{0}</li>", text); }
        string listItems(string[] texts) { return (texts != null && texts.Length > 0) ? string.Format("<ul>{0}</ul>", string.Concat(texts.Select(t => li(t)))) : ""; }

        //
        // Simply override ToString to render the actual output
        // we don't want string tags here - we want to express the 
        // intent of our code - that is to print out a header
        // and a text.
        // 
        public override string ToString()
        {
            return Layout(

                string.Concat(

                    /* here we use the components which are named with their usage / intent in mind
                     * which means we can easily change the necessary tag structure with intent kept intact */

                    header(Header),

                    text(Text),                    

                    

                    /* the listItems takes an array of strings as input, no risk of sending it incompatible data */

                    listItems(ListItems)

                ));
        }
    }
}