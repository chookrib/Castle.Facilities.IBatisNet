using System;

namespace Castle.Facilities.IBatisNet
{
    /// <summary>
    /// Indicates that the target class wants to use the transactional services.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TransactionalAttribute : System.Attribute
    {
    }
}
