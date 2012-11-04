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
                var selectSingleNode = nodeTrack.SelectSingleNode("//image[@size='extralarge']");
                return new Track
                           {
                               Artist = GetData(nodeTrack, "artist"),
                               Song = GetData(nodeTrack, "name"),
                               UrlCover = selectSingleNode != null ? selectSingleNode.InnerText : string.Empty
                           };
            }
            return null;
        }

        private string GetData(XmlNode node, string field)
        {
            var selectSingleNode = node.SelectSingleNode(string.Format("//{0}", field));
            if (selectSingleNode != null)
            {
                return selectSingleNode.InnerText;
            }
            throw new BadFormatResponseLastFmException(string.Format("no {0} node in track", field));
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