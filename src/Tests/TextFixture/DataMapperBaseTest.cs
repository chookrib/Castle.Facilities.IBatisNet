using System;
using System.Linq;

using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// DataMapper test base class.
    /// </summary>
    public abstract class DataMapperBaseTest
    {
        protected TestObjectDao _testObjectDao;
        protected TestNullableObjectDao _testNullableObjectDao;

        [Test]
        public void TestObjectCRUD()
        {
            TestObject object1 = TestObject.NewRandom();
            _testObjectDao.Insert(object1);

            TestObject object2 = _testObjectDao.SelectById(object1.Id);
            AssertHelper.AreEqual(object1, object2);

            _testObjectDao.DeleteById(object1.Id);
            Assert.IsNull(_testObjectDao.SelectById(object1.Id));
        }

        [Test]
        public void TestNullableObjectCRUD()
        {
            //property not null
            Random random = new Random();
            TestNullableObject object1 = new TestNullableObject();
            object1.StringValue = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray());
            object1.IntValue = random.Next();
            object1.LongValue = random.Next();
            object1.DecimalValue = Math.Round(Convert.ToDecimal(random.NextDouble()), 4);
            object1.BoolValue = random.Next(100) < 50;
            DateTime now = DateTime.Now;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond));
            //Console.Out.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss:fffffff"));
            object1.DateTimeValue = now;
            object1.EnumValue = (TestEnum)Enum.Parse(typeof(TestEnum), random.Next(1, 3).ToString());
            _testNullableObjectDao.Insert(object1);

            TestNullableObject object2 = _testNullableObjectDao.SelectById(object1.Id);
            AssertHelper.AreEqual(object1, object2);

            _testNullableObjectDao.DeleteById(object1.Id);
            Assert.IsNull(_testNullableObjectDao.SelectById(object1.Id));

            //property null
            TestNullableObject object4 = new TestNullableObject();
            _testNullableObjectDao.Insert(object4);

            TestNullableObject object5 = _testNullableObjectDao.SelectById(object4.Id);
            AssertHelper.AreEqual(object4, object5);
            Assert.IsNull(object5.EnumValue);
            //if config nullValue="Enum1"
            //Assert.AreEqual(object5.EnumValue, TestEnum.Enum1);

            _testNullableObjectDao.DeleteById(object4.Id);
            Assert.IsNull(_testNullableObjectDao.SelectById(object4.Id));
        }

    }
}
