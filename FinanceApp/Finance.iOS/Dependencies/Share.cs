using System;
using System.Threading.Tasks;
using Finance.Interfaces;
using Finance.iOS.Dependencies;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Share))]
namespace Finance.iOS.Dependencies
{
	public class Share : IShare
    {
        public async Task Show(string title, string url)
        {
            var viewController = GetVisibleViewController();
            var items = new NSObject[] { NSObject.FromObject(url) };
            var activityController = new UIActivityViewController(items, null);

            if (activityController.PopoverPresentationController != null)
                activityController.PopoverPresentationController.SourceView = viewController.View;

            await viewController.PresentViewControllerAsync(activityController, true);
        }

        private UIViewController GetVisibleViewController()
        {
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootViewController.PresentedViewController == null)
                return rootViewController;

            if (rootViewController.PresentedViewController is UINavigationController)
                return ((UINavigationController)rootViewController.PresentedViewController).TopViewController;

            if (rootViewController.PresentedViewController is UITabBarController)
                return ((UITabBarController)rootViewController.PresentedViewController).SelectedViewController;

            return rootViewController.PresentedViewController;
        }
    }
}
