using Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceAgent
{
	public class LastFmServiceAgent : ILastFmServiceAgent
	{
		HttpClient _client;

		public LastFmServiceAgent()
		{
			_client = new HttpClient();
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
		}

		public async Task<IEnumerable<Album>> GetAlbums(string albumName)
		{
			IEnumerable<Album> albums = new List<Album>();
			var response = await _client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=album.search&album=" + albumName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			AlbumsSearchByNameResponse result = JsonConvert.DeserializeObject<AlbumsSearchByNameResponse>(response);
			if (result != null)
			{
				albums = result.Results.AlbumMatches.Album;
			}

			return albums;
		}

		public async Task<Artist> GetArtist(string artistName)
		{
			Artist artist = new Artist();
			var response = await _client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + artistName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			ArtistSearchByNameResponse result = JsonConvert.DeserializeObject<ArtistSearchByNameResponse>(response);
			if (result != null)
			{
				artist = result.Artist;
			}

			return artist;
		}

		public async Task<IEnumerable<TopAlbum>> GetTopAlbums(string artistName)
		{
			IEnumerable<TopAlbum> topAlbums = new List<TopAlbum>();
			var response = await _client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=" + artistName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			TopAlbumsSearchByArtistNameResponse result = JsonConvert.DeserializeObject<TopAlbumsSearchByArtistNameResponse>(response);
			if (result != null)
			{
				topAlbums = result.TopAlbums.Album;
			}

			return topAlbums;
		}

		public async Task<IEnumerable<Track>> GetTopTracks(string artistName)
		{
			IEnumerable<Track> tracks = new List<Track>();
			var response = await _client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=" + artistName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			TopTracksSearchByArtistNameResponse result = JsonConvert.DeserializeObject<TopTracksSearchByArtistNameResponse>(response);
			if (result != null)
			{
				tracks = result.TopTracks.Track;
			}

			return tracks;
		}
	}
}
