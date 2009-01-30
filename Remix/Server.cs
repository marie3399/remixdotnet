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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Web;

namespace Remix
{
    public class Server
    {
        // Version 1 Product interface. Change with new versions!
        private static string PRODUCT = "http://api.remix.bestbuy.com/v1/products";
        
        // Your Remix API key goes here.
        private static string APIKEY = "";
        
        private string user = "";
        private string pass = "";

        public Server(string u, string p)
        {
            this.user = u;
            this.pass = p;
        }

        public string Get(string url)
        {
            try
            {
                // Create the web request   
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(this.user + ":" + this.pass)));

                // HACK: Fixes "417 Expectation Failed".
                System.Net.ServicePointManager.Expect100Continue = false;

                // Get response   
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // check return codes
                //200 OK: everything went awesome.
                if (response.StatusCode == HttpStatusCode.OK)
                    return new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
                else
                    return null; // and log!
            }
            catch (WebException we)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Post(string url, string status)
        {
            try
            {
                // Create the web request   
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(this.user + ":" + this.pass)));

                // Set type to POST   
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                // Create the data we want to send   
                StringBuilder data = new StringBuilder();
                data.Append("status=" + HttpUtility.UrlEncode(status));

                // Create a byte array of the data we want to send   
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

                // Set the content length in the request headers   
                request.ContentLength = byteData.Length;

                // HACK: Fixes "417 Expectation Failed" error from Twitter.
                System.Net.ServicePointManager.Expect100Continue = false;
                
