using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Glimpse.Core.ViewModel;
using Glimpse.Droid.Activities;
using Glimpse.Droid.Extensions;
using Android.Widget;
using Glimpse.Core.Model;
using System.IO;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Repositories;
using SQLite.Net.Platform.XamarinAndroid;
using System.Threading.Tasks;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.LikedPromotionsFragment")]
    public class LikedPromotionsFragment : MvxFragment<LikedPromotionsViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        private LocalPromotionRepository _localPromotionRepository;
        private RadioGroup _radioGroup;
        private SearchView _searchView;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.LikedPromotionsView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Settings");

        }

        public override async void OnResume()
        {
            base.OnResume();

             _radioGroup = (RadioGroup)View.FindViewById(Resource.Id.filter_radiogroup);
             _radioGroup.SetOnCheckedChangeListener(this);

            _searchView = (SearchView)View.FindViewById(Resource.Id.searchview);           
            _searchView.SetIconifiedByDefault(true);

        }

        public async void ReloadPromotions()
        {           
            _localPromotionRepository = new LocalPromotionRepository();
            var path = GetDbPath();
            await _localPromotionRepository.InitializeAsync(path, new SQLitePlatformAndroid());
            ViewModel.PromotionList = await _localPromotionRepository.GetActivePromotions();
            ViewModel.PromotionsStored = ViewModel.PromotionList;
           
        }


        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            //radio group index is based on 1, making base 0
            checkedId = checkedId - 1;
            //the filter on previous page made this checkedID increment by 7...
            checkedId = checkedId % 7;
            if (checkedId < 0)
                checkedId = checkedId + 7;

            if (checkedId == 0)
            {
                ViewModel.SelectedItem = null;
            }
            else
            {
                int checkedId0BasedIndex = checkedId - 1;
                Categories category = (Categories)checkedId0BasedIndex;
                ViewModel.SelectedItem = category;
            }
        }

        private string GetDbPath()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, "glimpse.db3");
        }
    }
}