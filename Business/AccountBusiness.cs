using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _accountRepository;

        public AccountBusiness(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> GetAccount(string username, string password)
        {
            var userId = await _accountRepository.GetUser(username, password);
            if (userId > 0)
            {
                var user = new Account();

                // remove password before returning
                user.UserId = userId;
                user.Username = username;
                user.Password = null;
                return user;
            }

            return null;
        }

        public async Task<int> Create(string username, string password)
        {
            var userId = await _accountRepository.Create(username, password);
            if (userId > 0)
            {
                return userId;
            }

            return 0;
        }
    }
}
