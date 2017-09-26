using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Castle Windsor test class.
    /// </summary>
    [TestFixture]
    public class WindsorTest
    {
        private IWindsorContainer _windsorContainer;

        public WindsorTest()
        {
            _windsorContainer = new WindsorContainer(new XmlInterpreter());
        }

        [Test]
        public void Test()
        {
            WindsorTestObject c = _windsorContainer.Resolve<WindsorTestObject>();
            Assert.AreEqual(c.Test(), "test");
        }
    }

    #region WindsorTestObject
    /// <summary>
    /// Use for Windsor test.
    /// </summary>
    public class WindsorTestObject
    {
        public string Test()
        {
            return "test";
        }
    }
    #endregion
}
