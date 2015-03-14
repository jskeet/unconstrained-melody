There are various useful things that can be done with generic methods/classes where there's a type constraint of "T : enum" or "T : delegate" - but unfortunately, those are prohibited in C#.

This utility library works around the prohibitions using ildasm/ilasm, by building the code with "fake" constraints and then rewriting the constraints afterwards. The resulting binary is usable from "normal" C#; the C# compiler **understands** the constraints just fine, even if it won't let you express them.

This is a tiny little pet project, but you may still find it useful. There's a [NuGet package](http://nuget.org/List/Packages/UnconstrainedMelody) containing just the assembly and XML documentation file, but if you want to use the "constraint changer" you'll need to download and build the source from this site.

A [blog post from 2009](http://codeblog.jonskeet.uk/2009/09/10/generic-constraints-for-enums-and-delegates) goes into a little more detail, and hopefully I'll get some wiki pages with sample code up at some point...