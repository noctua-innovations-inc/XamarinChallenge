using Android.App;
using AndroidX.AppCompat.App;
using CodeX.Core.Contracts;
using CodeX.Core.Services;

namespace CodeX.Activities
{
    public abstract class BaseActivity : AppCompatActivity
    {
        public BaseActivity()
        {
            AccountClerk = CzClientAccountClerk.Instance;
        }

        protected ICzClientAccountClerk AccountClerk { get; }

        protected override void OnDestroy()
        {
            UnwireView();
            base.OnDestroy();
        }

        // Wire up view to implementation.
        protected abstract void WireView();

        // Unwire view from implemenation.
        protected abstract void UnwireView();
    }
}