using System;
using System.Collections.Generic;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Com.Google.Maps.Android.Clustering;
using Glimpse.Core.Model;
using Glimpse.Droid.Helpers;


namespace Glimpse.Core.Helpers
{
    public class PromotionItem : Java.Lang.Object, IClusterItem
    {
        public PromotionItem(List<PromotionWithLocation> promotionItems, double lat, double lng)
        {
            PromotionItems = promotionItems;
            Position = new LatLng(lat, lng);
        }


        public PromotionItem(double lat, double lng, string title, string description, int expirationDate, string companyName, Bitmap promotionImage, int promotionId)
        {
            Position = new LatLng(lat, lng);
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            CompanyName = companyName;
            PromotionImage = promotionImage;
            PromotionId = promotionId;
        }

        public int PromotionId { get; set; }
        public LatLng Position { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int ExpirationDate { get; set; }

        public string CompanyName { get; set; }

        public Bitmap PromotionImage { get; set; }

        public List<PromotionWithLocation> PromotionItems { get; set; }
    }
}
