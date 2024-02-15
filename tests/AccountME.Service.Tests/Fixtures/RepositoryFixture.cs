using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccountMe.Service.Tests.Fixtures
{


    public class RepositoryFixture : IDisposable
    {


        public RepositoryFixture()
        {
            Repository = new Repository();
        }

        public void Dispose()
        {
        }

        public Repository Repository { get; private set; }

    }

    [CollectionDefinition("Repository collection")]
    public class RepositoryCollection : ICollectionFixture<RepositoryFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
