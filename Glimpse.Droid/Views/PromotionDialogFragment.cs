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
        private string title;
        private string description;
        private string expirationDate;
        private string companyName;
        private Bitmap image;

        public PromotionDialogFragment(PromotionItem item)
        {
            this.title = item.Title;
            this.description = item.Description;
            this.expirationDate = item.ExpirationDate;
            this.companyName = item.CompanyName;
            this.image = item.PromotionImage;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.PromotionDialogView, container, false);

            TextView txtTitle = view.FindViewById<TextView>(Resource.Id.txtPromoDialogTitle);
            TextView txtDescription = view.FindViewById<TextView>(Resource.Id.txtPromoDialogDescription);
            TextView txtExpirationDate = view.FindViewById<TextView>(Resource.Id.txtPromoDialogExpirationDate);
            TextView txtCompanyName = view.FindViewById<TextView>(Resource.Id.txtPromoDialogCompanyName);
            ImageView promotionImage = view.FindViewById<ImageView>(Resource.Id.imgPromoDialogPicture);

            txtTitle.Text = title;
            txtDescription.Text = description;
            txtExpirationDate.Text = expirationDate;
            txtCompanyName.Text = companyName;
            promotionImage.SetImageBitmap(BitmapProcessing.decodeSampledBitmapFromResource(Resources,Resource.Id.imgPromoDialogPicture, 200, 200));


            return view;
        }


    }
}