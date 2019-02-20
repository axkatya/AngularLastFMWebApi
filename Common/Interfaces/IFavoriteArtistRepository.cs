using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IFavoriteArtistRepository
	{
		Task<IEnumerable<Entities.Artist>> GetFavoriteArtists(int userId);

		Task<IEnumerable<Entities.Artist>> GetFavoriteArtistsByName(string albumName, int userId);

		int SaveFavoriteArtist(Entities.Artist artist, int userId);

		void DeleteFavoriteArtist(int favoriteArtistId);
	}
}
