// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CRMSoapAuthXamarin
{
	[Register ("RootViewController")]
	partial class RootViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField CrmUrl { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton Login { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Username { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel WhoAmI { get; set; }

		[Action ("Login_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Login_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (CrmUrl != null) {
				CrmUrl.Dispose ();
				CrmUrl = null;
			}
			if (Login != null) {
				Login.Dispose ();
				Login = null;
			}
			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}
			if (WhoAmI != null) {
				WhoAmI.Dispose ();
				WhoAmI = null;
			}
		}
	}
}
