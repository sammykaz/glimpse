using Glimpse.Core.Contracts.Services;
using Plugin.Connectivity;

namespace Glimpse.Core.Services.General
{
    public class ConnectionService : IConnectionService
    {
        public bool CheckOnline()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
