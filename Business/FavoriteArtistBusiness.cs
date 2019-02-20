using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;

namespace Business
{
	public class FavoriteArtistBusiness : IFavoriteArtistBusiness
	{
		private readonly IFavoriteArtistRepository _favoriteArtistRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="FavoriteArtistBusiness"/> class.
		/// </summary>
		/// <param name="favoriteArtistRepository">The favorite artist repository.</param>
		public FavoriteArtistBusiness(IFavoriteArtistRepository favoriteArtistRepository)
		{
			_favoriteArtistRepository = favoriteArtistRepository;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Artist>> GetFavoriteArtists(int userId)
		{
			return await _favoriteArtistRepository.GetFavoriteArtists(userId);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Artist>> GetFavoriteArtistsByName(string albumName, int userId)
		{
			return await _favoriteArtistRepository.GetFavoriteArtistsByName(albumName, userId);
		}

		/// <inheritdoc />
		public int SaveFavoriteArtist(Artist artist, int userId)
		{
			return _favoriteArtistRepository.SaveFavoriteArtist(artist, userId);
		}

		/// <inheritdoc />
		public void DeleteFavoriteArtist(int favoriteAlbumId)
		{
			_favoriteArtistRepository.DeleteFavoriteArtist(favoriteAlbumId);
		}
	}
}
