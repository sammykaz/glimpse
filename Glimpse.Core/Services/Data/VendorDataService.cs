using Amazon.DynamoDBv2;
using Glimpse.Core.Contracts.Services;
using System.Collections.Generic;
using System;

namespace Glimpse.Core.Services.Data
{
    public class VendorDataService: IVendorDataService
    {
        private readonly IVendorDataService _vendorRepository;

        public VendorDataService(IVendorDataService vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public void PutItem()
        {
            _vendorRepository.PutItem();
        }
    }
}
