using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAgent
{
	public interface ILastFmServiceAgent
	{
		Task<IEnumerable<Album>> GetAlbums(string albumName);

		Task<Artist> GetArtist(string artistName);

		Task<IEnumerable<TopAlbum>> GetTopAlbums(string artistName);

		Task<IEnumerable<Track>> GetTopTracks(string artistName);
	}
}