                // Write data   
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // Get response   
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // check return codes
                if (response.StatusCode == HttpStatusCode.OK)
                    return new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
                else
                    return null; // and log!
            }
            catch (WebException we)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Products GetProduct(string skuid, bool bTerse)
        {
            return this.GetProduct(skuid, bTerse, null, null);
        }

        public Products GetProduct(string skuid, bool bTerse, string postalcode, string radius)
        {
            string search = "sku='" + skuid + "'";
            string show = "";
            string area = "";
            
            if (bTerse) show = "&show=sku,productId,name,type,regularPrice,salePrice,url,inStoreAvailability,onlineAvailability,thumbnailImage";
            if (postalcode != null && radius != null)
            {
                area = "+stores(area(" + postalcode + "," + radius + "))";
                if (bTerse) show += ",stores";
            }
            
            Products p = UTF8XmlSerializer.Deserialize<Products>(Get(PRODUCT + "(" + search + ")" + area + "?apiKey=" + APIKEY + show));
            return p;
        }

        public Products GetGames(string args, bool bTerse)
        {
            return this.GetGames(args, bTerse, 0, null, null);
        }

        public Products GetGames(string args, bool bTerse, int ipage)
        {
            return this.GetGames(args, bTerse, ipage, null, null);
        }

        public Products GetGames(string args, bool bTerse, int ipage, string postalcode, string radius)
        {
            args = args.Replace(" ", "%20");
            string search = "type='Game'" 
                + "&name='" + args + "*'";
            string sort = "&sort=regularPrice.desc";
            string show = "";
            string spage = "";
            string area = "";
            if (bTerse) show = "&show=sku,productId,name,type,regularPrice,salePrice,url,inStoreAvailability,onlineAvailability,thumbnailImage";
            if (postalcode != null && radius != null)
            {
                area = "+stores(area(" + postalcode + "," + radius + "))";
                if (bTerse) show += ",stores";
            }
            if(ipage > 0) spage = "&page=" + ipage.ToString();
            
            Products list = UTF8XmlSerializer.Deserialize<Products>(Get(PRODUCT + "(" + search + ")" + area + "?apiKey=" + APIKEY + sort + show + spage));

            /*
            int totalpages = 0;
            int next = 0;
            try
            {
                totalpages = Convert.ToInt32(list.TotalPages);
                next = Convert.ToInt32(list.CurrentPage) + 1;
            }
            catch (Exception ex) { }
            
            list.RemoveAll(delegate(Product p) { return !p.Type.Contains("Game"); });
            
            if (list.Count == 0 && totalpages > 0 && next <= totalpages)
            {
                list.AddRange(this.GetGames(args, bTerse, next));
            }

            foreach (Product p in list) { p.generateImageUrl(); };
             */

            return list;
        }

        public Products GetMusic(string args, bool bTerse)
        {
            return this.GetMusic(args, bTerse, 0, null, null);
        }
        
        public Products GetMusic(string args, bool bTerse, int ipage)
        {
            return this.GetMusic(args, bTerse, ipage, null, null);
        }

        public Products GetMusic(string args, bool bTerse, int ipage, string postalcode, string radius)
        {
            args = args.Replace(" ", "%20");
            string search = "type='Music'"
                + "&artistName='" + args + "*'";
            string sort = "&sort=sku.desc";
            string show = "";
            string spage = "";
            string area = "";
            if (bTerse) show = "&show=sku,productId,name,artistName,type,regularPrice,salePrice,url,inStoreAvailability,onlineAvailability,thumbnailImage";
            if (postalcode != null && radius != null)
            {
                area = "+stores(area(" + postalcode + "," + radius + "))";
                if (bTerse) show += ",stores";
            }
            if (ipage > 0) spage = "&page=" + ipage.ToString();
            
            Products list = UTF8XmlSerializer.Deserialize<Products>(Get(PRODUCT + "(" + search + ")" + area + "?apiKey=" + APIKEY + sort + show + spage));

            /*
            int totalpages = 0;
            int next = 0;
            try
            {
                totalpages = Convert.ToInt32(list.TotalPages);
                next = Convert.ToInt32(list.CurrentPage) + 1;
            }
            catch (Exception ex) { }

            list.RemoveAll(delegate(Product p) { return !p.Type.Contains("Music"); });

            if (list.Count == 0 && totalpages > 0 && next <= totalpages)
            {
                list.AddRange(this.GetMusic(args, bTerse, next));
            }

            foreach (Product p in list) { p.generateImageUrl(); };
            */

            return list;
        }

        public Products GetMovies(string args, bool bTerse)
        {
            return this.GetMovies(args, bTerse, 0, null, null);
        }

        public Products GetMovies(string args, bool bTerse, int ipage)
        {
            return this.GetMovies(args, bTerse, ipage, null, null);
        }

        public Products GetMovies(string args, bool bTerse, int ipage, string postalcode, string radius)
        {
            args = args.Replace(" ", "%20");
            string search = "type='Movie'"
                + "&name='" + args + "*'";
            string sort = "&sort=sku.desc";
            string show = "";
            string spage = "";
            string area = "";
            if (bTerse) show = "&show=sku,productId,name,type,regularPrice,salePrice,url,inStoreAvailability,onlineAvailability,thumbnailImage";
            if (postalcode != null && radius != null)
            {
                area = "+stores(area(" + postalcode + "," + radius + "))";
                if (bTerse) show += ",stores";
            }
            if (ipage > 0) spage = "&page=" + ipage.ToString();
            
            Products list = UTF8XmlSerializer.Deserialize<Products>(Get(PRODUCT + "(" + search + ")" + area + "?apiKey=" + APIKEY + sort + show + spage));

            /*
            int totalpages = 0;
            int next = 0;
            try
            {
                totalpages = Convert.ToInt32(list.TotalPages);
                next = Convert.ToInt32(list.CurrentPage) + 1;
            }
            catch (Exception ex) { }

            list.RemoveAll(delegate(Product p) { return !p.Type.Contains("Movie"); });

            if (list.Count == 0 && totalpages > 0 && next <= totalpages)
            {
                list.AddRange(this.GetMovies(args, bTerse, next));
            }

            foreach (Product p in list) { p.generateImageUrl(); };
            */

            return list;
        }

        public Products GetHardGoods(string args, bool bTerse)
        {
            return this.GetHardGoods(args, bTerse, 0, null, null);
        }

        public Products GetHardGoods(string args, bool bTerse, int ipage)
        {
            return this.GetHardGoods(args, bTerse, ipage, null, null);
        }

        public Products GetHardGoods(string args, bool bTerse, int ipage, string postalcode, string radius)
        {
            // Note: Filter out accessories in favor of actual hard goods (ie: ipod, not ipod earbuds)??

            args = args.Replace(" ", "%20");
            string search = "type='HardGood'"
                + "&name='" + args + "*'";
            string sort = "&sort=regularPrice.desc";
            string show = "";
            string spage = "";
            string area = "";
            if (bTerse) show = "&show=sku,productId,name,type,regularPrice,salePrice,url,inStoreAvailability,onlineAvailability,thumbnailImage";
            if (postalcode != null && radius != null)
            {
                area = "+stores(area(" + postalcode + "," + radius + "))";
                if (bTerse) show += ",stores";
            }
            if (ipage > 0) spage = "&page=" + ipage.ToString();

            Products list = UTF8XmlSerializer.Deserialize<Products>(Get(PRODUCT + "(" + search + ")" + area + "?apiKey=" + APIKEY + sort + show + spage));

            /*
            int totalpages = 0;
            int next = 0;
            try
            {
                totalpages = Convert.ToInt32(list.TotalPages);
                next = Convert.ToInt32(list.CurrentPage) + 1;
            }
            catch (Exception ex) { }

            list.RemoveAll(delegate(Product p) { return !p.Type.Contains("HardGood"); });

            if (list.Count == 0 && totalpages > 0 && next <= totalpages)
            {
                list.AddRange(this.GetHardGoods(args, bTerse, next));
            }

            foreach (Product p in list) { p.generateImageUrl(); };
            */

            return list;
        }
    }
}
