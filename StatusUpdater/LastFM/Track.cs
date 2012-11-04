using StatusUpdater.RavenRepositories;

namespace StatusUpdater.LastFM
{
    public class Track:IEntity
    {
        public string Artist { get; set; }
        public string Song { get; set; }
        public bool IsCurrent { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Artist, Song);
        }

        public string Id { get; set; }

        public string UrlCover { get; set; }
    }
}