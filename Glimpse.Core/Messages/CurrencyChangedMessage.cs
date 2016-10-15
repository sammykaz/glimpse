using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Model;

namespace Glimpse.Core.Messages
{
    public class CurrencyChangedMessage: MvxMessage
    {
        public CurrencyChangedMessage(object sender) : base(sender)
        {
        }

        public Currency NewCurrency { get; set; }
    }
}