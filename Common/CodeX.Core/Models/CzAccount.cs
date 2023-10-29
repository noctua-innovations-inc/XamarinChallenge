using CodeX.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeX.Core.Models
{
    public class CzAccount
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "error_required_name_user", ErrorMessageResourceType = typeof(CodeX.Core.Properties.Resources))]
        [StringLength(72, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 72 characters")]
        public string NameUser { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "error_required_name_given", ErrorMessageResourceType = typeof(CodeX.Core.Properties.Resources))]
        [SpecialCharacterValidation("!@#$%^&", ErrorMessage = "Given name must not have special characters")]
        public string NameGiven { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "error_required_name_family", ErrorMessageResourceType = typeof(CodeX.Core.Properties.Resources))]
        [SpecialCharacterValidation("!@#$%^&", ErrorMessage = "Family name must not have special characters")]
        public string NameFamily { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "error_required_phonemain", ErrorMessageResourceType = typeof(CodeX.Core.Properties.Resources))]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Phone number must have this format: (###) ###-####")]
        public string PhoneMain
        {
            get => _phoneMain;
            set { _phoneMain = CzTransformer.FormatPhoneNumber(value); }
        }
        private string _phoneMain;

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "error_required_password", ErrorMessageResourceType = typeof(CodeX.Core.Properties.Resources))]
        [PasswordValidation(8, MaximumLength = 15)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Service Start Date is required")]
        [DateValidation]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ServiceDateStart { get; set; }
    }
}