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
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Remix
{
    [XmlRoot("products")]
    public class Products : List<Product>
    {
        [XmlAttribute("currentPage")]
        public string CurrentPage;

        [XmlAttribute("totalPages")]
        public string TotalPages;

        [XmlAttribute("from")]
        public string From;

        [XmlAttribute("to")]
        public string To;

        [XmlAttribute("total")]
        public string Total;

        [XmlAttribute("queryTime")]
        public string QueryTime;

        [XmlAttribute("totalTime")]
        public string TotalTime;

        [XmlAttribute("canonicalUrl")]
        public string CanonicalURL;

        [XmlAttribute("guid")]
        public string Guid;

        public Products() { }

        public Products(string currentpage, string totalpages, string from, string to,
                    string total, string querytime, string totaltime, string canonicalurl)
        {
            this.CurrentPage = currentpage;
            this.TotalPages = totalpages;
            this.From = from;
            this.To = to;
            this.Total = total;
            this.QueryTime = querytime;
            this.TotalTime = totaltime;
            this.CanonicalURL = canonicalurl;
        }

        public override bool Equals(object obj)
        {
            bool bRet = false;
            Products prods = ((Products)obj);

            if (this.Count == prods.Count)
            {
                for (int x = 0; x < this.Count; x++)
                {
                    Product p1 = this[x];
                    Product p2 = prods[x];
                    bRet = p1.Equals(p2);
                    if (!bRet) break;
                }
            }

            return bRet;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        /*
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        public void WriteXml(XmlWriter writer)
        {
            return;
        }

        public void ReadXml(XmlReader reader)
        {
            //reader.Read();
            //reader.ReadStartElement("products");

            this.CurrentPage = reader.GetAttribute("currentPage");
            this.TotalPages = reader.GetAttribute("totalPages");
            this.From = reader.GetAttribute("from"); ;
            this.To = reader.GetAttribute("to");
            this.Total = reader.GetAttribute("total");
            this.QueryTime = reader.GetAttribute("queryTime");
            this.TotalTime = reader.GetAttribute("totalTime");
            this.CanonicalURL = reader.GetAttribute("canonicalUrl");

            reader.Read();

            XmlSerializer serializer = new XmlSerializer(typeof(Product));
            XmlSerializer serializer2 = new XmlSerializer(typeof(Store));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                try
                {
                    Product item = (Product)serializer.Deserialize(reader);
                    Store s = (Store)
                    if(item != null) this.Add(item);
                }
                catch { }
            }

            reader.ReadEndElement();

        } */
        
        public string ToXml()
        {
            return UTF8XmlSerializer.Serialize(this);
        }
    }

    [XmlType("product")]
    public class Product
    {
        [XmlElement("sku")]
        public string Sku = "";

        [XmlElement("productId")]
        public string ProductId = "";

        [XmlElement("name")]
        public string Name = "";

        [XmlElement("artistName")]
        public string ArtistName = "";

        [XmlElement("type")]
        public string Type = "";

        [XmlElement("regularPrice")]
        public string RegularPrice = "";

        [XmlElement("salePrice")]
        public string SalePrice = "";

        [XmlElement("url")]
        public string BBYUrl = "";

        [XmlElement("thumbnailImage")]
        public string imageUrl = "";

        [XmlElement("inStoreAvailability")]
        public string InStoreAvailability = "";

        [XmlElement("onlineAvailability")]
        public string OnlineAvailability = "";

        private List<Store> _stores = new List<Store>();

        [XmlArray("stores")]
        public List<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; }
        }

        [XmlAttribute("guid")]
        public string Guid;

        public Product() { }

        public Product(string sku, string productid, string name, string artistname, string type,
                        string regularprice, string saleprice, string bbyurl,
                        string instoreavailability, string onlineavailability)
        {
            this.Sku = sku;
            this.ProductId = productid;
            this.Name = name;
            this.ArtistName = artistname;
            this.Type = type;
            this.RegularPrice = regularprice;
            this.SalePrice = saleprice;
            this.BBYUrl = bbyurl;
            this.InStoreAvailability = instoreavailability;
            this.OnlineAvailability = onlineavailability;
        }

        public override bool Equals(object obj)
        {
            return obj is Product &&
                ((Product)obj).Sku == Sku &&
                ((Product)obj).ProductId == ProductId &&
                ((Product)obj).Name == Name &&
                ((Product)obj).ArtistName == ArtistName && 
                ((Product)obj).Type == Type;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public String ToXml()
        {
            return UTF8XmlSerializer.Serialize(this);
        }
    }
}
