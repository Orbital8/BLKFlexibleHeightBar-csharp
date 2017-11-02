using Foundation;
using System;
using BLKFlexibleHeightBar;
using BLKFlexibleHeightBar.Behaviour;
using CoreGraphics;
using Intents;
using UIKit;

namespace Sample
{
    public partial class FacebookStyleTableViewController : UITableViewController
    {
        private DataSource _dataSource;
        private BLKFlexibleHeightBarSubviewUIButton _closeButton;

        public FacebookStyleTableViewController (IntPtr handle) : base (handle)
        {
        }

        public FacebookStyleBar MyCustomBar { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetNeedsStatusBarAppearanceUpdate();

            //Set up the bar
            MyCustomBar = new FacebookStyleBar(new CGRect(0.0, 0.0, View.Frame.Width, 100.0));

            var behaviorDefiner = new FacebookStyleBarBehaviorDefiner();
            behaviorDefiner.AddSnappingPositionProgress(0.0f, 0.0f, 40.0f / (105.0f - 20.0f));
            behaviorDefiner.AddSnappingPositionProgress(1.0f, 40.0f / (105.0f - 20.0f), 1.0f);
            behaviorDefiner.IsSnappingEnabled = true;
            behaviorDefiner.ThresholdNegativeDirection = 140.0f;

            TableView.Delegate = behaviorDefiner;
            MyCustomBar.BehaviorDefiner = behaviorDefiner;

            View.AddSubview(MyCustomBar);

            // Setup the table view
            TableView.ContentInset = new  UIEdgeInsets(MyCustomBar.MaximumBarHeight, 0.0f, 0.0f, 0.0f);
            TableView.Source = _dataSource = new DataSource();

            // Add a close button to the bar
            _closeButton = new BLKFlexibleHeightBarSubviewUIButton
            {
                UserInteractionEnabled = true,
                TintColor = UIColor.White
            };
            _closeButton.SetImage(UIImage.FromFile("Close.png"),UIControlState.Normal);

           var initialCloseButtonLayoutAttributes = new BLKFlexibleHeightBarSubviewLayoutAttributes();
            initialCloseButtonLayoutAttributes.Frame = new CGRect(MyCustomBar.Frame.Size.Width - 35.0, 26.5, 30.0, 30.0);
            initialCloseButtonLayoutAttributes.ZIndex = 1024;

            _closeButton.AddLayoutAttributes(initialCloseButtonLayoutAttributes, 0.0f);
            _closeButton.AddLayoutAttributes(initialCloseButtonLayoutAttributes, 40.0f / (105.0f - 20.0f));
    
            var finalCloseButtonLayoutAttributes = new BLKFlexibleHeightBarSubviewLayoutAttributes(initialCloseButtonLayoutAttributes);
            finalCloseButtonLayoutAttributes.Transform = CGAffineTransform.MakeTranslation(0.0f, -0.3f*(105-20));
            finalCloseButtonLayoutAttributes.Alpha = 0.0f;

            _closeButton.AddLayoutAttributes(finalCloseButtonLayoutAttributes, 0.8f);
            _closeButton.AddLayoutAttributes(finalCloseButtonLayoutAttributes, 1.0f);

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

            public DataSource()
            {
            }


            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return false;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.BackgroundColor = new UIColor(0.95f,0.95f,0.95f,1);


                return cell;
            }

            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return 20;
            }
        }

    }
}