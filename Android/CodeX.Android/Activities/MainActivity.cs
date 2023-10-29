using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using CodeX.Controls;
using CodeX.Core.Models;
using CodeX.Core.Services;
using System;
using System.Threading.Tasks;

namespace CodeX.Activities
{
    [Activity(Icon = "@drawable/icon", NoHistory = false)]
    public class MainActivity : BaseActivity
    {
        private TextView Username;
        private TextView Password;

        private Button SignInButton;
        private Button NewUserButton;

        static MainActivity()
        {
            // Establish database location
            CzSqliteDbContext.DatabaseDirectory = Xamarin.Essentials.FileSystem.AppDataDirectory;

            // Initialize database engine (so the first end-user interaction won't take the hit)
            Task.Run(() => _ = CzRepository.Instance.GetRecordCountAsync<CzAccount>());
        }

        #region --[ Activity Lifecycle ]--

        /// <summary>
        /// The first method to be called when an activity is created.
        /// OnCreate is always overridden to perform any startup initializations that may be required by an Activity such as:
        ///  * Creating views
        ///  * Initializing variables
        ///  * Binding static data to lists
        /// </summary>
        /// <param name="savedInstanceState">
        /// Dictionary for storing and passing state information and objects between activities.
        /// If the bundle is not null, this indicates the activity is restarting and
        /// it should restore its state from the previous instance.
        /// </param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Complex and/or expensive is persisted by overriding OnRetainNonConfigurationInstance, and
            // retrieved on demand by reference to LastNonConfigurationInstance.
            _ = LastNonConfigurationInstance;

            SetContentView(Resource.Layout.main);
            WireView();
        }

