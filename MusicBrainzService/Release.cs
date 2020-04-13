using System;
using System.Collections.Generic;

namespace MusicBrainzService
{
    public class Release
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public List<Artist> Artists { get; set; }

        public Release(string title, Guid id, List<Artist> artists)
        {
            Title = title;
            Id = id;
            Artists = artists;
        }
    }
}
