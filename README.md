Vanilla C# views
================

**tl;dr; if you are good at C#, and like things like DRY, refactoring and TDD. And if you usually get ready made HTML to work with.
Then I think you will be more productive with pure C# (creating HTML strings) than with Razor.**

Mixing languages is always a source of possible friction, Microsoft did a great work creating the Razor View Engine. But
I think it sometimes add more problems than it solves. An initial assumption is that we need to keep HTML files separate from
our C# code. Because (at least I think this is the reason) we like HTML-designers to handle the HTML files in a project. However -
with Razor our designers need to understand both Razor syntax, and how the data is structured in our application. From my experience
the designers are most happy when they can focus 100% on the HTML part, creating pure HTML prototypes.

From ready made, nicely designed HTML prototypes, I find it to be a rather small part of the work to re-create the HTML dynamically
from C# functions. And when I get another HTML page to add to the project, or some existing page gets redesigned, it's really not much 
work to redo it either.

Also with vanilla C#-to HTML code I can easily create tests which I run until I'm done with the actual code. Also, I keep the tests and 
can refactor my C# how much I want.

I'm usually responsible for the application, and to extend and reuse the design components in a maintainable way. When I use Razor I 
often find myself using more complicated, less testable and less DRY code compared to when I do it with pure vanilla C#.

**Things I get in "vanilla views" compared to in Razor:**

- Real static typed views with IDE help like
	- correct types
	- navigate to
	- find usages
	- helptext
- I can reuse helpers the way I'm used to in C#
- I can test the views completely outside of web context
- I can use my views for things like Emails or HTML-based reports

**One way to work with vanilla views**

1) Create (or get from someone who actually knows HTML better...) pure HTML prototype (s)

2) Identify repeated and dynamic parts

3) Build tests that renders views and compares them to the HTML prototypes (as files or URLs)

4) Create Views and Subviews/components the way you always work in C# (classes / inheritance / DI / whatever you need)

5) Make tests pass

6) Repeat 4-5 to refactor as much as you like

**Ok, so what *is* a vanilla view?**

It's simply a C# function that returns a string! Using the standard string handling, linq, class structure and whatever you find useful. 
For example in the way I show below.

**A note about DRY and extensibility**

In my sample view I use 100% vanilla code intentionally. Naturally it would be easy to add some helper functions, base
classes or extension methods to get leaner code. The purpose here is to show a starting point for such work and that HTML
can be done easily with what we have in C#.

**Tests and HTML prototypes**
Keep the HTML prototypes in a folder in your test project

	/tests/prototypes/index.html

Then compare your views with the contents of those files. Use HtmlAgilityPack or CsQuery to be able to compare the HTML - or parts
of the HTML easily.

**Performance and compilation**
Vanilla views are as fast as regular code. One feature with Razor is that you can edit it during runtime, since it can recompile 
dynamically if you like that feature vanilla views might not be for you (at least not without Roslyn).

**Sample view:**

    public class IndexViewVanilla
    {
        //
        // Exposed Viewmodel properties (could be fields, and could be set from a constructor)
        //
        public string Header {get;set;}
        public string[] ListItems {get;set;}

        //
        // Layout definition as a standard func (could be placed in a base class or interface)
        //
        public Func<string, string> Layout;

        //
        // private Helpers / Components (could be done in numerous different ways)
        //
        string header(string header) { return string.Format("<h1>{0}</h1>", header); }
        string li(string text) { return string.Format("<li>{0}</li>", text); }
        string listItems(string[] texts) { return (texts != null && texts.Length > 0) ? string.Format("<ul>{0}</ul>", string.Concat(texts.Select(t => li(t)))) : ""; }

		// 
		// naturally global helpers can be used to make our views as DRY as needed
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
                        * which means we can easily for example change the necessary tag structure with intent 
						* kept intact */

                    header(Header),

                    /* the listItems takes an array of strings as input, no risk of sending it 
					 * incompatible data */

                    listItems(ListItems)

                ));
        }
    }

**Usage from a controller:**

	public string Index() {	
		var view = new IndexViewVanilla{Header = "This is a header"}; 
		view.Layout = new Func<string, string>(s => new LayoutView(s).ToString());
		return view.ToString();
	}

**Installation:**

Nuget:

	intentionally left blank ;)