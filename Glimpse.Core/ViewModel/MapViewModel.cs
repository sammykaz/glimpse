using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;

namespace MyTrains.Core.ViewModel
{
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel(IMvxMessenger messenger) : base(messenger)
        {
            Store = new First()
            {
                Name = "Store",
                Location = new Location()
                {
                    Lat = 51.4,
                    Lng = 0.4
                },
            };
        }

         
        private First _store;
        public First Store
        {
            get { return _store; }
            set { _store = value; RaisePropertyChanged(() => Store); }
        }


        public IMvxCommand MoveCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Store.Location = new Location()
                    {
                        Lat = Store.Location.Lat - 0.1,
                        Lng = Store.Location.Lng
                    };

                });
            }
        }
    

}
}