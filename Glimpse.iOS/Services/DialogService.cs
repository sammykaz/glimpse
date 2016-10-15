using System.Threading.Tasks;
using Glimpse.Core.Contracts.Services;
using UIKit;

namespace Glimpse.iOS.Services
{
    public class DialogService: IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonText)
        {
            return Task.Run(() =>
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    new UIAlertView(title, message, null, buttonText).Show();
                }));
        }
    }
}
