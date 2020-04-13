using System;

namespace MusicBrainzService
{
    public class Artist
    {
        public Artist(Guid id, string name, string sortName, string description)
        {
            Id = id;
            Name = name;
            SortName = sortName;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string SortName { get; private set; }
        public string Description { get; private set; }
    }
}