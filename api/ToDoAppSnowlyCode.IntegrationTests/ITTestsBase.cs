namespace ToDoAppSnowlyCode.IntegrationTests
{
    /// <summary>
    /// Base class for IT tests that need to work with in-memory test server.
    /// </summary>
    public abstract class ITTestsBase
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