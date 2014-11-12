VanillaViews
============

Vanilla C# #noViewEngine ;)

One thing I dont't like with Razor is how much we regard HTML as more important than our C# code. 

We like it to be available to the designers - but yet we never let them do the hard work which is
creating dynamic parts of HTML where the tag structure and content is very bound to whatever HTML
tool / library we are using. Instead we usually let them do a HTML prototype.

And then we are responsible to extend it and reuse components. With the tools given to us in Razor, 
which is really a bunch of hacks compared to what we already have in C#.

**Things I dislike in Razor which I get easily in Vanilla Views:**

- Real static typed views. I know in the controller which view I'm and I get the intellisense help I'm used
to like 
	- navigate to
	- find usages
	- misspelled squiggles
	- helptext
- I can reuse helpers (which are same kind of vanilla views, or plain text functions) the way I'm used to
- I can test the views completely free from web context since they simply are classes with overridden ToString()
methods

**One way or work that works good with this method**

1) Create (or get from someone who actually knows HTML better...) pure HTML prototype (s)
2) Identify repeated and dynamic parts
3) Build tests that renders views and compares them to the HTML prototypes (as files or URLs)
4) Create Views and Subviews/components the way you always work in C# (classes / inheritance / DI / whatever you need)
5) Make tests pass
6) Repeat 4-5 to refactor as much as you like

**Sample view:**

    public class IndexViewVanilla
    {
        //
        // Exposed Viewmodel properties (could be fields, and could be set from a constructor)
        //
        public string Header {get;set;}
        public string Text {get;set;}
        public string[] ListItems {get;set;}

        //
        // Layout definition as a standard func 
        //
        public Func<string, string> Layout;

        //
        // private Helpers / Components - could be done in numerous ways, here's one example
        //
        string header(string header) { return string.Format("<h1>{0}</h1>", header); }
        string text(string text) { return string.Format("<p>{0}</p>", text); }
        string li(string text) { return string.Format("<li>{0}</li>", text); }
        string listItems(string[] texts) { return (texts != null && texts.Length > 0) ? string.Format("<ul>{0}</ul>", string.Concat(texts.Select(t => li(t)))) : ""; }

		// 
		// naturally global helpers can be used to make our views as DRY as needed, the idea is 
		// use C# with its possibilities
		//

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

*Usage from a controller:*

	public string Index() {	
		var view = new MySimpleView{Header = "This is a header"};
		return view.ToString();
	}

Note the intentionally vanillaism. We could easily add some helper to for example get to use {header} inside the string, 
and we could do all kinds of small things to make the view look cleaner. But really they would not help that much, and if we are 
almost as good with this 100% vanilla way.

Installation:

Nuget:

	intentionally left blank ;)