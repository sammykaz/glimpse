﻿using Glimpse.Core.Model;
using System;
using System.Net;
using System.Xml.Linq;

namespace Glimpse.Core.Utility
{
    public static class Geocoding
    {
        public static Location Geocode(string addressToConvert)
        {
            var address = addressToConvert;
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponseAsync();


            var xdoc = XDocument.Load(response.Result.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat");
            var lng = locationElement.Element("lng");

            double latitude = Double.Parse(lat.Value);
            double longitude = Double.Parse(lng.Value);
            
            return new Location(latitude, longitude);
        }
    }
}