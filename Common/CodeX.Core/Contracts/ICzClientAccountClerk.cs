using CodeX.Core.Models;
using System.Threading.Tasks;

namespace CodeX.Core.Contracts
{
    public interface ICzClientAccountClerk
    {
        Task<bool> Login(string username, string password);

        Task<bool> CreateAccount(CzAccount account);
    }
}