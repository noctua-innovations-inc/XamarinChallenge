using System;
using System.ComponentModel.DataAnnotations;

namespace CodeX.Core.Models
{
    /// <summary>
    /// Data validation via Open-Closed Principle.
    /// Data Transfer Objects (DTO) are closed to modification,
    /// but open to extension...
    /// </summary>
    public sealed class DateValidationAttribute : ValidationAttribute
    {
        // TODO : Put strings into string resource file
        public override bool IsValid(object value)
        {
            // TODO : Make the number of days into the future a validation parameter
            const int MaxFutureDateInDays = 30;

            try
            {
                var serviceDate = ((DateTime)value).Date;
                var today = DateTime.Now.Date;

                if (serviceDate < today)
                {
                    Reason = "Service Start Date cannot be in the past";
                    return false;
                }

                if (serviceDate.Subtract(today).TotalDays > MaxFutureDateInDays)
                {
                    Reason = "To early to create an account for this Service Start Date";
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Reason { get; private set; }

        public override string FormatErrorMessage(string name)
        {
            return Reason;
        }
    }
}