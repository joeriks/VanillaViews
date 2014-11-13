Vanilla C# views
================

**tl;dr; if you are good at C#, and like things like DRY, refactoring and TDD. And if you usually get ready made HTML to work with.
Then I think you will be more productive with pure C# (creating HTML strings) than with Razor.**

**"So why use Razor at all? Content should come from content mgnt. And HTML structures should be components, where C# shines"**

Mixing languages is always a source of possible friction, Microsoft did a great work creating the Razor View Engine. But
I think it sometimes add more problems than it solves. An initial assumption is that we need to keep HTML files separate from
our C# code. Because (at least I think this is the reason) we like HTML-designers to handle the HTML files in a project. However -
with Razor our designers need to understand both Razor syntax, and how the data is structured in our application. From my experience
the designers are most happy when they can focus 100% on the HTML part, creating pure HTML prototypes.

**Designer: "Gaah, what is this cshtml mess? Here - take my prototypes, and you do the rest!"**

From ready made, nicely designed HTML prototypes, I find it to be a rather small part of the work to re-create the HTML dynamically
from C# functions. And when I get another HTML page to add to the project, or some existing page gets redesigned, it's really not much 
work to redo it either.

Also with vanilla C#-to HTML code I can easily create tests which I run until I'm done with the actual code. Also, I keep the tests and 
can refactor my C# how much I want.

**Boss: "I said change it _everywhere_!"**

I'm usually responsible for the application, and to extend and reuse the design components in a maintainable way. When I use Razor I 
often find myself using more complicated, less testable and less DRY code compared to when I do it with pure vanilla C#.

Changing a piece of HTML structure in several places in the application is usual as part of maintenance, but with razor it usually 
involves error prone text search-and-replacing and quite a bit of manual testing.

*Also - client side frameworks. With vanilla C# nothing stops me from adding model references and strongly typed directives (I simply build 
classes or functions for them).*

**Partial Views**

Let's say we'd like to use a partial view for a small thing like a panel displaying some information from different pages:

	@model ViewModels.PanelInformation

	<div class="panel">
		<div class="panel-head">
			<h2>@Model.Header</h2>
		</div>
		<div class="panel-body">
			@Model.Body
		</div>
	</div>

Besides the view file, we also need the following pieces

* a PanelInformation ViewModel with the necessary properties
* a PanelInformation action in the controller, returning a PartialView - with the same name as the view (or naming it explicitly)
* render the action result in our main view @Html.Action("Home","PanelInformation")

*Contrasting the normal C# way of how we work with code we've got three pitfalls here since we use file names (action name == view file 
name) and strings to connect our pieces. That means IDE help gets lost and refactoring gets a lot more complicated than with regular C#.*

If we like to rename the panel to something else we need to change it in three different places, and we get no help showing if we 
misspelled or missed to rename the view, or if we missed in the main view. Imagine if we called it from multiple of views (which is
after all one of the reasons to use a partial view in the first place).

Yes, we could use 3rd party tools to get some help here. But really - is that what we like for a basic thing like this?

*And again - why not use old friend C#?*

**Things we might miss**

We do get a warm and fuzzy feeling having the HTML separated into files in the Views folder, and we can say - if something needs to 
be changed in the HTML - look in the views folder. (But be sure to not mess up anything, test it visually, and repeat the changes in 
all recessary files if the HTML structure is not fully DRYed, for example add a **div class="something"** around all lists of some kind).

**Enterprise coder: "Hey, I've got 100 forms, I need my ModelFor"**

The Razor extension methods are nice sometimes. (I don't use those much, but I guess if you do you'd need to find a replacement.)

**Things I get in "vanilla views" compared to in Razor:**

- Real static typed views with IDE help like
	- refactor
	- correct types	
	- navigate to
	- find usages
	- tooltip
- I can reuse helpers the way I'm used to in C#
- I can test the views completely outside of web context
- I can use my views for things like Emails or HTML-based reports

**One way to work with vanilla views**

1. Create (or get from someone who actually knows HTML better...) pure HTML prototype (s)
2. Identify repeated and dynamic parts
3. Build tests that renders views and compares them to the HTML prototypes (as files or URLs)
4. Create Views and Subviews/components the way you always work in C# (classes / inheritance / DI / whatever you need)
5. Make tests pass
6. Repeat 4-5 to refactor as much as you like

**Ok, so what *is* a vanilla view?**

**It's simply a C# function that returns a string!** Using the standard string handling, linq, class structure and whatever you find useful. 
For example in the way I show below.

**A note about DRY and extensibility**

In my sample view I use 100% vanilla code intentionally with all code explicitly written in the view. Naturally it would be easy to 
add some helper functions, base classes or extension methods to get leaner code. The purpose here is to show a starting point for 
such work and that HTML can be done easily with what we have in C#.

However - one of the goals with this is to have as few "surprises" as possible in the code. For readability, maintainability and 
portability reasons. (I.e. if you add too much abstraction you might end up with views that are harder to maintain in the end.)

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