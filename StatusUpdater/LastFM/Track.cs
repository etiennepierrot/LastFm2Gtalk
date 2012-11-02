namespace StatusUpdater.LastFM
{
    public class Track
    {
        public string Artist { get; set; }
        public string Song { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Artist, Song);
        }
    }
}