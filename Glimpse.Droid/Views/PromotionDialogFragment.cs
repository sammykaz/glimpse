using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Glimpse.Core.Helpers;
using Glimpse.Droid.Helpers;

namespace Glimpse.Droid.Views
{
    public class PromotionDialogFragment : DialogFragment
    {
        private readonly string title;
        private readonly string description;
        private readonly int expirationDate;
        private readonly string companyName;
        private Bitmap image;

        public PromotionDialogFragment(PromotionItem item)
        {
            title = item.Title;
            description = item.Description;
            expirationDate = item.ExpirationDate;
            companyName = item.CompanyName;
            image = item.PromotionImage;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.PromotionDialogView, container, false);

            Activity.RunOnUiThread(() =>
            {
                var txtTitle = view.FindViewById<TextView>(Resource.Id.txtPromoDialogTitle);
                var txtDescription = view.FindViewById<TextView>(Resource.Id.txtPromoDialogDescription);
                var txtExpirationDate = view.FindViewById<TextView>(Resource.Id.txtPromoDialogExpirationDate);
                var txtCompanyName = view.FindViewById<TextView>(Resource.Id.txtPromoDialogCompanyName);
                var promotionImage = view.FindViewById<ImageView>(Resource.Id.imgPromoDialogPicture);

                txtTitle.Text = title;
                txtDescription.Text = description;
                txtExpirationDate.Text = expirationDate.ToString();
                txtCompanyName.Text = companyName;
                promotionImage.SetImageBitmap(BitmapProcessing.decodeSampledBitmapFromResource(Resources,
                    Resource.Id.imgPromoDialogPicture, 400, 400));
            });

            return view;
        }
    }
}