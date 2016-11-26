using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glimpse.Core.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(IMvxMessenger messenger) : base(messenger)
        {
         
        }

        private List<string> _languages = new List<string> { "French", "English" };
        public List<string> Languages
        {
            get
            {
                return _languages;
            }
        }

        private string _currentLanguage;
        public string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                RaisePropertyChanged(() => CurrentLanguage);

            }
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
                    int x = 0;
                });
            }
        }
    }
}