using Glimpse.Core.Model;
using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class TileDetailsViewModel : BaseViewModel
    {
        private List<byte[]> _images;
        private int promotionId; 

        public TileDetailsViewModel()
        { }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("PromotionID"))
            {
                promotionId = Convert.ToInt32((parameters.Data["PromotionID"]));
            }


            base.InitFromBundle(parameters);
        }

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

     /*   public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override Task InitializeAsync()
        {
            return Task.Run(() =>
            {
                
            });
        }*/
    }
}