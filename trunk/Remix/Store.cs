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
    [XmlType("store")]
    public class Store
    {
        [XmlElement("storeId")]
        public string StoreID;

        [XmlElement("name")]
        public string Name;

        [XmlElement("address")]
        public string Address;

        [XmlElement("city")]
        public string City;

        [XmlElement("region")]
        public string Region;

        [XmlElement("postalCode")]
        public string PostalCode;

        [XmlElement("fullPostalCode")]
        public string FullPostalCode;

        [XmlElement("country")]
        public string Country;

        [XmlElement("lat")]
        public string Latitude;

        [XmlElement("lng")]
        public string Longitude;

        [XmlElement("phone")]
        public string Phone;

        [XmlElement("hours")]
        public string Hours;

        [XmlElement("distance")]
        public string Distance;

        [XmlElement("guid")]
        public string Guid;

        public Store() { }

        public Store(string address,string city,string country,string distance,string fullpostalcode,
                string hours,string latitude,string longitude,string name,string phone,
                string postalcode,string region,string storeid)
        {
            this.Address = address;
            this.City = city;
            this.Country = country;
            this.Distance = distance;
            this.FullPostalCode = fullpostalcode;
            this.Hours = hours;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = name;
            this.Phone = phone;
            this.PostalCode = postalcode;
            this.Region = region;
            this.StoreID = storeid;
            
        }

        public override bool Equals(object obj)
        {
            return obj is Store &&
                ((Store)obj).Name == Name &&
                ((Store)obj).Address == Address &&
                ((Store)obj).City == City &&
                ((Store)obj).PostalCode == PostalCode &&
                ((Store)obj).Latitude == Latitude &&
                ((Store)obj).Longitude == Longitude;
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
