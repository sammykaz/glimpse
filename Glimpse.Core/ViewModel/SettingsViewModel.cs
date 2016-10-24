using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Plugins.WebBrowser;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.Extensions;
using Glimpse.Core.Messages;
using Glimpse.Core.Model;
using Glimpse.Core.Repositories;

namespace Glimpse.Core.ViewModel
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private readonly ISettingsDataService _settingsDataService;
        private string _aboutContent;
        private readonly IMvxWebBrowserTask _webBrowser;
        private VendorRepository _vendorDataService;

        public SettingsViewModel(IMvxMessenger messenger, ISettingsDataService settingsDataService, IMvxWebBrowserTask webBrowser) : base(messenger)
        {
            _settingsDataService = settingsDataService;
            _webBrowser = webBrowser;
        }


        //Command to initiate PutItem in the database

        public MvxCommand PutItemInDB
        {
            get
            {
                return new MvxCommand(() =>
                {
                    _vendorDataService = new VendorRepository();
                    _vendorDataService.PutItem();

    
                });
            }
        }

        //

        public MvxCommand HelpCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    _webBrowser.ShowWebPage
                        ("http://www.snowball.be");//what an awesome site!
                });
            }
        }
        public MvxCommand SwitchCurrencyCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Messenger.Publish(
                        new CurrencyChangedMessage(this)
                            { NewCurrency = ActiveCurrency });
                    _settingsDataService.SetActiveCurrency(ActiveCurrency);
                });
            }
        }

        private ObservableCollection<Currency> _currencies;
        private Currency _activeCurrency;

        public Currency ActiveCurrency
        {
            get { return _activeCurrency; }
            set
            {
                _activeCurrency = value;
                RaisePropertyChanged(() => ActiveCurrency);
            }
        }

        public string AboutContent
        {
            get { return _aboutContent; }
            set
            {
                _aboutContent = value;
                RaisePropertyChanged(() => AboutContent);
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
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
                Currencies = _settingsDataService.GetCurrencies().ToObservableCollection();
                AboutContent = _settingsDataService.GetAboutContent();
                ActiveCurrency = Currencies[0];
            });
        }
    }
}