using Raven.Client;
using Raven.Client.Embedded;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace StatusUpdater
{
    public sealed class StructureMapConfigurationRegistry : Registry
    {
        public StructureMapConfigurationRegistry()
    {
        // register RavenDB document store
        ForSingletonOf<IDocumentStore>().Use(() =>
        {
            var documentStore = new EmbeddableDocumentStore { DataDirectory = "Data2", UseEmbeddedHttpServer = false };

            documentStore.Initialize();

            return documentStore;
        });

        ObjectFactory.Configure(x => x.For<Facade>().Singleton());

        // register RavenDB document session
        For<IDocumentSession>().Use(context => context.GetInstance<IDocumentStore>().OpenSession());
        Scan(x => x.TheCallingAssembly());
    }
    }
}
