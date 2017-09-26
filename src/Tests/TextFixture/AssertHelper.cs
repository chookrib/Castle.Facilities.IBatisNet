using NUnit.Framework;

namespace Tests
{
    public class AssertHelper
    {
        public static void AreEqual(TestObject object1, TestObject object2)
        {
            Assert.AreEqual(object1.Id, object2.Id);
            Assert.AreEqual(object1.StringValue, object2.StringValue);
            Assert.AreEqual(object1.IntValue, object2.IntValue);
            Assert.AreEqual(object1.LongValue, object2.LongValue);
            Assert.AreEqual(object1.DecimalValue, object2.DecimalValue);
            Assert.AreEqual(object1.BoolValue, object2.BoolValue);
            Assert.AreEqual(object1.DateTimeValue, object2.DateTimeValue);
            Assert.AreEqual(object1.EnumValue, object2.EnumValue);
        }

        public static void AreEqual(TestNullableObject object1, TestNullableObject object2)
        {
            Assert.AreEqual(object1.Id, object2.Id);
            Assert.AreEqual(object1.StringValue, object2.StringValue);
            Assert.AreEqual(object1.IntValue, object2.IntValue);
            Assert.AreEqual(object1.LongValue, object2.LongValue);
            Assert.AreEqual(object1.DecimalValue, object2.DecimalValue);
            Assert.AreEqual(object1.BoolValue, object2.BoolValue);
            Assert.AreEqual(object1.DateTimeValue, object2.DateTimeValue);
            Assert.AreEqual(object1.EnumValue, object2.EnumValue);
        }
    }
}
