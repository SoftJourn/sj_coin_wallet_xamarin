using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZXing;
using SJCoinWallet.Helpers;

namespace SJCoinWallet.iOS
{
    public partial class SettingsViewController : UIViewController
    {
		private addressBookController abContoller;
		public List<string> accounts { get; set; }
		public event EventHandler SettingsChanged;
		public bool IsDirty { get; set; }

		public string ServiceEndpoint 
		{ 
			get
			{
				return urlTextField.Text;
			}
		}

		public string MyAddress 
		{ 
			get
			{
				return sAddressLabel.Text;
			}
		}

		public string MyPubKey 
		{ 
			get
			{
				return sPublicKeyTextView.Text;
			}
		}

		public string MyPrivKey 
		{ 
			get
			{
				return sPrivateKeyTextView.Text;
			}
		}

        public SettingsViewController (IntPtr handle) : base (handle)
        {
			urlTextField.ValueChanged += OnUrlTextFieldValueChanged;
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			PopulateSettings();

			IsDirty = false;
		}

		public override void ViewWillDisappear(bool animated)
		{
			if (IsDirty && SettingsChanged != null)
			{
				SettingsChanged(this, null);
				IsDirty = false;
			}
			base.ViewWillDisappear(animated);
		}

		public void PopulateSettings()
		{
			urlTextField.Text = Settings.ServiceEndpoint;
			sAddressLabel.Text = Settings.MyAddress;
			sPublicKeyTextView.Text = Settings.MyPubKey;
			sPrivateKeyTextView.Text = Settings.MyPrivKey;
		}

		async partial void ScanPublicKeyButton_TouchUpInside(UIButton sender)
		{
			string result = await ScanCode();
			if (string.IsNullOrEmpty(result))
			{
				showAlert("Error", "Please scan public key QR code");
				return;
			}
			sPublicKeyTextView.Text = result;
			Settings.MyPubKey = result;
			IsDirty = true;
		}

		async partial void ScanPrivateKeyButton_TouchUpInside(UIButton sender)
		{
			string result = await ScanCode();
			if (string.IsNullOrEmpty(result))
			{
				showAlert("Error", "Please scan private key QR code");
				return;
			}
			sPrivateKeyTextView.Text = result;
			Settings.MyPrivKey = result;
			IsDirty = true;
		}

		async private Task<string> ScanCode()
		{
			var code = "";
			var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
			options.PossibleFormats = new List<BarcodeFormat> {
					BarcodeFormat.QR_CODE,
					BarcodeFormat.AZTEC
			};
			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan(options, true);

			if (result != null)
			{
				System.Diagnostics.Debug.WriteLine("Scanned Barcode: " + result.Text);
				code = result.Text;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Error: Failed to scan");
			}	
			return code;	
		}

		void OnUrlTextFieldValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(urlTextField.Text))
			{
				showAlert("Error", "Please enter URL");
			}
			Settings.ServiceEndpoint = urlTextField.Text; 
			IsDirty = true;
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (string.IsNullOrEmpty(urlTextField.Text))
			{
				showAlert("Error", "Please enter URL");
				return;
			}
			if (accounts != null && accounts.Count > 0)
			{
				accounts.Clear();
			}
			var al = new AccountsLoader(urlTextField.Text);
			accounts = al.LoadAccounts();

			abContoller = segue.DestinationViewController as addressBookController;
			if (abContoller != null)
			{
				abContoller.accounts = accounts;
				abContoller.isSelectMode = true;
				abContoller.ValueChanged += SettingsAddressChanged;
			}
		}

		private void SettingsAddressChanged(object sender, EventArgs e)
		{
			abContoller.ValueChanged -= SettingsAddressChanged;
			var address = abContoller.selectedAccount;
			if (address == null || address.Length < 10)
			{
				showAlert("Error", "Please select address");
				return;
			}
			sAddressLabel.Text = address;
			Settings.MyAddress = address;
			IsDirty = true;
		}

		public void showAlert(string title, string body)
		{
			UIAlertController okAlertController =
				UIAlertController.Create(
					title,
					body,
					UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
			PresentViewController(okAlertController, true, null);
		}
    }
}