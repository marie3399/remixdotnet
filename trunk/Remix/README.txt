/*    Remix.NET
 *
 * Remix.NET is licensed under the MIT license:
 * http://www.opensource.org/licenses/mit-license.html
 *
 * Copyright (c) 2009 Omar Abdelwahed
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
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
