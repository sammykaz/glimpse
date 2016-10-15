using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface ISavedJourneyDataService
    {
        Task<IEnumerable<SavedJourney>> GetSavedJourneyForUser(int userId);

        Task AddSavedJourney(int userId, int journeyId, int numberOfTravellers);
    }
}