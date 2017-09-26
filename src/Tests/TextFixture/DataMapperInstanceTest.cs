using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// DataMapper test class, create instance manual.
    /// </summary>
    [TestFixture]
    public class DataMapperInstanceTest : DataMapperBaseTest
    {
        public DataMapperInstanceTest()
        {
            ISqlMapper _sqlMapper = new DomSqlMapBuilder().Configure("IBatisNet.config");
            _testObjectDao = new TestObjectDao(_sqlMapper);
            _testNullableObjectDao = new TestNullableObjectDao(_sqlMapper);
        }
    }
}
