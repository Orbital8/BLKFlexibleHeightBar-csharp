using System;
using UIKit;

namespace BLKFlexibleHeightBar.Behaviour
{
	public class SquareCashStyleBehaviorDefiner : BLKFlexibleHeightBarBehaviorDefiner
	{
		public override void Scrolled (UIScrollView scrollView)
		{
			if(!IsCurrentlySnapping)
			{
				var progress = (scrollView.ContentOffset.Y+scrollView.ContentInset.Top) / (FlexibleHeightBar.MaximumBarHeight - FlexibleHeightBar.MinimumBarHeight);
				FlexibleHeightBar.Progress = progress;
				FlexibleHeightBar.SetNeedsLayout();
			}
		}
	}

}
