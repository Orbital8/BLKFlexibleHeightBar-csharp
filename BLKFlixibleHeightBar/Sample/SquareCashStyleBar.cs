using BLKFlexibleHeightBar;
using CoreGraphics;
using UIKit;

namespace Sample
{
    public class SquareCashStyleBar : BLKFlexibleHeightBar.BLKFlexibleHeightBar
    {
        public SquareCashStyleBar(CGRect frame) : base(frame)
        {
            Initialise();
        }

        private void Initialise()
        {
            // Configure bar appearence
            MaximumBarHeight = 200.0f;
            MinimumBarHeight = 65.0f;
            BackgroundColor = new UIColor(0.17f, 0.63f, 0.11f, 1);
            //ClipsToBounds = true;


            // Add and configure name label
            var nameLabel = new BLKFlexibleHeightBarSubviewUILabel
            {
                BackgroundColor = BackgroundColor,
                Font = UIFont.SystemFontOfSize(22.0f),
                TextColor = UIColor.White,
                Text = "Bryan Keller"
            };

            var initialNameLabelLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes
                {
                    Size = nameLabel.SizeThatFits(CGSize.Empty),
                    Center = new CGPoint(Frame.Size.Width * 0.5, MaximumBarHeight - 50.0f)
                };
            nameLabel.AddLayoutAttributes(initialNameLabelLayoutAttributes, 0.0f);

            var midwayNameLabelLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(initialNameLabelLayoutAttributes)
                {
                    Center = new CGPoint(Frame.Size.Width * 0.5,
                        (MaximumBarHeight - MinimumBarHeight) * 0.4 + MinimumBarHeight - 50.0)
                };
            nameLabel.AddLayoutAttributes(midwayNameLabelLayoutAttributes, 0.6f);


            var finalNameLabelLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(midwayNameLabelLayoutAttributes)
                {
                    Center = new CGPoint(Frame.Size.Width * 0.5, MinimumBarHeight - 25.0)
                };
            nameLabel.AddLayoutAttributes(finalNameLabelLayoutAttributes, 1.0f);

            AddSubview(nameLabel);

            // Add and configure profile image
            var profileImageView =
                new BLKFlexibleHeightBarSubviewUIImageView
                {
                    Image = UIImage.FromFile("ProfilePicture.png")
                }; 
            profileImageView.SizeToFit();
            profileImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            profileImageView.ClipsToBounds = true;
            profileImageView.Layer.CornerRadius = 35.0f;
            profileImageView.Layer.BorderWidth = 2.0f;
            profileImageView.Layer.BorderColor = UIColor.White.CGColor;

            var initialProfileImageViewLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes
                {
                    Size = new CGSize(70.0, 70.0),
                    Center = new CGPoint(Frame.Size.Width * 0.5, MaximumBarHeight - 110.0)
                };
            profileImageView.AddLayoutAttributes(initialProfileImageViewLayoutAttributes, 0.0f);

            var midwayProfileImageViewLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(initialProfileImageViewLayoutAttributes)
                {
                    Center = new CGPoint(Frame.Size.Width * 0.5,
                        (MaximumBarHeight - MinimumBarHeight) * 0.8 + MinimumBarHeight - 110.0)
                };
            profileImageView.AddLayoutAttributes(midwayProfileImageViewLayoutAttributes, 0.2f);

            var finalProfileImageViewLayoutAttributes =
                new BLKFlexibleHeightBarSubviewLayoutAttributes(midwayProfileImageViewLayoutAttributes)
                {
                    Center = new CGPoint(Frame.Size.Width * 0.5,
                        (MaximumBarHeight - MinimumBarHeight) * 0.64 + MinimumBarHeight - 110.0),
                    Transform = CGAffineTransform.MakeScale(0.5f, 0.5f),
                    Alpha = 0.0f
                };
            profileImageView.AddLayoutAttributes(finalProfileImageViewLayoutAttributes, 0.5f);

            AddSubview(profileImageView);
        }
    }
}