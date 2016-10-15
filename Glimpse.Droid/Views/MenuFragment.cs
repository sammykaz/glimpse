using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using Glimpse.Core.ViewModel;
using Glimpse.Droid.Activities;
using System;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.left_drawer, true)]
    [Register("glimpse.droid.views.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>
    {
        public MenuFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.fragment_menu, null);
        }

        public override void OnStart()
        {
            base.OnStart();
            ViewModel.CloseMenu += OnCloseMenu;
        }

        public override void OnStop()
        {
            base.OnStop();
            ViewModel.CloseMenu -= OnCloseMenu;
        }

        private void OnCloseMenu(object sender, EventArgs e)
        {
            (Activity as MainActivity)?.CloseDrawerMenu();
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            return true;
        }
    }
}