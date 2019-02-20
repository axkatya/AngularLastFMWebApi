using Business.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
	/// <summary>
	/// The favorite album business.
	/// </summary>
	/// <seealso cref="Business.Interfaces.IFavoriteAlbumBusiness" />
	public class FavoriteAlbumBusiness : IFavoriteAlbumBusiness
	{
		private readonly IFavoriteAlbumRepository _favoriteAlbumRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="FavoriteAlbumBusiness"/> class.
		/// </summary>
		/// <param name="favoriteAlbumRepository">The favorite album repository.</param>
		public FavoriteAlbumBusiness(IFavoriteAlbumRepository favoriteAlbumRepository)
		{
			_favoriteAlbumRepository = favoriteAlbumRepository;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Album>> GetFavoriteAlbums(int userId)
		{
			return await _favoriteAlbumRepository.GetFavoriteAlbums(userId);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId)
		{
			return await _favoriteAlbumRepository.GetFavoriteAlbumsByName(albumName, userId);
		}

		/// <inheritdoc />
		public Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
		{
			return _favoriteAlbumRepository.GetFavoriteAlbumsByAlbumNameAndArtistName(albumName, artistName, userId);
		}

		/// <inheritdoc />
		public int SaveFavoriteAlbum(Album album, int userId)
		{
			return _favoriteAlbumRepository.SaveFavoriteAlbum(album, userId);
		}

		/// <inheritdoc />
		public void DeleteFavoriteAlbum(int favoriteAlbumId)
		{
			_favoriteAlbumRepository.DeleteFavoriteAlbum(favoriteAlbumId);
		}
	}
}
