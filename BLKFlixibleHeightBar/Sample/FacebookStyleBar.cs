using BLKFlexibleHeightBar;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Sample
{
    public class FacebookStyleBar : BLKFlexibleHeightBar.BLKFlexibleHeightBar
    {
        public FacebookStyleBar(CGRect frame) : base(frame)
        {
            Initialise();
        }

        private void Initialise()
        {
            // Configure bar appearence
            MaximumBarHeight = 105.0f;
            MinimumBarHeight = 20.0f;
            BackgroundColor = new UIColor(0.31f, 0.42f, 0.64f, 1);
            ClipsToBounds = true;


            // Add blue bar view
            var blueBarView = new BLKFlexibleHeightBarSubviewUIView {BackgroundColor = BackgroundColor};

            var initialBlueBarLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes
                {
                    Frame = new CGRect(0.0f, 25.0f, Frame.Size.Width, 40.0),
                    ZIndex = 1023
                };
            blueBarView.AddLayoutAttributes(initialBlueBarLayoutAttributes, 0.0f);
            blueBarView.AddLayoutAttributes(initialBlueBarLayoutAttributes, 40.0f / (105.0f - 20.0f));

            var finalBlueBarLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(initialBlueBarLayoutAttributes)
                {
                    Transform = CGAffineTransform.MakeTranslation(0.0f, -40.0f)
                };
            blueBarView.AddLayoutAttributes(finalBlueBarLayoutAttributes, 1.0f);

            AddSubview(blueBarView);

            // Add search field and close button
            var searchField = new BLKFlexibleHeightBarSubviewUITextField();
            searchField.Layer.CornerRadius = 5.0f;
            searchField.AttributedPlaceholder = new NSAttributedString("Search",
                new UIStringAttributes() {ForegroundColor = new UIColor(0.55f, 0.6f, 0.75f, 1)});
            searchField.BackgroundColor = new UIColor(0.22f, 0.27f, 0.46f, 1);
            searchField.Layer.SublayerTransform = CATransform3D.MakeTranslation(5, 0, 0);

            var initialSearchFieldLayoutAttributes = new BLKFlexibleHeightBarSubviewLayoutAttributes
            {
                Frame = new CGRect(8.0f, 25.0f,
                    initialBlueBarLayoutAttributes.Size.Width * 0.85f,
                    initialBlueBarLayoutAttributes.Size.Height - 8.0),
                ZIndex = 1024
            };
            searchField.AddLayoutAttributes(initialSearchFieldLayoutAttributes, 0.0f);
            searchField.AddLayoutAttributes(initialSearchFieldLayoutAttributes, 40.0f / (105.0f - 20.0f));

            var finalSearchFieldLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(initialSearchFieldLayoutAttributes)
                {
                    Transform = CGAffineTransform.MakeTranslation(0.0f, -0.3f * (105f - 20f)),
                    Alpha = 0.0f
                };

            searchField.AddLayoutAttributes(finalSearchFieldLayoutAttributes, 0.8f);
            searchField.AddLayoutAttributes(finalSearchFieldLayoutAttributes, 1.0f);

            AddSubview(searchField);

            // Add white bar view
            var whiteBarView = new BLKFlexibleHeightBarSubviewUIView {BackgroundColor = UIColor.White};

            var initialWhiteBarLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes
                {
                    Frame = new CGRect(0.0f, 65.0f, Frame.Size.Width, 40.0)
                };
            whiteBarView.AddLayoutAttributes(initialSearchFieldLayoutAttributes, 0.0f);

            var finalWhiteBarLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(initialWhiteBarLayoutAttributes)
                {
                    Transform = CGAffineTransform.MakeTranslation(0.0f, -40.0f)
                };
            whiteBarView.AddLayoutAttributes(finalWhiteBarLayoutAttributes, 40.0f / (105.0f - 20.0f));

            AddSubview(whiteBarView);

            // Configure white bar view subviews
            var bottomBorderView = new UIView(new CGRect(0.0f, initialWhiteBarLayoutAttributes.Size.Height - 0.5,
                initialWhiteBarLayoutAttributes.Size.Width, 0.5))
            {
                BackgroundColor = new UIColor(0.75f, 0.76f, 0.78f, 1)
            };
            whiteBarView.AddSubview(bottomBorderView);

            var leftVerticalDividerView = new UIView(new CGRect(initialWhiteBarLayoutAttributes.Size.Width * 0.334,
                initialWhiteBarLayoutAttributes.Size.Height * 0.1, 0.5,
                initialWhiteBarLayoutAttributes.Size.Height * 0.8))
            {
                BackgroundColor = new UIColor(0.85f, 0.86f, 0.88f, 1)
            };
            whiteBarView.AddSubview(leftVerticalDividerView);

            var rightVerticalDividerView = new UIView(new CGRect(initialWhiteBarLayoutAttributes.Size.Width * 0.667,
                initialWhiteBarLayoutAttributes.Size.Height * 0.1, 0.5,
                initialWhiteBarLayoutAttributes.Size.Height * 0.8))
            {
                BackgroundColor = new UIColor(0.85f, 0.86f, 0.88f, 1)
            };
            whiteBarView.AddSubview(rightVerticalDividerView);
        }
    }
}