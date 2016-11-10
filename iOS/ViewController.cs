using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using SJCoinWallet.Helpers;

namespace SJCoinWallet.iOS
{
	public partial class ViewController : UIViewController
	{
		private addressBookController abContoller;
		private SettingsViewController sController;
		public List<string> accounts { get; set; }
		public long balance { get; set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			accounts = new List<string>();
			balance = 0;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			refreshButton.Enabled = false;
			refreshButton.Hidden = true;
			sendcoinsButton.Enabled = false;
			sendcoinsButton.Hidden = true;

			addressLabel.Text = Settings.MyAddress;

			renderQRCode(Settings.MyAddress);

			if (!string.IsNullOrEmpty(Settings.MyAddress))
			{
				refreshRequest(this, null);
			}
			checkControls(Settings.MyAddress, Settings.MyPrivKey);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (accounts == null || accounts.Count < 1)
			{
				var al = new AccountsLoader(Settings.ServiceEndpoint);
				accounts = al.LoadAccounts();
			}

			try
			{
				abContoller = segue.DestinationViewController as addressBookController;
				abContoller.accounts = accounts;
				if (sender == sendcoinsButton)
				{
					abContoller.isSelectMode = true;
					abContoller.ValueChanged += SendCoidAddressChanged;
				}
			}
			catch (Exception) { }

			try
			{
				if (sender == settingsButton)
				{
					sController = segue.DestinationViewController as SettingsViewController;
					sController.accounts = accounts;
					sController.SettingsChanged += SettingsChanged;
				}
			}
			catch (Exception) { }
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

		partial void AddressBookButton_TouchUpInside(UIButton sender)
		{
			/*
            addressBookController ab = this.Storyboard.InstantiateViewController("addressBookController") as addressBookController;
			if (ab != null)
			{
				ab.accounts = accounts;
				this.NavigationController.PushViewController(ab, true);
			}
			*/
		}

		partial void RefreshButton_TouchUpInside(UIButton sender)
		{
			refreshRequest(sender, null);
		}

		partial void SendcoinsButton_TouchUpInside(UIButton sender)
		{
			sendcoinsRequest(sender, null);
		}

		private void refreshRequest(object sender, EventArgs e)
		{
			var c = new SJCoinContract(
				Settings.ServiceEndpoint,
				Settings.MyAddress, 
				Settings.MyPubKey, 
				Settings.MyPrivKey);
			balance = c.QueryBalance();
			System.Diagnostics.Debug.WriteLine(balance);

			balanceLabel.Text = balance.ToString();
		}

		private void sendcoinsRequest(object sender, EventArgs e)
		{
			/*
			AccountsLoader al = new AccountsLoader(serviceEndpoint);
			accounts = al.LoadAccounts();
			*/
			if (abContoller != null)
			{
				if (balance < 1)
				{
					refreshRequest(sender, e);
				}
				abContoller.accounts = accounts;
				abContoller.isSelectMode = true;
				//this.NavigationController.PushViewController(abContoller, true);
			}
		}

		private void SettingsChanged(Object sender, EventArgs e)
		{
			var settings = sender as SettingsViewController;
			Settings.ServiceEndpoint = settings.ServiceEndpoint;
			Settings.MyAddress = settings.MyAddress;
			Settings.MyPubKey = settings.MyPubKey;
			Settings.MyPrivKey = settings.MyPrivKey;

			checkControls(settings.MyAddress, settings.MyPrivKey);
			addressLabel.Text = settings.MyAddress;
			renderQRCode(settings.MyAddress);
			sController.SettingsChanged -= SettingsChanged;
		}
					
		private void SendCoidAddressChanged(object sender, EventArgs e)
		{
			abContoller.ValueChanged -= SendCoidAddressChanged;
			var address = abContoller.selectedAccount;
			if (address == null || address.Length < 10)
			{
				showAlert("Error","Please select address to send coins to");
			}
			else
			{
				var alert = new UIAlertView();
				alert.Title = address;
				alert.AddButton("Send");
				alert.AddButton("Cancel");
				alert.Message = "Please Enter amount.";
				alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
				alert.Clicked += (object s, UIButtonEventArgs ev) =>
				{
					if (ev.ButtonIndex == 0)
					{
						long amount = long.Parse(alert.GetTextField(0).Text);
						if (amount > 0 && amount <= balance)
						{
							var c = new SJCoinContract(
								Settings.ServiceEndpoint, 
								Settings.MyAddress, 
								Settings.MyPubKey,
								Settings.MyPrivKey);
							string txId = c.Send(address, amount);
							if (string.IsNullOrEmpty(txId))
							{
								showAlert("Error", "Transaction failed");
							}
							else
							{
								showAlert("Transaction Posted", txId);
							}
						}
						else
						{
							showAlert("Error", "Invalid amount");
						}
					}
				};
				alert.Show();
			}
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

		private void renderQRCode(string what)
		{
			string html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
	"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" +
	"<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n    " +
	"<title>QR Code</title>\n</head>\n<body style=\"padding:0;margin:0;background:grey\">\n    " +
	"<div style=\"width: 200px; height: 200px; background: red;\">\n        " +
	"<img src='https://chart.googleapis.com/chart?cht=qr&chl=\"" +
				(string.IsNullOrEmpty(what) ? "Value is not set" : what) +
	"\"&chs=200x200' width='200' height='200' border='0' style=\"padding:0;margin:0;\" />" +
	"\n</div>\n</body>\n</html>";
			qrcodeView.LoadHtmlString(html, null);
		}

		private void checkControls(string MyAddress, string MyPrivKey)
		{
			if (!string.IsNullOrEmpty(MyAddress))
			{
				refreshButton.Enabled = true;
				refreshButton.Hidden = false;
				if (!string.IsNullOrEmpty(MyPrivKey))
				{
					sendcoinsButton.Enabled = true;
					sendcoinsButton.Hidden = false;
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Key=" + MyPrivKey);
				}
			}
		}
	}
}
