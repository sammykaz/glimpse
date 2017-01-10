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
        private string _currentLanguage;
        private List<string> _languages;

        public TileDetailsViewModel(IMvxMessenger messenger) : base(messenger)
        {
          
        }

        
        public List<string> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
                RaisePropertyChanged(() => Languages);
            }
        }


        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                RaisePropertyChanged(() => CurrentLanguage);
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
                CurrentLanguage = Settings.Language;
                Languages = new List<string> { CurrentLanguage };
                if (CurrentLanguage == "English")
                    _languages.Add("Français");
                else if (CurrentLanguage == "Français")
                    _languages.Add("English");
            });
        }

        /// <summary>
        /// Triggered when the language is selected
        /// </summary>
        public MvxCommand SwitchLanguageCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    Settings.Language = CurrentLanguage;
                });
            }
        }
    }
}