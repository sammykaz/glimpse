using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using Glimpse.Core.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Glimpse.Droid.Extensions;
using Square.TimesSquare;
using Glimpse.Droid.Activities;
using Glimpse.Droid;
using Glimpse.Droid.Views;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(Glimpse.Core.ViewModel.LoginMainViewModel), Resource.Id.login_content, true)]
    [Register("glimpse.droid.views.UserSignUpFragment")]
    public class UserSignUpFragment : MvxFragment<UserSignUpViewModel>
    {


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.UserSignUpView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as LoginActivity).SetCustomTitle("User Sign Up");
            Button acc_Button = view.FindViewById<Button>(Resource.Id.SignUpButton);
            acc_Button.Click += delegate
            {
                OnClick(this.View);
            };
        }

        public override void OnStart()
        {
            base.OnStart();
        }
        public void OnClick(View view)
        {
            string _firstName = view.FindViewById<EditText>(Resource.Id.txtFirstName).Text;           
            string _email = view.FindViewById<EditText>(Resource.Id.txtEmail).Text;

            SendMail sendMail = new Glimpse.Droid.Views.SendMail();

            //Mail for user
            string mailBody = sendMail.CreateMailBodyForVendor(_firstName);
            sendMail.SendEmail("Account Created", mailBody, _email);         
        }

    }
}