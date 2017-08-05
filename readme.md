# This isn't the library you're looking for

Neat as Unconstrained Melody was, it was very much hacked together.
The enums part of it was always more compelling to me than the delegate part, and
Tyler Brinkley has done a better job of implementing that in his [Enums.NET](https://github.com/TylerBrinkley/Enums.NET)
project. Importantly, the [NuGet package for Enums.NET](https://www.nuget.org/packages/Enums.NET/) supports
.NET Core by targeting `netstandard1.0`. I looked into doing the same thing for Unconstrained Melody
(as did Nick Craver - thanks!) but it's too much work for too little benefit compared with rallying
round Enums.NET and its accompanying [corefx proposal](https://github.com/dotnet/corefx/issues/15453).

I don't expect to publish any more releases of Unconstrained Melody.

----

Full documentation may come later, if there's enough demand.

For the moment, read this blog post:
http://codeblog.jonskeet.uk/2009/09/10/generic-constraints-for-enums-and-delegates/

Then if you just want the library, install the NuGet package:
http://nuget.org/List/Packages/UnconstrainedMelody

Or if you want the "Constraint Changer" to write your own generic members
in the same way, build the source code from the project home page:
https://github.com/jskeet/unconstrained-melody

Email me with any queries, questions, etc. (Or use the issue tracker to report bugs.)
Jon Skeet, skeet@pobox.com
