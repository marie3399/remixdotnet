/*    Remix.NET
##    Copyright 2008 Omar Abdelwahed
##
##    This file is part of Remix.NET.
##
##    Remix.NET is free software: you can redistribute it and/or modify
##    it under the terms of the GNU General Public License as published by
##    the Free Software Foundation, either version 3 of the License, or
##    (at your option) any later version.
##
##    Remix.NET is distributed in the hope that it will be useful,
##    but WITHOUT ANY WARRANTY; without even the implied warranty of
##    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
##    GNU General Public License for more details.
##
##    You should have received a copy of the GNU General Public License
##    along with Shelftalkers.  If not, see <http://www.gnu.org/licenses/>.
*/

Welcome to Remix.NET!

Remix.NET is a C# library that wraps API calls for the Best Buy Remix API.
Remix API documentation and usage can be found here:
http://remix.bestbuy.com/

The library is very simple and utilizes .NET XML serialization to return
products and store information from the Remix REST API. There are a few 
method calls that return very specific data filtered by both passed 
attributes and hardcoded attributes that were part of an original client 
I wrote. Use these methods as examples of how to extend the library for 
your own specific needs.

How To Use This Program:

1. If you are new to the Best Buy Remix API, follow the "Getting started
is easy" guide and register for a Best Buy Remix API key:
    http://remix.bestbuy.com/

2. Open the Remix.NET project in Visual Studio. Open the "Server.cs" code
file. Copy/paste your Remix API key into the "APIKEY" static variable at
the top of the page.

3. Build. You now have a .NET library for all your Remix applications!

Example Usage:

Using Remix;

Product p = null;
try
{
   Server remix = new Server("username", "password");
   String filter = "iphone";
   String postalcode = "94102";
   String radius = "25";
   bool tersemode = true;
   int pagenum = 0;

   // Get all Hardgoods that match "iphone" in the name attribute and
   // are in stores in the 94102 area code in a 25 mile radius.
   // When "tersemode" is true, only return a handful of information. 
   // (See method signature for details.)
   Products list = remix.GetHardGoods(filter, tersemode, pagenum, postalcode, radius);

   if (list.Count > 0) p = list[0];
}
catch (Exception e)
{
}
return p;

Contact the Author:
    
twitter: appleweed
facebook: Omar Abdelwahed
web: http://www.agentdisco.com
