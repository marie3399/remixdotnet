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
