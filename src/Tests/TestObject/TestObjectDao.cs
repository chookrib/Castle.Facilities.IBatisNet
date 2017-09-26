using System.Collections;
using System.Collections.Generic;

using IBatisNet.DataMapper;

namespace Tests
{
    public class TestObjectDao
    {
        private ISqlMapper _sqlMapper;

        public TestObjectDao(ISqlMapper sqlMapper)
        {
            _sqlMapper = sqlMapper;
        }

        public void Insert(TestObject testObject)
        {
            _sqlMapper.Insert("TestObject.Insert", testObject);
        }

        public void Update(TestObject testObject)
        {
            _sqlMapper.Update("TestObject.Update", testObject);
        }

        public void DeleteById(string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            _sqlMapper.Delete("TestObject.Delete", ht);
        }

        public TestObject SelectById(string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            return _sqlMapper.QueryForObject<TestObject>("TestObject.Select", ht);
        }

        public IList<TestObject> SelectAll()
        {
            return _sqlMapper.QueryForList<TestObject>("TestObject.Select", null);
        }
    }
}
