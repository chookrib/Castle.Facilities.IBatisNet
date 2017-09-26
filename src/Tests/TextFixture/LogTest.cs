using System;
using System.IO;

using log4net;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Log test class.
    /// </summary>
    [TestFixture]
    public class LogTest
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(LogTest));

        public LogTest()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config"));
        }

        [Test]
        public void TestLog()
        {
            _log.Info(DateTime.Now);
        }
    }
}
