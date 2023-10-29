using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace CodeX.Activities
{
    [Activity(NoHistory = true)]
    public class NotificationActivity : BaseActivity
    {
        private Button loginNowButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.notification);
            WireView();
        }

        protected override void WireView()
        {
            (loginNowButton = FindViewById<Button>(Resource.Id.notification_btn_login)).Click += OnLoginNowClick;
            loginNowButton.RequestFocus();
        }

        protected override void UnwireView()
        {
            loginNowButton.Click -= OnLoginNowClick;
        }

        private void OnLoginNowClick(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}