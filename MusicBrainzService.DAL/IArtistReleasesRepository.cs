using System;
using System.Collections.Generic;

namespace MusicBrainzService.DAL
{
    public interface IArtistReleasesRepository
    {
        public List<Artist> RetrieveArtistBySearchTerm(string searchTerm, int limit, int offset);
        public List<Release> RetrieveReleasesByArtistId(Guid id, int limit, int offset);
    }
}