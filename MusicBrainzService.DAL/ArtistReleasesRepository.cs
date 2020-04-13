using System;
using System.Collections.Generic;
using MetaBrainz.MusicBrainz;
using MusicBrainzService.DAL.Mappers;
using Microsoft.Extensions.Logging;

namespace MusicBrainzService.DAL
{
    public class ArtistReleasesRepository : IArtistReleasesRepository
    {
        private const string ArtistsSearchString = "artist:";
        private const string ReleaseSearchString = "arid:";
        private const string UserAgent = "Ceola's App/1.0.0 ( me@gmail.com )";
        private readonly ILogger<ArtistReleasesRepository> _logger;
        private readonly Query _queryservice;

        public ArtistReleasesRepository(ILogger<ArtistReleasesRepository> logger)
        {
            _queryservice = new Query(UserAgent);
            _logger = logger;
        }

        public List<Artist> RetrieveArtistBySearchTerm(string artistName, int limit, int offset)
        {
            try
            {
                var artists = _queryservice.FindArtists($"{ArtistsSearchString}{artistName}", limit, offset);

                if (artists == null) return null;

                return ArtistReleasesMapper.MapToArtistList(artists.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Issue retrieving Artist: {artistName}", ex);
                throw ex;
            }
            
        }

        public List<Release> RetrieveReleasesByArtistId(Guid artistId, int limit, int offset)
        {
            try
            {
                // NB: Using ReleaseGroups here instead of Release as the 3rd party has a bug in the Release endpoint that breaks the .net package
                var releases = _queryservice.FindReleaseGroups($"{ReleaseSearchString}{artistId}", limit, offset);

                return ArtistReleasesMapper.MapToReleaseList(releases.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Issue retrieving Releases for artist: {artistId}", ex);
                throw ex;
            }
}
    }
}
