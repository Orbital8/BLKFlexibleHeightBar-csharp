using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;
using UIKit;

namespace BLKFlexibleHeightBar.Behaviour
{
	public class BLKFlexibleHeightBarBehaviorDefiner : UIScrollViewDelegate, IUITableViewDelegate
	{
		private Dictionary<Range, float> _snappingPositionsForProgressRanges;

		public BLKFlexibleHeightBarBehaviorDefiner()
		{
			_snappingPositionsForProgressRanges = new Dictionary<Range, float>();
			IsSnappingEnabled = true;
			IsCurrentlySnapping = false;
			IsElasticMaximumHeightAtTop = false;
		}

		public BLKFlexibleHeightBar FlexibleHeightBar { get; set; }

		public bool IsSnappingEnabled { get; set; }

		public bool IsCurrentlySnapping { get; private set; }

		public bool IsElasticMaximumHeightAtTop { get; set; }

		public void AddSnappingPositionProgress(float progress, float start, float end)
		{
			// Make sure start and end are between 0 and 1
			start = (Math.Max (Math.Min (start, 1.0f), 0.0f) * 100.0f);
			end = (Math.Max (Math.Min (end, 1.0f), 0.0f) * 100.0f);
			var progressPercentRange = new Range(start, end-start);
		
			foreach(var existingRange in _snappingPositionsForProgressRanges.Keys)
			{
				bool noRangeConflict = (progressPercentRange.Intersection(existingRange).Length == 0);
				Debug.Assert(noRangeConflict, @"progressPercentRange sent to -addSnappingProgressPosition:forProgressPercentRange: intersects a progressPercentRange for an existing progressPosition.");
			}

			_snappingPositionsForProgressRanges.Add (progressPercentRange, progress);
		}

		public void RemoveSnappingPositionProgressForProgressRangeStart(float start, float end)
		{
			// Make sure start and end are between 0 and 1
			start = (Math.Max (Math.Min (start, 1.0f), 0.0f) * 100.0f);
			end = (Math.Max (Math.Min (end, 1.0f), 0.0f) * 100.0f);
			var progressPercentRange = new Range(start, end-start);

			_snappingPositionsForProgressRanges.Remove (progressPercentRange);
		}

		public void SnapToProgress(float progress, UIScrollView scrollView)
		{
			var deltaProgress = progress - FlexibleHeightBar.Progress;
			var deltaYOffset = (FlexibleHeightBar.MaximumBarHeight-FlexibleHeightBar.MinimumBarHeight) * deltaProgress;
			scrollView.ContentOffset = new CGPoint(scrollView.ContentOffset.X, scrollView.ContentOffset.Y+deltaYOffset);

			FlexibleHeightBar.Progress = progress;
			FlexibleHeightBar.SetNeedsLayout ();
			FlexibleHeightBar.LayoutIfNeeded ();

			IsCurrentlySnapping = false;
		}

		public void SnapWithScrollView(UIScrollView scrollView)
		{
			if(!IsCurrentlySnapping && IsSnappingEnabled && FlexibleHeightBar.Progress >= 0.0)
			{
				IsCurrentlySnapping = true;

				var snapPosition = float.MaxValue;
				foreach(var pair in _snappingPositionsForProgressRanges)
				{
					var existingRange = pair.Key;
					var progressPercent = FlexibleHeightBar.Progress * 100.0f;

					if(progressPercent >= existingRange.Location && (progressPercent <= (existingRange.Location+existingRange.Length)))
					{
						snapPosition = pair.Value;
					}

				}

				if(snapPosition != float.MaxValue)
				{
					UIView.Animate (0.15, delegate
					{

						SnapToProgress (snapPosition, scrollView);

					}, delegate
					{

						IsCurrentlySnapping = false;
					});
				}
				else
				{
					IsCurrentlySnapping = false;
				}
			}
		}

		public override void DecelerationEnded(UIScrollView scrollView)
		{
			SnapWithScrollView(scrollView);
		}

		public override void DraggingEnded(UIScrollView scrollView, bool willDecelerate)
		{
			if (!willDecelerate)
			{
				SnapWithScrollView(scrollView);
			}
		}
	}

}
