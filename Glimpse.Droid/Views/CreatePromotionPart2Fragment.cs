using System;
using System.IO;
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
using MvvmCross.Binding.BindingContext;
using Android.Graphics;
using Android.Util;

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

        public static readonly int PickImageId = 1000;
        private ImageView _imageView;

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


            _imageView = view.FindViewById<ImageView>(Resource.Id.promotion_picture);
            Button button = view.FindViewById<Button>(Resource.Id.btnChoosePicture);
            button.Click += ButtonOnClick;

         
        }


        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), PickImageId);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if ((resultCode == (int)Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                
                _imageView = (this.Activity as MainActivity).FindViewById<ImageView>(Resource.Id.promotion_picture);
                _imageView.SetImageURI(uri);


                _imageView.BuildDrawingCache(true);
                Bitmap bmap = _imageView.GetDrawingCache(true);
                _imageView.SetImageBitmap(bmap);
                Bitmap b = Bitmap.CreateBitmap(_imageView.GetDrawingCache(true));


                var stream = new MemoryStream();

                b.Compress(Bitmap.CompressFormat.Png, 100, stream);

                var viewModel = (CreatePromotionPart2ViewModel)ViewModel;

                byte[] bytes = stream.ToArray();

                viewModel.Bytes = bytes;
            }
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