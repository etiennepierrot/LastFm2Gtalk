using System.Collections.Generic;
using StatusUpdater.RavenRepositories;
using System.Linq;

namespace StatusUpdater.LastFM
{
    public class TrackService
    {
        private readonly RavenRepository<Track> _trackRavenRepository;

        public TrackService(RavenRepository<Track> trackRavenRepository)
        {
            _trackRavenRepository = trackRavenRepository;
        }

        public void SetCurrentTrack(Track track)
        {
            track.IsCurrent = true;
            DeleteCurrentTrack();
            _trackRavenRepository.Save(track);
            }

        public void DeleteCurrentTrack()
        {
            var oldCurrentTrack = GetCurrentTrack();

            if (oldCurrentTrack != null)
            {
                _trackRavenRepository.Delete(oldCurrentTrack.Id);
            }
        }

        public Track GetCurrentTrack()
        {
            IEnumerable<Track> queryAll = _trackRavenRepository.QueryAll();
            Track singleOrDefault = queryAll.SingleOrDefault(x => x.IsCurrent);
            return singleOrDefault;
        }
    }
}