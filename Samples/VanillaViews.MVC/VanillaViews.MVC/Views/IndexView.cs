using System;
namespace VanillaViews.MVC.Views
{    
    public class IndexViewVanilla
    {
        //
        // Viewmodel properties (of fields)
        //
        public string Header;
        public string Text;
        
        //
        // Vanilla layout definition
        //
        public Func<string, string> Layout;

        //
        // Helpers - this is the vanilla way we mix values and html        
        //
        public string header(string header){ return string.Format("<h1>{0}</h1>",header);}
        public string text(string text) { return string.Format("<p>{0}</p>", text); }

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

                    header(Header),
                    text(Text)

                ));
        }
    }
}