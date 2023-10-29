using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using CodeX.Controls;
using CodeX.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace CodeX.Activities
{
    [Activity(NoHistory = true)]
    public class AccountCreateActivity : BaseActivity
    {
        private TextView TextNameFirst;
        private TextView TextNameLast;
        private TextView TextNameUser;
        private TextView TextPassword;
        private TextView TextPhoneNumber;
        private TextView TextServiceStart;

        private Button createAccountButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_create);
            WireView();
        }

        protected override void WireView()
        {
            (createAccountButton = FindViewById<Button>(Resource.Id.account_create_btn_createaccount)).Click += OnCreateAccountClick;

            (TextNameFirst = FindViewById<TextView>(Resource.Id.account_create_txt_firstname)).TextChanged += OnTextChanged;
            (TextNameLast = FindViewById<TextView>(Resource.Id.account_create_txt_lastname)).TextChanged += OnTextChanged;
            (TextNameUser = FindViewById<TextView>(Resource.Id.account_create_txt_username)).TextChanged += OnTextChanged;
            (TextPassword = FindViewById<TextView>(Resource.Id.account_create_txt_password)).TextChanged += OnTextChanged;
            (TextPhoneNumber = FindViewById<TextView>(Resource.Id.account_create_txt_phonenumber)).TextChanged += OnTextChanged;
            (TextServiceStart = FindViewById<TextView>(Resource.Id.account_create_txt_servicestartdate)).TextChanged += OnTextChanged;

            TextServiceStart.FocusChange += TextServiceStartOnFocusChange;

            TextServiceStart.Text = DateTime.Now.ToShortDateString();

            TextNameFirst.RequestFocus();
        }

        protected override void UnwireView()
        {
            TextNameFirst.TextChanged -= OnTextChanged;
            TextNameLast.TextChanged -= OnTextChanged;
            TextNameUser.TextChanged -= OnTextChanged;
            TextPassword.TextChanged -= OnTextChanged;
            TextPhoneNumber.TextChanged -= OnTextChanged;
            TextServiceStart.TextChanged -= OnTextChanged;

            createAccountButton.Click -= OnCreateAccountClick;
        }

        private void TextServiceStartOnFocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                if (! DateTime.TryParse(TextServiceStart.Text, out var date))
                {
                    date = DateTime.Now;
                }
                using (var dpd = new DatePickerDialog(this, 0, OnSetServiceStartDate, date.Year, (date.Month - 1), date.Day))
                {
                    dpd.Show();
                }
            }
        }

        private void OnSetServiceStartDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            try
            {
                var dt = new DateTime(e.Year, (e.Month + 1), e.DayOfMonth);
                TextServiceStart.Text = dt.ToShortDateString();
            }
            catch { }
        }

        private async void OnCreateAccountClick(object sender, EventArgs e)
        {
            try
            {
                if (await CreateAccount())
                {
                    StartActivity(typeof(NotificationActivity));
                }
            }
            catch
            {
                // Don't let uncaught exception bubble from async void!
            }
        }

        private async Task<bool> CreateAccount()
        {
            var result = false;
            var dto = ViewToDto();
            if (ValidateAccountInfo(dto))
            {
                try
                {
                    result = await AccountClerk.CreateAccount(dto);
                }
                catch (Exception ex)
                {
                    var dialog = new CzDialog(
                        this,
                        CodeX.Core.Properties.Resources.all_application_name,
                        ex.Message);

                    dialog.Show();
                }
            }
            return result;
        }

        private bool ValidateAccountInfo(CzAccount info)
        {
            // TODO Provide nicer error notification
            // TextNameFirst.SetError("Some error", null);

            var validationContext = new ValidationContext(info, null, null);
            var validationResult = new List<ValidationResult>();

            var isGood = Validator.TryValidateObject(info, validationContext, validationResult, true);

            if (!isGood)
            {
                var message = new StringBuilder();
                message.AppendLine("Data validation errors: \r\n");
                foreach (var error in validationResult)
                {
                    message.AppendLine($" * {error.ErrorMessage}");
                }

                var dialog = new CzDialog(
                    this,
                    CodeX.Core.Properties.Resources.all_application_name,
                    message.ToString());

                dialog.Show();
            }
            return isGood;
        }

        private CzAccount ViewToDto()
        {
            if (!DateTime.TryParse(TextServiceStart.Text, out var serviceDateStart))
            {
                serviceDateStart = DateTime.MinValue;
            }

            var result = new CzAccount()
            {
                NameGiven = TextNameFirst.Text,
                NameFamily = TextNameLast.Text,
                NameUser = TextNameUser.Text,
                PhoneMain = TextPhoneNumber.Text,
                Password = TextPassword.Text,
                ServiceDateStart = serviceDateStart
            };

            return result;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            createAccountButton.Enabled = (
                (TextNameFirst.Length() > 0) &&
                (TextNameLast.Length() > 0) &&
                (TextNameUser.Length() > 0) &&
                (TextPassword.Length() > 0) &&
                (TextPhoneNumber.Length() > 0) &&
                (TextServiceStart.Length() > 0)
            );
        }

    }
}