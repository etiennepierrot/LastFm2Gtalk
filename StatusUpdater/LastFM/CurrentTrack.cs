using System;

namespace StatusUpdater.LastFM
{
    public sealed class CurrentTrack
    {
        private static volatile CurrentTrack _instance;
        private static readonly object SyncRoot = new Object();

        private CurrentTrack() { }

        public Track Track {get;set;}

        public static CurrentTrack Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new CurrentTrack();
                    }
                }

                return _instance;
            }
        }
    }
}