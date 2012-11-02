using System.Xml;
using StatusUpdater.Exceptions;

namespace StatusUpdater.LastFM
{
    public class LastFmContentParserService
    {
        public string ParseStatusResponse(string content)
        {
            var doc = new XmlDocument();
            doc.LoadXml(content);
            var statusReponseNode = doc.SelectSingleNode("lfm");
            if (statusReponseNode == null)
            {
                throw new BadFormatResponseLastFmException("No lfm node");
            }
            if (statusReponseNode.Attributes == null)
            {
                throw new BadFormatResponseLastFmException("no attributes for lfm node");
            }
            var statusAttribute = statusReponseNode.Attributes["status"];

            return statusAttribute.Value;
        }

        public Track ParseCurrentTrack(string response)
        {
            var nodeTrack = GetNodeCurrentTrack(response);

            if(nodeTrack != null)
            {
                var selectSingleNode = nodeTrack.SelectSingleNode("//artist");
                string artist;
                if (selectSingleNode != null)
                {
                    artist = selectSingleNode.InnerText;
                }
                else
                {
                    throw new BadFormatResponseLastFmException("no artist node in track");
                }

                var nodeSong = nodeTrack.SelectSingleNode("//name");
                string song;
                if (nodeSong != null) song = nodeSong.InnerText;
                else
                {
                    throw new BadFormatResponseLastFmException("no song node in track");
                }

                return new Track
                           {
                               Artist = artist,
                               Song = song
                           };
            }
            return null;
        }

        public virtual XmlNode GetNodeCurrentTrack(string response)
        {
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var nodeTrack = doc.SelectSingleNode("//track[@nowplaying='true']");
            return nodeTrack;
        }
    }
}