using System.Threading.Tasks;
using Entities;

namespace Business.Interfaces
{
    public interface IAccountBusiness
    {
        Task<Account> GetAccount(string login, string password);

        Task<int> Create(string login, string password);
    }
}
