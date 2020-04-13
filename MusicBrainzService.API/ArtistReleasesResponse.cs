using System.Collections.Generic;

namespace MusicBrainzService.API
{
    public class ArtistReleasesResponse
    {
        public List<Artist> Artists { get; set; }

        public List<Release> Releases { get; set; }
    }
}
