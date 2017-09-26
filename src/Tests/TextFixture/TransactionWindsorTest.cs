using System;

using Castle.Facilities.IBatisNet;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TransactionWindsorTestEntry
    {
        private TestObjectDao _testObjectDao;
        private TransactionWindsorTest _test;
        private ITransactionWindsorTest _itest;

        public TransactionWindsorTestEntry()
        {
            WindsorContainer windsorContainer = new WindsorContainer(new XmlInterpreter());
            _testObjectDao = windsorContainer.Resolve<TestObjectDao>();
            _test = windsorContainer.Resolve<TransactionWindsorTest>();
            _itest = windsorContainer.Resolve<ITransactionWindsorTest>();
        }

        [Test]
        public void TestTransaction()
        {
            TestInsertFaild(_test.InsertTwiceT);   //Transaction
            //TestInsertFaild(_test.InsertTwiceTNotVirtual);   //FacilitiesException
            TestInsertSuccess(_test.InsertTwice);  //No transaction
        }

        [Test]
        public void TestNestedTransaction()
        {
            TestInsertFaild(_test.InsertTwiceT_Nested_NN);
            TestInsertFaild(_test.InsertTwiceT_Nested_NT);
            TestInsertFaild(_test.InsertTwiceT_Nested_TN);
            TestInsertFaild(_test.InsertTwiceT_Nested_TT);

            TestInsertSuccess(_test.InsertTwice_Nested_NN);
            TestInsertSuccess(_test.InsertTwice_Nested_NT);
            TestInsertSuccess(_test.InsertTwice_Nested_TN);
            TestInsertSuccess(_test.InsertTwice_Nested_TT);
        }

        [Test]
        public void TestMixedT()
        {
            TestObject object1 = TestObject.NewRandom();
            TestObject object2 = TestObject.NewRandom();
            TestObject object3 = TestObject.NewRandom();
            try { _test.TestMixedT(object1, object2, object3); } catch { }
            Assert.IsNull(_testObjectDao.SelectById(object1.Id));
            Assert.IsNull(_testObjectDao.SelectById(object2.Id));
            Assert.IsNull(_testObjectDao.SelectById(object3.Id));
        }

        [Test]
        public void TestMixed()
        {
            TestObject object1 = TestObject.NewRandom();
            TestObject object2 = TestObject.NewRandom();
            TestObject object3 = TestObject.NewRandom();
            try { _test.TestMixed(object1, object2, object3); } catch { }
            AssertHelper.AreEqual(_testObjectDao.SelectById(object1.Id), object1);
            Assert.IsNull(_testObjectDao.SelectById(object2.Id));
            AssertHelper.AreEqual(_testObjectDao.SelectById(object3.Id), object3);
        }

        [Test]
        public void TestTransactionInterface()
        {
            TestInsertFaild(_itest.InsertTwiceT);   //Transaction
            TestInsertSuccess(_itest.InsertTwice);  //No transaction
        }

        public void TestInsertFaild(Action<TestObject> action)
        {
            TestObject object1 = TestObject.NewRandom();
            try
            {
                action(object1);   //SqlException : Violation of PRIMARY KEY constraint
            }
            catch
            {
            }
            Assert.IsNull(_testObjectDao.SelectById(object1.Id));
        }

        public void TestInsertSuccess(Action<TestObject> action)
        {
            TestObject object1 = TestObject.NewRandom();
            try
            {
                action(object1);   //SqlException : Violation of PRIMARY KEY constraint
            }
            catch
            {
            }
            AssertHelper.AreEqual(_testObjectDao.SelectById(object1.Id), object1);
        }
    }

    #region TransactionWindsorTest
    /// <summary>
    /// Transaction test class, create instance by Windsor container.
    /// </summary>
    [Transactional]
    public class TransactionWindsorTest
    {
        protected TestObjectDao _testObjectDao;

        public TransactionWindsorTest(TestObjectDao testObjectDao)
        {
            _testObjectDao = testObjectDao;
        }

        [Transaction]
        public virtual void InsertT(TestObject object1)
        {
            _testObjectDao.Insert(object1);
        }

        public virtual void Insert(TestObject object1)
        {
            _testObjectDao.Insert(object1);
        }

        //=================================================================

        [Transaction]
        public virtual void InsertTwiceT(TestObject object1)
        {
            _testObjectDao.Insert(object1);
            _testObjectDao.Insert(object1);
        }

        //[Transaction]
        //public void InsertTwiceTNotVirtual(TestObject object1)
        //{
        //    _testObjectDao.Insert(object1);
        //    _testObjectDao.Insert(object1);
        //}

        public virtual void InsertTwice(TestObject object1)
        {
            _testObjectDao.Insert(object1);
            _testObjectDao.Insert(object1);
        }

        //=================================================================

        [Transaction]
        public virtual void InsertTwiceT_Nested_NN(TestObject object1)
        {
            Insert(object1);
            Insert(object1);
        }

        [Transaction]
        public virtual void InsertTwiceT_Nested_TT(TestObject object1)
        {
            InsertT(object1);
            InsertT(object1);
        }

        [Transaction]
        public virtual void InsertTwiceT_Nested_NT(TestObject object1)
        {
            Insert(object1);
            InsertT(object1);
        }

        [Transaction]
        public virtual void InsertTwiceT_Nested_TN(TestObject object1)
        {
            InsertT(object1);
            Insert(object1);
        }

        public virtual void InsertTwice_Nested_NN(TestObject object1)
        {
            Insert(object1);
            Insert(object1);
        }

        public virtual void InsertTwice_Nested_TT(TestObject object1)
        {
            InsertT(object1);
            InsertT(object1);
        }

        public virtual void InsertTwice_Nested_NT(TestObject object1)
        {
            Insert(object1);
            InsertT(object1);
        }

        public virtual void InsertTwice_Nested_TN(TestObject object1)
        {
            InsertT(object1);
            Insert(object1);
        }

        //=====================================================================

        [Transaction]
        public virtual void InsertTwoT(TestObject object1, TestObject object2)
        {
            Insert(object1);
            Insert(object2);
        }

        public virtual void InsertTwo(TestObject object1, TestObject object2)
        {
            Insert(object1);
            Insert(object2);
        }

        [Transaction]
        public virtual void TestMixedT(TestObject object1, TestObject object2, TestObject object3)
        {
            Insert(object1);
            try
            {
                InsertTwiceT(object2); //faild
            }
            catch
            {
            }
            Insert(object3);
            throw new Exception("throw exception for test.");
        }

        public virtual void TestMixed(TestObject object1, TestObject object2, TestObject object3)
        {
            Insert(object1);
            try
            {
                InsertTwiceT(object2); //faild
            }
            catch
            {
            }
            Insert(object3);
            throw new Exception("throw exception for test.");
        }
    }

    public interface ITransactionWindsorTest
    {
        void InsertTwiceT(TestObject object1);

        void InsertTwice(TestObject object1);
    }

    [Transactional]
    public class TransactionWindsorTestImpl : ITransactionWindsorTest
    {
        protected TestObjectDao _testObjectDao;

        public TransactionWindsorTestImpl(TestObjectDao testObjectDao)
        {
            _testObjectDao = testObjectDao;
        }

        [Transaction]
        public void InsertTwiceT(TestObject object1)
        {
            _testObjectDao.Insert(object1);
            _testObjectDao.Insert(object1);
        }

        public void InsertTwice(TestObject object1)
        {
            _testObjectDao.Insert(object1);
            _testObjectDao.Insert(object1);
        }
    }
    #endregion
}
