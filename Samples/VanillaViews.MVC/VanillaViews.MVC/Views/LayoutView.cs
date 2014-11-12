using System;

namespace VanillaViews.MVC.Views
{
    //
    // a layoutview is also a simple class, but it need to have
    // a place to include the content, using a field and a constructor
    // is one way
    //
    public class LayoutView
    {
        private string content;
        
        //
        // this is the layout constructor
        //
        public LayoutView(string content) { this.content = content; }

        //
        // here we "render" the result
        //
        public override string ToString()
        {
            return string.Concat(

                @"
                <!DOCTYPE html>
                <html>",
                
                /* we can add comments inline here... */

                @"<head>
                    <meta charset='utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                </head>",

                /* here comes the body: */

                @"<body>
                ", content, @"

                </body>
                </html>

                ");
            
        }
    }
}