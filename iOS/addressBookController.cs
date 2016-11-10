using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace SJCoinWallet.iOS
{
    public partial class addressBookController : UITableViewController
    {
        public List<String> accounts { get; set; }
		public bool isSelectMode { get; set; }
		public String selectedAccount { get; set; }
		public event EventHandler ValueChanged;

		static NSString addressBookCellId = new NSString("AddressBookCell");

		public addressBookController(IntPtr handle) : base (handle)
        {
			TableView.RegisterClassForCellReuse(typeof(UITableViewCell), addressBookCellId);
			TableView.Source = new CallHistoryDataSource(this);
			accounts = new List<string>();
			isSelectMode = false;
			selectedAccount = "";
		}

		class CallHistoryDataSource : UITableViewSource
		{
			addressBookController controller;

			public CallHistoryDataSource(addressBookController controller)
			{
				this.controller = controller;
			}

			// Returns the number of rows in each section of the table
			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return controller.accounts.Count;
			}
			//
			// Returns a table cell for the row indicated by row property of the NSIndexPath
			// This method is called multiple times to populate each row of the table.
			// The method automatically uses cells that have scrolled off the screen or creates new ones as necessary.
			//
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(addressBookController.addressBookCellId);

				int row = indexPath.Row;
				cell.TextLabel.Text = controller.accounts[row];
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				controller.selectedAccount = controller.accounts[indexPath.Row];
				if (controller.isSelectMode)
				{
					tableView.DeselectRow(indexPath, true);
					//controller.DismissViewController(true, null);
					controller.NavigationController.PopViewController(true);
					if (controller.ValueChanged != null) 
					{
						controller.ValueChanged(this, EventArgs.Empty);
					};
				}
				else
				{
					UIAlertController okAlertController =
						UIAlertController.Create(
							"Address Selected",
							controller.accounts[indexPath.Row],
							UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
					controller.PresentViewController(okAlertController, true, null);					
				}
			}
		}
    }
}