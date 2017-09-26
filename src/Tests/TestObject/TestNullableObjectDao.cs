using System.Collections;
using System.Collections.Generic;

using IBatisNet.DataMapper;

namespace Tests
{
    public class TestNullableObjectDao
    {
        private ISqlMapper _sqlMapper;

        public TestNullableObjectDao(ISqlMapper sqlMapper)
        {
            _sqlMapper = sqlMapper;
        }

        public void Insert(TestNullableObject testNullableObject)
        {
            _sqlMapper.Insert("TestNullableObject.Insert", testNullableObject);
        }

        public void Update(TestNullableObject testNullableObject)
        {
            _sqlMapper.Update("TestNullableObject.Update", testNullableObject);
        }

        public void DeleteById(string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            _sqlMapper.Delete("TestNullableObject.Delete", ht);
        }

        public TestNullableObject SelectById(string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            return _sqlMapper.QueryForObject<TestNullableObject>("TestNullableObject.Select", ht);
        }

        public IList<TestNullableObject> SelectAll()
        {
            return _sqlMapper.QueryForList<TestNullableObject>("TestNullableObject.Select", null);
        }
    }
}
