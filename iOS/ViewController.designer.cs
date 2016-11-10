// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SJCoinWallet.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addressBookButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel addressLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel balanceLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView qrcodeView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton refreshButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton sendcoinsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton settingsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView sjcoinLogo { get; set; }

        [Action ("AddressBookButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AddressBookButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("RefreshButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void RefreshButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SendcoinsButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SendcoinsButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (addressBookButton != null) {
                addressBookButton.Dispose ();
                addressBookButton = null;
            }

            if (addressLabel != null) {
                addressLabel.Dispose ();
                addressLabel = null;
            }

            if (balanceLabel != null) {
                balanceLabel.Dispose ();
                balanceLabel = null;
            }

            if (qrcodeView != null) {
                qrcodeView.Dispose ();
                qrcodeView = null;
            }

            if (refreshButton != null) {
                refreshButton.Dispose ();
                refreshButton = null;
            }

            if (sendcoinsButton != null) {
                sendcoinsButton.Dispose ();
                sendcoinsButton = null;
            }

            if (settingsButton != null) {
                settingsButton.Dispose ();
                settingsButton = null;
            }

            if (sjcoinLogo != null) {
                sjcoinLogo.Dispose ();
                sjcoinLogo = null;
            }
        }
    }
}