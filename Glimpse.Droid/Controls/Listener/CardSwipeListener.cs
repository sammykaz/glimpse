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
using Glimpse.Droid.Controls;

namespace Glimpse.Droid.Controls.Listener
{
    

    public class CardSwipeListener : CardStack.ICardEventListener
    {
        private readonly int _discardDistancePx;
        private readonly CardStack _cardStack;
        private bool _swipeDiscard;
        public event Action<string> OnCardSwipeActionEvent;

        public CardSwipeListener(int discardDistancePx, CardStack cardStack)
        {
            _discardDistancePx = discardDistancePx;
            _cardStack = cardStack;
            _swipeDiscard = false;
        }

        public bool SwipeEnd(int section, float x1, float y1, float x2, float y2)
        {
            //Discard card only if user moves card to Right or Left
            _swipeDiscard = Math.Abs(x2 - x1) > _discardDistancePx;
            var cardView = _cardStack.TopView as CustomCardView;
            if (_swipeDiscard)
            {
                var action = (x2 < x1) ? "Dislike" : "Like";
                OnCardSwipeActionEvent?.Invoke(action);
            }            
            return _swipeDiscard;
        }

        public bool SwipeStart(int section, float x1, float y1, float x2, float y2)
        {
            return false;
        }

        public bool SwipeContinue(int section, float x1, float y1, float x2, float y2)
        {
            return false;
        }

        public void TopCardTapped()
        {
        }

       //for some reason discarding the card by swiping returns index+1.therefore this is my current solution for thios issue.
       //use do something to send promo to like list
        public void Discarded(int index, int direction)
        {
            if (_swipeDiscard)
            {
                dosomething(index - 1, direction);
                _swipeDiscard = false;
            }
            else
                dosomething(index, direction);
        }

        private void dosomething(int index, int direction)
        {
            //direction: 2=dislike, 3=like
           
            var x = _cardStack.Adapter.GetItem(index);  // to get discarded promotion 
        }
    }
}