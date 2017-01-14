﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Services.General;
using Glimpse.Core.Services.Data;

namespace Glimpse.Core.Services.Data
{
    public class PromotionDataService : IPromotionDataService
    {
        private readonly IPromotionRepository promotionRepository;
        private readonly IVendorRepository vendorRepository;

        public PromotionDataService(IPromotionRepository promotionRepository, IVendorRepository vendorRepositor)
        {
            this.promotionRepository = promotionRepository;
            this.vendorRepository = vendorRepositor;
        }

        public async Task<List<Promotion>> GetPromotion(int id)
        {
            return await promotionRepository.GetPromotion(id);
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            return await promotionRepository.GetPromotions();
        }

        public async Task<List<Promotion>> GetPromotionsByCategory(Categories category)
        {
            return await promotionRepository.GetPromotionsByCategory(category);
        }

        public async Task StorePromotion(Promotion promotion)
        {
            await promotionRepository.StorePromotion(promotion);
        }

        public async Task<List<PromotionWithLocation>> GetActivePromotions()
        {
            List<Promotion> allPromotions = await promotionRepository.GetPromotions();
            List<Vendor> allVendors = await vendorRepository.GetVendors();

            //Get unique vendors
            var uniqueVendors = allVendors.GroupBy(x => new { x.Location.Lat, x.Location.Lng }).Select(g => g.First()).ToList();

            List<Promotion> activePromotions = allPromotions.Where(e => e.PromotionStartDate.CompareTo(DateTime.Now) <= 0 && e.PromotionEndDate.CompareTo(DateTime.Now) >= 0).ToList();

            var mapPromotions = uniqueVendors.Join(activePromotions, e => e.VendorId, b => b.VendorId,
                (e, b) => new PromotionWithLocation
                {
                    VendorId = b.VendorId,
                    Title = b.Title,
                    Location = e.Location,
                    Description = b.Description,
                    CompanyName = e.CompanyName,
                    Duration = 9999,
                    Image = b.PromotionImage,
                    PromotionId = b.PromotionId,
                    PromotionStartDate = b.PromotionStartDate,
                    PromotionEndDate = b.PromotionEndDate
                });

            //Select all promotions excluding those with empty locations
            var validatedMapPromotions = mapPromotions.Where(e => e.Location.Lat != 0 || e.Location.Lng != 0);

            return validatedMapPromotions.ToList();
        }
    }

}