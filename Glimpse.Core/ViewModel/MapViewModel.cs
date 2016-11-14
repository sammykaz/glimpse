using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using System.Collections.ObjectModel;
using Glimpse.Core.Contracts.Services;
using System.Threading.Tasks;
using Glimpse.Core.Extensions;

namespace Glimpse.Core.ViewModel
{
    public class MapViewModel:  MvxViewModel

    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private Promotion _promotion;
        private ObservableCollection<Vendor> _vendors;
        private IVendorDataService _vendorDataService;

        public MapViewModel(IVendorDataService vendorDataService)
        {
            _vendorDataService = vendorDataService;
            LoadVendors();
        }


        public int DefaulZoom
        {
            get { return _defaultZoom; }
        }


        public int DefaultTilt
        {
            get { return _defaultTilt; }
        }
   

        public int DefaultBearing
        {
            get { return _defaultBearing; }
        }


        public ObservableCollection<Vendor> Vendors
        {
            get { return _vendors; }
            set
            {
                _vendors = value;
                RaisePropertyChanged(() => Vendors);
            }
        }

        internal async Task LoadVendors()

        {
            _vendors = (await _vendorDataService.GetVendors()).ToObservableCollection();
        }
      }
    }

