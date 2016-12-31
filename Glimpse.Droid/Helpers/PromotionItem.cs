﻿using Android.Gms.Maps.Model;
using Android.Graphics;
using Com.Google.Maps.Android.Clustering;
using Glimpse.Droid.Helpers;


namespace Glimpse.Core.Helpers
{
    public class PromotionItem : Java.Lang.Object, IClusterItem
    {
        private double lat;
        private double lng;
        private Android.Graphics.Bitmap image;

        public PromotionItem(double lat, double lng, string title, string description, string expirationDate, string companyName, Bitmap promotionImage)
        {
            Position = new LatLng(lat, lng);
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            CompanyName = companyName;
            PromotionImage = promotionImage;
        }

        public LatLng Position { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string ExpirationDate { get; set; }

        public string CompanyName { get; set; }

        public Bitmap PromotionImage { get; set; }
    }
}