using System;

namespace Castle.Facilities.IBatisNet
{
    /// <summary>
    /// Declares that a component wants to use a specific IBatis DataMapper Instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        private String _sqlMapId;

        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionAttribute() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sqlMapId"></param>
        public TransactionAttribute(String sqlMapId)
        {
            _sqlMapId = sqlMapId;
        }

        /// <summary>
        /// 
        /// </summary>
        public String SqlMapId
        {
            get { return _sqlMapId; }
        }
    }
}
