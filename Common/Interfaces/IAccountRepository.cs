using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IAccountRepository
	{
		Task<int> GetUser(string login, string password);

		Task<int> Create(string login, string password);
	}
}
