using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostlyMusic
{
    public interface IMostlyMusicAdapter
    {
        /// <summary>
        /// Searches Mostly Music for an album.
        /// </summary>
        /// <param name="query">The query to search for.</param>
        /// <returns>A list of responses.</returns>
        List<MostlyMusicQueryResponse> searchForAlbum(string query);
        /// <summary>
        /// Downloads the track titles for a specific album.
        /// </summary>
        /// <param name="url">The url of the album</param>
        /// <returns>A collection of all the tracks.</returns>
        List<MostlyMusicTrack> getTracksOfAlbum(string url);
    }
}
