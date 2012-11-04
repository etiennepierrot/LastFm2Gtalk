namespace StatusUpdater.LastFM
{
    public class TrackService
    {
        public void SetCurrentTrack(Track track)
        {
            CurrentTrack.Instance.Track = track;
        }

        public Track GetCurrentTrack()
        {
            return CurrentTrack.Instance.Track;
        }
    }
}