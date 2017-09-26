using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// DataMapper test class, create instance by Windsor container.
    /// </summary>
    [TestFixture]
    public class DataMapperWindsorTest : DataMapperBaseTest
    {
        public DataMapperWindsorTest()
        {
            IWindsorContainer _windsorContainer = new WindsorContainer(new XmlInterpreter());
            _testObjectDao = _windsorContainer.Resolve<TestObjectDao>();
            _testNullableObjectDao = _windsorContainer.Resolve<TestNullableObjectDao>();
        }
    }
}
