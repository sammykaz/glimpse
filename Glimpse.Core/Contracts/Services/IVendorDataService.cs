﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IVendorDataService
    {
        Task<Vendor> SearchVendorByEmail(string email);

        Task SignUp(Vendor vendor);

        Task<int> GetVendorId(string email);

        Task<List<Vendor>> GetVendors();

        Task AddVendorPromotion(Vendor vendor);
    }
}