        /// <summary>
        /// OnStart is always called by the system after OnCreate is finished.
        /// Activities may override this method if they need to perform any specific tasks right before
        /// an activity becomes visible such as refreshing current values of views within the activity.
        /// Android will call OnResume immediately after this method.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
        }

        /// <summary>
        /// The system calls OnResume when the Activity is ready to start interacting with the user.
        /// Activities should override this method to perform tasks such as:
        ///  * Ramping up frame rates(a common task in game development)
        ///  * Starting animations
        ///  * Listening for GPS updates
        ///  * Display any relevant alerts or dialogs
        ///  * Wire up external event handlers
        /// OnResume is important because any operation that is done in OnPause should be un-done in OnResume,
        /// since it's the only lifecycle method that is guaranteed to execute after OnPause when bringing the
        /// activity back to life.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// OnPause is called when the system is about to put the activity into the background or
        /// when the activity becomes partially obscured.
        /// Activities should override this method if they need to:
        ///  * Commit unsaved changes to persistent data
        ///  * Destroy or clean up other objects consuming resources
        ///  * Ramp down frame rates and pausing animations
        ///  * Unregister external event handlers or notification handlers (those that are tied to a service).
        ///    This must be done to prevent Activity memory leaks.
        ///  * Likewise, if the Activity has displayed any dialogs or alerts, they must be cleaned up with the
        ///    .Dismiss() method.
        /// There are two possible lifecycle methods that will be called after OnPause:
        ///  1) OnResume will be called if the Activity is to be returned to the foreground.
        ///  2) OnStop will be called if the Activity is being placed in the background.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>
        /// OnStop is called when the activity is no longer visible to the user. This happens when one of the following occurs:
        ///  * A new activity is being started and is covering up this activity.
        ///  * An existing activity is being brought to the foreground.
        ///  * The activity is being destroyed.
        /// OnStop may not always be called in low-memory situations,
        /// such as when Android is starved for resources and cannot properly background the Activity.For this reason,
        /// it is best not to rely on OnStop getting called when preparing an Activity for destruction.
        /// The next lifecycle methods that may be called after this one will be OnDestroy if the Activity is going away,
        /// or OnRestart if the Activity is coming back to interact with the user.
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
        }

        /// <summary>
        /// OnDestroy is the final method that is called on an Activity instance
        /// before it's destroyed and completely removed from memory.
        /// In extreme situations Android may kill the application process that is hosting the Activity,
        /// which will result in OnDestroy not being invoked.
        /// Most Activities will not implement this method because most clean up and shut down has been done
        /// in the OnPause and OnStop methods. The OnDestroy method is typically overridden to clean up long
        /// running tasks that might leak resources.
        /// An example of this might be background threads that were started in OnCreate.
        /// There will be no lifecycle methods called after the Activity has been destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <summary>
        /// OnRestart is called after your activity has been stopped, prior to it being started again.
        /// A good example of this would be when the user presses the home button while on an activity in the application.
        /// When this happens OnPause and then OnStop methods are called, and the Activity is moved to
        /// the background but is not destroyed. If the user were then to restore the application by 
        /// using the task manager or a similar application, Android will call the OnRestart method of the activity.
        /// There are no general guidelines for what kind of logic should be implemented in OnRestart.
        /// This is because OnStart is always invoked regardless of whether the Activity is being created or being restarted,
        /// so any resources required by the Activity should be initialized in OnStart, rather than OnRestart.
        /// The next lifecycle method called after OnRestart will be OnStart.
        /// </summary>
        protected override void OnRestart()
        {
            base.OnRestart();
        }

        #endregion

        #region --[ Bundle State ]--

        /// <summary>
        /// OnSaveInstanceState will be called as the Activity is being stopped.
        /// It will receive a bundle parameter that the Activity can store its state in.
        /// When a device experiences a configuration change, an Activity can use
        /// the Bundle object that is passed in to preserve the Activity state by
        /// overriding OnSaveInstanceState.
        /// 
        /// The default implementation of OnSaveInstanceState will
        /// take care of saving transient data in the UI for every view,
        /// so long as each view has an ID assigned.
        /// </summary>
        /// <param name="outState">Activity Bundle</param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            // Not designed for large objects, such as images.
            // Use OnRetainNonConfigurationInstance for persisting larger objects.
            base.OnSaveInstanceState(outState);
        }

        /// <summary>
        /// OnRestoreInstanceState will be called after OnStart.
        /// It provides an activity the opportunity to restore any state that was previously saved
        /// to a Bundle during the previous OnSaveInstanceState.
        /// This is the same bundle that is provided to OnCreate, however.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
        }

        #endregion

        #region --[ Application Code ]--

        protected override void WireView()
        {
            (Username = FindViewById<TextView>(Resource.Id.sign_in_txt_username)).TextChanged += OnTextChanged;
            (Password = FindViewById<TextView>(Resource.Id.sign_in_txt_password)).TextChanged += OnTextChanged;

            (SignInButton = FindViewById<Button>(Resource.Id.sign_in_btn_sign_in)).Click += OnSignInClick;
            (NewUserButton = FindViewById<Button>(Resource.Id.sign_in_btn_new_user)).Click += OnNewUserClick;

            Username.RequestFocus();
        }

        protected override void UnwireView()
        {
            Username.TextChanged -= OnTextChanged;
            Password.TextChanged -= OnTextChanged;

            SignInButton.Click -= OnSignInClick;
            NewUserButton.Click -= OnNewUserClick;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SignInButton.Enabled = (Username.Length() > 0) && (Password.Length() > 0);
        }

        private async void OnSignInClick(object sender, EventArgs e)
        {
            try
            {
                _ = await AccountClerk.Login(Username.Text, Password.Text);

                var dialog = new CzDialog(
                    this,
                    CodeX.Core.Properties.Resources.all_application_name,
                    "Congradulations, you have successfully logged in!");

                dialog.OnOkay += Finish;
                dialog.Show();
            }
            catch (System.Exception ex)
            {
                var dialog = new CzDialog(
                    this,
                    CodeX.Core.Properties.Resources.all_application_name,
                    ex.Message);

                dialog.Show();

                Username.RequestFocus();
            }
        }

        private void Finish(object sender, EventArgs e)
        {
            // Don't give the end-user the impression that the application
            // crashed, by terminating the application... even though
            // there isn't anything more to do.
            Password.Text = string.Empty;
            Username.Text = string.Empty;
            Username.RequestFocus();
        }

        private void OnNewUserClick(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountCreateActivity));
        }

        #endregion
    }
}