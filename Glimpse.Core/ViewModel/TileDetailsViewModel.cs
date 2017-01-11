using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class TileDetailsViewModel : BaseViewModel
    {
        private List<byte[]> _images;

        public TileDetailsViewModel()
        { }


        public List<byte[]> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                RaisePropertyChanged(() => Images);
            }
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override Task InitializeAsync()
        {
            return Task.Run(() =>
            {
                
            });
        }
    }
}