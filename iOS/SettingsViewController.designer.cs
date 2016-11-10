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
    [Register ("SettingsViewController")]
    partial class SettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel sAddressLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton scanPrivateKeyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton scanPublicKeyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton selectAddressButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView sPrivateKeyTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView sPublicKeyTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField urlTextField { get; set; }

        [Action ("ScanPrivateKeyButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ScanPrivateKeyButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("ScanPublicKeyButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ScanPublicKeyButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (sAddressLabel != null) {
                sAddressLabel.Dispose ();
                sAddressLabel = null;
            }

            if (scanPrivateKeyButton != null) {
                scanPrivateKeyButton.Dispose ();
                scanPrivateKeyButton = null;
            }

            if (scanPublicKeyButton != null) {
                scanPublicKeyButton.Dispose ();
                scanPublicKeyButton = null;
            }

            if (selectAddressButton != null) {
                selectAddressButton.Dispose ();
                selectAddressButton = null;
            }

            if (sPrivateKeyTextView != null) {
                sPrivateKeyTextView.Dispose ();
                sPrivateKeyTextView = null;
            }

            if (sPublicKeyTextView != null) {
                sPublicKeyTextView.Dispose ();
                sPublicKeyTextView = null;
            }

            if (urlTextField != null) {
                urlTextField.Dispose ();
                urlTextField = null;
            }
        }
    }
}