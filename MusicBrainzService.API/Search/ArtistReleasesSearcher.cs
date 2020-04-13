using System;
using System.Collections.Generic;
using System.Linq;
using MusicBrainzService.DAL;

namespace MusicBrainzService.API.Search
{
    public class ArtistReleasesSearcher : IArtistReleasesSearcher
    {
        private readonly IArtistReleasesRepository _musicBrainzRepository;

        public ArtistReleasesSearcher(IArtistReleasesRepository musicBrainzRepository)
        {
            _musicBrainzRepository = musicBrainzRepository;
        }

        public ArtistReleasesResponse GetReleasesForArtistOrListOfPossibleArtistMatches(ArtistReleasesRequest request)
        {
            var result = new ArtistReleasesResponse();
            var artists = GetArtists(request);

            if (OnlyOneArtistReturned(artists))
            {
                result.Releases = GetReleases(artists.First(), request.Limit, request.Offset);
                return result;
            }

            if (MoreThanOneArtistReturned(artists))
            {
                result.Artists = artists;
                return result;
            }

            return result;
        }

        private List<Artist> GetArtists(ArtistReleasesRequest request) =>
            _musicBrainzRepository.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset);
        

        private List<Release> GetReleases(Artist artist, int limit, int offset)
        {
            if (artist == null || artist.Id ==  null)
                throw new Exception("Cannot retrieve Releases when artist not specified");

            return _musicBrainzRepository.RetrieveReleasesByArtistId(artist.Id, limit, offset);
        }

        private bool OnlyOneArtistReturned(List<Artist> artists) => artists != null && artists.Count == 1;

        private static bool MoreThanOneArtistReturned(List<Artist> artists) => artists != null && artists.Count > 1;

    }
}