using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.Model;
using MvvmCross.Platform.Platform;

namespace Glimpse.Core.ViewModel
{
    public class JourneyDetailViewModel : BaseViewModel, IJourneyDetailViewModel
    {
        private readonly IJourneyDataService _journeyDataService;
        private readonly IDialogService _dialogService;
        private readonly IUserTempDataService _userDataService;
        private Journey _selectedJourney;
        private int _journeyId;
        private int _numberOfTravellers;      

        public MvxCommand CloseCommand
        { get { return new MvxCommand(() => Close(this)); } }

        public Journey SelectedJourney
        {
            get { return _selectedJourney; }
            set
            {
                _selectedJourney = value;
                RaisePropertyChanged(() => SelectedJourney);
            }
        }

        public int NumberOfTravellers
        {
            get { return _numberOfTravellers; }
            set
            {
                _numberOfTravellers = value;
                RaisePropertyChanged(() => NumberOfTravellers);
            }
        }

        public JourneyDetailViewModel(IMvxMessenger messenger, 
            IJourneyDataService journeyDataService,
            IDialogService dialogService, 
            IUserTempDataService userDataService) : base(messenger)
        {
            _journeyDataService = journeyDataService;
            _dialogService = dialogService;
            _userDataService = userDataService;
        }

        public void Init(int journeyId)
        {
            _journeyId = journeyId;
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            SelectedJourney = await _journeyDataService.GetJourneyDetails(_journeyId);
        }

        public class SavedState
        {
            public int JourneyId { get; set; }
        }

        public SavedState SaveState()
        {
            MvxTrace.Trace("SaveState called");
            return new SavedState { JourneyId = _journeyId };
        }

        public void ReloadState(SavedState savedState)
        {
            MvxTrace.Trace("ReloadState called with {0}", 
                savedState.JourneyId);
            _journeyId = savedState.JourneyId;
        }
    }
}