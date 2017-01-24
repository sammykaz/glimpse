using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gemslibe.Xamarin.Droid.UI.SwipeCards;
using Android.Util;
using Android.Support.V7.Widget;

namespace Glimpse.Droid.Controls
{
    public class CustomCardView : CardView
    {
        public CustomCardView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public CustomCardView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public CustomCardView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CustomCardView(Context context) : base(context)
        {
        }

        public event Action<string> OnCardSwipeActionEvent;

        internal class CardSwipeListener : CardStack.ICardEventListener
        {
            private readonly int _discardDistancePx;
            private readonly CardStack _cardStack;

            public CardSwipeListener(int discardDistancePx, CardStack cardStack)
            {
                _discardDistancePx = discardDistancePx;
                _cardStack = cardStack;
            }

            public bool SwipeEnd(int section, float x1, float y1, float x2, float y2)
            {
                //var distance = CardUtils.Distance(x1, y1, x2, y2);
                //Discard card only if user moves card to RIGHT/LEFT
                var discard = Math.Abs(x2 - x1) > _discardDistancePx;
                var cardView = _cardStack.TopView as CustomCardView;
                if (discard)
                {
                    var action = (x2 < x1) ? "Dislike" : "Like";
                    cardView.OnCardSwipeActionEvent?.Invoke(action);
                }
                ;
                return discard;
            }

            public bool SwipeStart(int section, float x1, float y1, float x2, float y2)
            {
                //var cardView = _cardStack.TopView as ProductCard;
                return false;
            }

            public bool SwipeContinue(int section, float x1, float y1, float x2, float y2)
            {
                // var cardView = _cardStack.TopView as ProductCard;
                //cardView.ProgressToDiscad = (x2 - x1) / _discardDistancePx;

                return false;
            }

            public void Discarded(int mIndex, int direction)
            {
            }

            public void TopCardTapped()
            {
            }
        }
    }
}