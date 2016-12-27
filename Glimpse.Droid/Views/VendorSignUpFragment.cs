using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using Glimpse.Core.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Glimpse.Droid.Extensions;
using Square.TimesSquare;
using Glimpse.Droid.Activities;
using Glimpse.Droid;
using Glimpse.Droid.Views;
using Android.Gms.Location.Places.UI;
using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Glimpse.Core.Model;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(Glimpse.Core.ViewModel.LoginMainViewModel), Resource.Id.login_content, true)]
    [Register("glimpse.droid.views.VendorSignUpFragment")]
    public class VendorSignUpFragment : MvxFragment<VendorSignUpViewModel>
    {
        private static readonly int PLACE_PICKER_REQUEST = 1;
        private Button _selectBuisinessLocationButton;
        private TextView _addressTextView;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.VendorSignUpView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as LoginActivity).SetCustomTitle("Vendor Sign Up");

            _addressTextView = (this.Activity as LoginActivity).FindViewById<TextView>(Resource.Id.txtAddress);
            _selectBuisinessLocationButton = (this.Activity as LoginActivity).FindViewById<Button>(Resource.Id.selectBuisinessLocationButton);
            _selectBuisinessLocationButton.Click += OnSelectBuisinessLocationTapped;

            //Sends email on click
            Button acc_Button = view.FindViewById<Button>(Resource.Id.SignUpButton);
            acc_Button.Click += delegate
            {
                OnClick(this.View);
            };
        }


        private void OnSelectBuisinessLocationTapped(object sender, EventArgs eventArgs)
        {
            PlacePicker.IntentBuilder builder = new PlacePicker.IntentBuilder();
            //If the user already picked a location, the place picker will zoom on the previously selected location. Else, it will default to the geolocation
            if (ViewModel.Location.Lat != 0 && ViewModel.Location.Lng != 0)
             builder.SetLatLngBounds( new LatLngBounds(new LatLng(ViewModel.Location.Lat-0.002500, ViewModel.Location.Lng - 0.002500), new LatLng(ViewModel.Location.Lat + 0.002500, ViewModel.Location.Lng + 0.002500)));     
               
            StartActivityForResult(builder.Build(this.Activity as LoginActivity), PLACE_PICKER_REQUEST);
        }



        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == PLACE_PICKER_REQUEST && resultCode == (int) Result.Ok)
            {
                setLocationProperty(data);
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

  
        /// <summary>
        /// This method sets the value of the address text box as well as the view model's location property
        /// </summary>
        /// <param name="data"></param>
        private void setLocationProperty(Intent data)
        {
            var placePicked = PlacePicker.GetPlace(this.Context, data);
            _addressTextView.Text = placePicked?.AddressFormatted?.ToString();

            ViewModel.Location = new Location()
            {
                Lat = placePicked.LatLng.Latitude,
                Lng = placePicked.LatLng.Longitude
            };
        }

        public override void OnStart()
        {
            base.OnStart();
        }
        public void OnClick(View view)
        {
            string _firstName = view.FindViewById<EditText>(Resource.Id.txtFirstName).Text;
            string _company = view.FindViewById<EditText>(Resource.Id.txtCompanyName).Text;
            string _email = view.FindViewById<EditText>(Resource.Id.txtEmail).Text;

            SendMail sendMail = new Glimpse.Droid.Views.SendMail();

            //Mail for vendor
            string mailBody = sendMail.CreateMailBodyForVendor(_firstName);
            sendMail.SendEmail("Account Created", mailBody, _email);

            //Mail for Admin
            mailBody = sendMail.CreateMailBodyForAdmin(_firstName,_company,"No number!",_email);
            sendMail.SendEmail("New Sign-Up Information", mailBody, "vendor.smtptest@gmail.com");
        }
        
    }
}