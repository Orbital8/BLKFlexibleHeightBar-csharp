using System;
using BLKFlexibleHeightBar;
using BLKFlexibleHeightBar.Behaviour;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Sample
{
    public partial class SquareCashStyleTableViewController : UITableViewController
    {
        private UIButton _closeButton;
        private DataSource _dataSource;

        public SquareCashStyleTableViewController(IntPtr handle) : base(handle)
        {
        }

        public SquareCashStyleBar MyCustomBar { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetNeedsStatusBarAppearanceUpdate();

            //Set up the bar
            MyCustomBar = new SquareCashStyleBar(new CGRect(0.0, 0.0, View.Frame.Width, 100.0));

            var behaviorDefiner = new SquareCashStyleBehaviorDefiner();
            behaviorDefiner.AddSnappingPositionProgress(0.0f, 0.0f, 0.5f);
            behaviorDefiner.AddSnappingPositionProgress(1.0f, 0.5f, 1.0f);
            behaviorDefiner.IsSnappingEnabled = true;
            behaviorDefiner.IsElasticMaximumHeightAtTop = true;
            MyCustomBar.BehaviorDefiner = behaviorDefiner;

            View.AddSubview(MyCustomBar);

            // Setup the table view
            TableView.ContentInset = new UIEdgeInsets(MyCustomBar.MaximumBarHeight, 0.0f, 0.0f, 0.0f);
            TableView.Source = _dataSource = new DataSource();

            // Add close button - it's pinned to the top right corner, so it doesn't need to respond to bar height changes
            _closeButton = new BLKFlexibleHeightBarSubviewUIButton
            {
                Frame = new CGRect(MyCustomBar.Frame.Size.Width - 40.0, 25.0, 30.0, 30.0),
                TintColor = UIColor.White
            };
            _closeButton.SetImage(UIImage.FromFile("Close.png"), UIControlState.Normal);
            MyCustomBar.AddSubview(_closeButton);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _closeButton.TouchUpInside += OnCloseButtonClick;
        }

        public override void ViewWillDisappear(bool animated)
        {
            _closeButton.TouchUpInside -= OnCloseButtonClick;
            base.ViewWillDisappear(animated);
        }

        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            DismissModalViewController(true);
        }

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return false;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.BackgroundColor = new UIColor(0.95f, 0.95f, 0.95f, 1);


                return cell;
            }

            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return 100;
            }
        }
    }
}