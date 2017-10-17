using System;

using Castle.Facilities.IBatisNet;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TransactionWindsorNestedTestEntry
    {
        private TestObjectDao _testObjectDao;
        private ITransactionWindsorNestedTest1 _test1;

        public TransactionWindsorNestedTestEntry()
        {
            WindsorContainer windsorContainer = new WindsorContainer(new XmlInterpreter());
            _testObjectDao = windsorContainer.Resolve<TestObjectDao>();
            _test1 = windsorContainer.Resolve<ITransactionWindsorNestedTest1>();
        }

        [Test]
        public void Test()
        {
            TestObject object1 = TestObject.NewRandom();
            _test1.Insert(object1);
            AssertHelper.AreEqual(_testObjectDao.SelectById(object1.Id), object1);
        }

        [Test]
        public void Test2()
        {
            TestObject object1 = TestObject.NewRandom();
            try
            {
                _test1.Insert2(object1);
            }
            catch
            {
            }
            Assert.IsNull(_testObjectDao.SelectById(object1.Id));
        }
    }

    #region TransactionWindsorNestedTest
    public interface ITransactionWindsorNestedTest1
    {
        void Insert(TestObject object1);

        void Insert2(TestObject object1);
    }

    [Transactional]
    public class TransactionWindsorNestedTest1 : ITransactionWindsorNestedTest1
    {
        protected ITransactionWindsorNestedTest2 _test2;

        public TransactionWindsorNestedTest1(ITransactionWindsorNestedTest2 test2)
        {
            _test2 = test2;
        }

        [Transaction]
        public virtual void Insert(TestObject object1)
        {
            _test2.Insert(object1);
        }

        [Transaction]
        public virtual void Insert2(TestObject object1)
        {
            _test2.Insert(object1);
            _test2.Insert(object1);
        }
    }

    public interface ITransactionWindsorNestedTest2
    {
        void Insert(TestObject object1);
    }

    [Transactional]
    public class TransactionWindsorNestedTest2 : ITransactionWindsorNestedTest2
    {
        protected TestObjectDao _testObjectDao;

        public TransactionWindsorNestedTest2(TestObjectDao testObjectDao)
        {
            _testObjectDao = testObjectDao;
        }

        [Transaction]
        public void Insert(TestObject object1)
        {
            _testObjectDao.Insert(object1);
        }
    }
    #endregion
}
