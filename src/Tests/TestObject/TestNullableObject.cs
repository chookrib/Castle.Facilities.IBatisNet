using System;

namespace Tests
{
    /// <summary>
    /// Nullable object for test.
    /// </summary>
    public class TestNullableObject
    {
        public TestNullableObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string StringValue { get; set; }

        public int? IntValue { get; set; }

        public long? LongValue { get; set; }

        public Decimal? DecimalValue { get; set; }

        public bool? BoolValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public TestEnum? EnumValue { get; set; }
    }
}
