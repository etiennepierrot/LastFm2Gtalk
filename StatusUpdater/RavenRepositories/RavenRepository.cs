using Raven.Client;
using StructureMap;

namespace StatusUpdater.RavenRepositories
{

    public class RavenRepository
    {
        public static IDocumentSession GetInstance
        {
            get { return ObjectFactory.GetInstance<IDocumentSession>(); }
        }
    }
}
