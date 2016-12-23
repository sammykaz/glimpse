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
using Android.Content;
using System.Text;
using Android.App;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(Glimpse.Core.ViewModel.MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.CreatePromotionPart2Fragment")]
    public class CreatePromotionPart2Fragment : MvxFragment<CreatePromotionPart2ViewModel> 
    {
        private TextView _startDateDisplay;
        private Button _btnChangeStartDate;

        private TextView _endDateDisplay;
        private Button _btnChangeEndDate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.CreatePromotionPart2View, null); 
        }
    
  

      




        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Create Promotion");


            _startDateDisplay = view.FindViewById<TextView>(Resource.Id.start_date_display);
            _startDateDisplay.Text = DateTime.Now.ToLongDateString();
            _btnChangeStartDate = view.FindViewById<Button>(Resource.Id.btnChangeStartDate);
            _btnChangeStartDate.Click += StartDateSelect_OnClick;

            _endDateDisplay = view.FindViewById<TextView>(Resource.Id.end_date_display);
            _btnChangeEndDate = view.FindViewById<Button>(Resource.Id.btnChangeEndDate);
            _btnChangeEndDate.Click += EndDateSelect_OnClick;

            /* ImageView acc_Button = view.FindViewById<ImageView>(Resource.Id.imgPic);
             acc_Button.Click += delegate
             {
                 OnClick(this.View);
             }; */
        }

        void StartDateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _startDateDisplay.Text = time.ToLongDateString();
            });
           
            frag.Show(this.Activity.FragmentManager, "dunno ");
        }

        void EndDateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _endDateDisplay.Text = time.ToLongDateString();
            });

            frag.Show(this.Activity.FragmentManager, "dunno ");
        }


        /*public void OnClick(View view)
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(
            Intent.CreateChooser(imageIntent, "Select photo"), 0);
        }*/

        public override void OnStart()
        {
            base.OnStart();
        }

    }
}