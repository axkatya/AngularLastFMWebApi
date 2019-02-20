using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace Business
{
	/// <summary>
	/// The account business.
	/// </summary>
	/// <seealso cref="Business.Interfaces.IAccountBusiness" />
	public class AccountBusiness : IAccountBusiness
	{
		private readonly IAccountRepository _accountRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountBusiness"/> class.
		/// </summary>
		/// <param name="accountRepository">The account repository.</param>
		public AccountBusiness(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public async Task<Account> GetAccount(string login, string password)
		{
			var userId = await _accountRepository.GetUser(login, password).ConfigureAwait(false);
			if (userId > 0)
			{
				return new Account {UserId = userId, Username = login, Password = null};
			}

			return null;
		}

		public async Task<int> Create(string login, string password)
		{
			var userId = await _accountRepository.Create(login, password).ConfigureAwait(false);
			if (userId > 0)
			{
				return userId;
			}

			return 0;
		}
	}
}
