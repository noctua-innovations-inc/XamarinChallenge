using CodeX.Core.Contracts;
using CodeX.Core.Models;
using System;
using System.Threading.Tasks;

namespace CodeX.Core.Services
{
    public class CzClientAccountClerk : ICzClientAccountClerk
    {
        public static ICzClientAccountClerk Instance = new CzClientAccountClerk();

        public CzClientAccountClerk()
        {
            Repository = CzRepository.Instance;
        }

        private ICzRepository Repository { get; }

        public async Task<bool> Login(string username, string password)
        {
            var result = false;

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException(CodeX.Core.Properties.Resources.error_account_bad_username);
            }

            var record = await Repository.GetRecordAsync<CzAccount>(entry => entry.NameUser.ToUpper() == username.ToUpper());
            if (record == null)
            {
                throw new ArgumentException($"Account does not exist for username: {username}");
            }

            if (record.Password != password)
            {
                throw new ArgumentException("Password is wrong");
            }

            return result;
        }

        public async Task<bool> CreateAccount(CzAccount account)
        {
            var record = await Repository.GetRecordAsync<CzAccount>(entry => entry.NameUser.ToUpper() == account.NameUser.ToUpper());
            if (record != null)
            {
                throw new ArgumentException("Username is already in use");
            }
            var count = await Repository.SaveRecordAsync(account);

            // One record was appended to the database table
            return (count == 1);
        }
    }
}