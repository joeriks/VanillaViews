VanillaViews
============

Vanilla C# #noViewEngine ;)

One thing I dont't like with Razor is how much we regard HTML as ontouchable from a C# point of view. 

We like it to be available to the designers - but yet we never let them do the hard work which is
creating dynamic parts of HTML where the tag structure and content is very bound to whatever HTML
tool / library we are using. Instead we usually let them do a HTML prototype, with no dynamic content
at all - and then we get to do the hard work. And we are responsible to extend it and reuse components
on different pages.

I consider Razor reusage of HTML a bunch of hacks compared to what we already have in C#.

*Things I dislike in Razor which I get easily in Vanilla Views:*

- Real static typed views. I know in the controller which view I'm and I get the intellisense help I'm used
to like 
	- navigate to
	- find usages
	- misspelled squiggles
	- helptext
- I can reuse helpers (which are same kind of vanilla views, or plain text functions) the way I'm used to
- I can test the views completely free from web context since they simply are classes with overridden ToString()
methods

*Simple view:*

	public class MySimpleView{
		public string Header {get;set;}
		public override string ToString(){
		return string.Concat(
			string.Format("<h1>{0}</h1>", Header)
		);
	}}

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