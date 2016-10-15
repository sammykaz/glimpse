using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IJourneyDataService
    {
        Task<IEnumerable<Journey>> SearchJourney(int fromCity, int toCity, DateTime journeyDate, DateTime departureTime);

        Task<Journey> GetJourneyDetails(int journeyId);
    }
}