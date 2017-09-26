using System.Data.SqlClient;

using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Transaction test class, create instance manual.
    /// </summary>
    [TestFixture]
    public class TransactionManualTest
    {
        [Test]
        public void TestTransaction1()
        {
            ISqlMapper sqlMapper = new DomSqlMapBuilder().Configure("IBatisNet.config");
            TestObjectDao testObjectDao = new TestObjectDao(sqlMapper);
            TestObject object1 = TestObject.NewRandom();

            //try...catch... syntax
            try
            {
                sqlMapper.BeginTransaction();
                testObjectDao.Insert(object1);
                testObjectDao.Insert(object1);
                sqlMapper.CommitTransaction();
            }
            catch
            {
                sqlMapper.RollBackTransaction();
            }

            TestObject object2 = testObjectDao.SelectById(object1.Id);
            Assert.IsNull(object2);
        }

        [Test]
        public void TestTransaction2()
        {
            ISqlMapper sqlMapper = new DomSqlMapBuilder().Configure("IBatisNet.config");
            TestObjectDao testObjectDao = new TestObjectDao(sqlMapper);
            TestObject object1 = TestObject.NewRandom();

            try
            {
                //using... syntax
                using (IDalSession session = sqlMapper.BeginTransaction())
                {
                    testObjectDao.Insert(object1);
                    testObjectDao.Insert(object1);
                    session.Complete();
                }
            }
            catch { }

            TestObject object2 = testObjectDao.SelectById(object1.Id);
            Assert.IsNull(object2);
        }

        [Test]
        public void TestNoTransaction()
        {
            ISqlMapper sqlMapper = new DomSqlMapBuilder().Configure("IBatisNet.config");
            TestObjectDao testObjectDao = new TestObjectDao(sqlMapper);

            TestObject object1 = TestObject.NewRandom();
            testObjectDao.Insert(object1);
            Assert.Throws<SqlException>(() => testObjectDao.Insert(object1));
        }
    }
}
