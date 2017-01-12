using System;
using System.IO;
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
        //date related variable
        private TextView _startDateDisplay;
        private Button _btnChangeStartDate;

        private TextView _endDateDisplay;
        private Button _btnChangeEndDate;


        //Image related variables
        public static readonly int PickCoverImage = 1000;
        public static readonly int PickRegularImage1 = 1001;
        public static readonly int PickRegularImage2 = 1002;
        public static readonly int PickRegularImage3 = 1003;
        private ImageView _imageViewCover;
        private ImageView _imageViewImage1;
        private ImageView _imageViewImage2;
        private ImageView _imageViewImage3;


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

            //sets the tag and click listener. Tag is used to differentiate between cover and slideshow image
            _imageViewCover = view.FindViewById<ImageView>(Resource.Id.promotion_cover);
            _imageViewCover.Tag = PickCoverImage;     
            _imageViewCover.Click += ButtonOnClick;

            _imageViewImage1 = view.FindViewById<ImageView>(Resource.Id.promotion_picture1);
            _imageViewImage1.Tag = PickRegularImage1;
            _imageViewImage1.Click += ButtonOnClick;

            _imageViewImage2 = view.FindViewById<ImageView>(Resource.Id.promotion_picture2);
            _imageViewImage2.Tag = PickRegularImage2;
            _imageViewImage2.Click += ButtonOnClick;

            _imageViewImage3 = view.FindViewById<ImageView>(Resource.Id.promotion_picture3);
            _imageViewImage3.Tag = PickRegularImage3;
            _imageViewImage3.Click += ButtonOnClick;
        }


        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {           
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            ImageView view = (sender as ImageView);         

            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), (int)view.Tag);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if ((resultCode == (int)Result.Ok) && (data != null))
            {
                var viewModel = (CreatePromotionPart2ViewModel)ViewModel;

                if (requestCode == PickCoverImage)
                    viewModel.Bytes = GetImageSelection(_imageViewCover, data);
                else if (requestCode == PickRegularImage1)
                    viewModel.PromotionImageList.Add(GetImageSelection(_imageViewImage1, data));
                else if (requestCode == PickRegularImage2)
                    viewModel.PromotionImageList.Add(GetImageSelection(_imageViewImage2, data));
                else if (requestCode == PickRegularImage3)
                    viewModel.PromotionImageList.Add(GetImageSelection(_imageViewImage3, data)); 

            }
        }

        private byte[] GetImageSelection(ImageView view, Intent data)
        {
            Android.Net.Uri uri = data.Data;
                        
            view.SetImageURI(uri);
            view.BuildDrawingCache(true);
            Bitmap bmap = view.GetDrawingCache(true);
            view.SetImageBitmap(bmap);
            Bitmap b = Bitmap.CreateBitmap(view.GetDrawingCache(true));

            var stream = new MemoryStream();

            b.Compress(Bitmap.CompressFormat.Png, 100, stream);

            var viewModel = (CreatePromotionPart2ViewModel)ViewModel;

            return stream.ToArray();
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