using System;

namespace StatusUpdater.Exceptions
{
    public class BadFormatResponseLastFmException : Exception
    {
        public BadFormatResponseLastFmException(string noLfmNode):base(noLfmNode)
        {
        }
    }
}