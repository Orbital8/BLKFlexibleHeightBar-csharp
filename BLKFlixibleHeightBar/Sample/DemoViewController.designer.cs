// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Sample
{
    [Register ("DemoViewController")]
    partial class DemoViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton facebookButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton squareCashButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView view { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (facebookButton != null) {
                facebookButton.Dispose ();
                facebookButton = null;
            }

            if (squareCashButton != null) {
                squareCashButton.Dispose ();
                squareCashButton = null;
            }

            if (view != null) {
                view.Dispose ();
                view = null;
            }
        }
    }
}