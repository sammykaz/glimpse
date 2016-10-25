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
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace Glimpse.Core.ViewModel
{
    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private readonly ISettingsDataService _settingsDataService;
        private string _aboutContent;
        private readonly IMvxWebBrowserTask _webBrowser;

        //GLOBAL
        AmazonDynamoDBClient client;

        public MvxCommand HelpCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                   
                    try
                    {
                       IEnumerable<string> profiles = InstanceProfileAWSCredentials.GetAvailableRoles();

                        AWSCredentials credentials = new BasicAWSCredentials("AKIAJO5TSFBKWVDWLOUA", "K+sH0xADftyggpX2hIwDqwblG/gUFKpYecWUvSm+");
                        AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
                        config.ServiceURL = "http://dynamodb.us-east-1.amazonaws.com";
                        config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
                        client = new AmazonDynamoDBClient(credentials, config);

                        UploadData();
                    }
                    catch (AmazonDynamoDBException e) { System.Diagnostics.Debug.WriteLine("DynamoDB Message" + e.StackTrace); }
                    catch (AmazonServiceException e) { System.Diagnostics.Debug.WriteLine("Service Exception" + e.StackTrace); }
                    catch (Exception e) { System.Diagnostics.Debug.WriteLine("General Exception:" + e.StackTrace); }
                    


                    //_webBrowser.ShowWebPage
                    //    ("http://www.snowball.be");//what an awesome site!
                });
            }
        }

        public void UploadData()
        {
            Table outputTable;
            Boolean exists = Table.TryLoadTable (client, "VendorAccount", out outputTable);
            Table vendorTable = Table.LoadTable(client, "VendorAccount");
            var d1 = new Document();

            d1["id"] = "5";
            d1["branchAddress"] = "4th Balls Street";
            d1["businessPhoneNumber"] = "1-234-567-8910";
            vendorTable.PutItemAsync(d1);

    

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

        public SettingsViewModel(IMvxMessenger messenger, ISettingsDataService settingsDataService, IMvxWebBrowserTask webBrowser) : base(messenger)
        {
            _settingsDataService = settingsDataService;
            _webBrowser = webBrowser;

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