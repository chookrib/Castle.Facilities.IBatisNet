using System;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Object for test.
    /// </summary>
    public class TestObject
    {
        public TestObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public long LongValue { get; set; }

        public Decimal DecimalValue { get; set; }

        public bool BoolValue { get; set; }

        public DateTime DateTimeValue { get; set; }

        public TestEnum EnumValue { get; set; }

        /// <summary>
        /// Create TestObject with random property value.
        /// </summary>
        /// <returns></returns>
        public static TestObject NewRandom()
        {
            Random random = new Random();
            TestObject obj = new TestObject();
            obj.StringValue = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray());
            obj.IntValue = random.Next();
            obj.LongValue = random.Next();
            obj.DecimalValue = Math.Round(Convert.ToDecimal(random.NextDouble()), 4);
            obj.BoolValue = random.Next(100) < 50;
            DateTime now = DateTime.Now;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond));
            //Console.Out.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss:fffffff"));
            obj.DateTimeValue = now;
            obj.EnumValue = (TestEnum)Enum.Parse(typeof(TestEnum), random.Next(1, 3).ToString());
            return obj;
        }
    }
}
