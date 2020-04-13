using System.Collections.Generic;
using System.Linq;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MusicBrainzService.DAL.Mappers
{
    public static class ArtistReleasesMapper
    {
        public static List<Release> MapToReleaseList(IReadOnlyList<IFoundReleaseGroup> results)
        {
            var releases = new List<Release>();

            results.ToList().ForEach(r => releases.Add(MapToRelease(r)));

            return releases;
        }

        public static List<Artist> MapToArtistList(IReadOnlyList<IFoundArtist> results)
        {
            var artists = new List<Artist>();

            results.ToList().ForEach(r => artists.Add(MapToArtist(r)));

            return artists;
        }

        private static List<Artist> MapArtistCreditsToArtist(IReadOnlyList<INameCredit> artistCredit)
        {
            var artists = new List<Artist>();
           
            artistCredit.ToList().ForEach(r => artists.Add(MapToArtist(r.Artist)));

            return artists;
        }

        private static Artist MapToArtist(IArtist artist)
        {
            return new Artist(artist.MbId, artist.Name, artist.SortName, artist.Disambiguation);
        }

        private static Release MapToRelease(IFoundReleaseGroup release)
        {
            return new Release(release.Title, release.MbId, MapArtistCreditsToArtist(release.ArtistCredit));
        }
    }

}
