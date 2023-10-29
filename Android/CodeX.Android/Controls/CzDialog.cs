using Android.App;
using Android.Content;
using System;

namespace CodeX.Controls
{
    public class CzDialog
    {
        public string Title { get; set; }
        public string Message { get; set; }

        protected Context Owner { get; }

        private AlertDialog _alertDialog = null;

        public event EventHandler<DialogClickEventArgs> OnOkay = null;

        public event EventHandler<DialogClickEventArgs> OnCancel = null;

        #region --[ Ctor/Dtor ]--

        public CzDialog(object owner) : this(owner, string.Empty, string.Empty) { }

        public CzDialog(object owner, string title, string message)
        {
            Owner = (owner as Context);
            Title = title;
            Message = message;
        }

        ~CzDialog()
        {
            _alertDialog?.Dismiss();
            _alertDialog?.Dispose();
        }

        #endregion

        public void Show()
        {
            var builder = new AlertDialog.Builder(Owner);

            _alertDialog = builder.Create();
            _alertDialog.SetTitle(Title);
            _alertDialog.SetIcon(Resource.Drawable.Icon);
            _alertDialog.SetMessage(Message);

            if (OnOkay == null)
            {
                OnOkay = (s, e) => { };
            }
            _alertDialog.SetButton(CodeX.Core.Properties.Resources.dialog_ok, OnOkay);

            if (OnCancel != null)
            {
                _alertDialog.SetButton(CodeX.Core.Properties.Resources.dialog_ok, OnCancel);
            }
            _alertDialog.Show();
        }
    }
}