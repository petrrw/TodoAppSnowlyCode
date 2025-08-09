using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppSnowlyCode.IntegrationTests
{
    /// <summary>
    /// Base class for IT tests that need to work with in-memory test server.
    /// </summary>
    public class ITTestsBase
    {
        protected TestServerFactory _serverFactory = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _serverFactory = new TestServerFactory();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _serverFactory.Dispose();
        }
    }
}