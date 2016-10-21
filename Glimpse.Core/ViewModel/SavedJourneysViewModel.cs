using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.Extensions;
using Glimpse.Core.Messages;
using Glimpse.Core.Model;

namespace Glimpse.Core.ViewModel
{
    public class SavedJourneysViewModel : BaseViewModel, ISavedJourneysViewModel
    {
        private readonly ISavedJourneyDataService _savedJourneyDataService;
        private readonly IUserTempDataService _userDataService;

        private ObservableCollection<SavedJourney> _savedJourneys;

        public MvxCommand ReloadDataCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    var user = _userDataService.GetActiveUser();
                    SavedJourneys = (await _savedJourneyDataService.GetSavedJourneyForUser(user.UserId)).ToObservableCollection();
                });
            }
        }

        public ObservableCollection<SavedJourney> SavedJourneys
        {
            get
            {
                return _savedJourneys;
            }
            set
            {
                _savedJourneys = value;
                RaisePropertyChanged(() => SavedJourneys);
            }
        }

        public SavedJourneysViewModel(IMvxMessenger messenger, ISavedJourneyDataService savedJourneyDataService, IUserTempDataService userDataService) : base(messenger)
        {
            _savedJourneyDataService = savedJourneyDataService;
            _userDataService = userDataService;

            InitializeMessenger();
        }

        private void InitializeMessenger()
        {
            Messenger.Subscribe<CurrencyChangedMessage>(async message => await ReloadDataAsync());
        }

      
        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            var user = _userDataService.GetActiveUser();
            SavedJourneys = (await _savedJourneyDataService.GetSavedJourneyForUser(user.UserId)).ToObservableCollection();
        }
    }
}