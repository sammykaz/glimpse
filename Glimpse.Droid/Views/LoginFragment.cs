using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Glimpse.Core.ViewModel;
using Glimpse.Droid.Activities;
using Glimpse.Droid.Extensions;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Android.Graphics;
using Glimpse.Droid.Controls;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(LoginMainViewModel), Resource.Id.login_content, true)]
    [Register("glimpse.droid.views.LoginFragment")]
    public class LoginFragment : MvxFragment<LoginViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.LogInView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as LoginActivity).SetCustomTitle("Login");
            

            //Adding Custom Fonts
            TextView welcomeLabel = view.FindViewById<TextView>(Resource.Id.lblWelcomeGlimpse);
            Typeface tf = Typeface.CreateFromAsset(this.Activity.Assets, "Avenir-Light.otf");
            welcomeLabel.SetTypeface(tf, TypefaceStyle.Normal);

        }
    }
}