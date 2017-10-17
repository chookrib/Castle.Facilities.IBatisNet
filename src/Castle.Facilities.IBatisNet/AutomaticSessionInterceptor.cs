using System;
using System.Reflection;

using Castle.DynamicProxy;
using Castle.MicroKernel;

using IBatisNet.Common;
using IBatisNet.Common.Logging;
using IBatisNet.DataMapper;

namespace Castle.Facilities.IBatisNet
{
    public class AutomaticSessionInterceptor : IInterceptor
    {
        private IKernel _kernel = null;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <param name="kernel"></param>
        public AutomaticSessionInterceptor(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Intercept(IInvocation invocation)
        {
            //if (_logger.IsDebugEnabled)
            //    _logger.Debug("Intercept :" + invocation.Method.Name);

            MethodInfo info = invocation.MethodInvocationTarget;

            if (!info.IsDefined(typeof(TransactionAttribute), true))
            {
                invocation.Proceed();
                return;
            }

            String key = ObtainSqlMapKeyFor(info);
            ISqlMapper sqlMap = ObtainSqlMapperFor(key);

            //for nested transaction.
            if (sqlMap.IsSessionStarted)
            {
                invocation.Proceed();
                return;
            }
            //=================================================================

            //if (_logger.IsDebugEnabled)
            //    _logger.Debug("Automatic transaction :" + invocation.Method.Name);

            //=================================================================
            //Use this code will catch exception, application not except this.
            //try
            //{
            //    if (_logger.IsDebugEnabled)
            //        _logger.Debug("BeginTransaction :" + invocation.Method.Name);

            //    sqlMap.BeginTransaction();

            //    invocation.Proceed();

            //    if (_logger.IsDebugEnabled)
            //        _logger.Debug("CommitTransaction :" + invocation.Method.Name);

            //    sqlMap.CommitTransaction();
            //}
            //catch
            //{
            //    if (_logger.IsDebugEnabled)
            //        _logger.Debug("RollBackTransaction :" + invocation.Method.Name);

            //    sqlMap.RollBackTransaction();
            //}
            //finally
            //{
            //    if (_logger.IsDebugEnabled)
            //        _logger.Debug("Close connection on method :" + invocation.Method.Name);

            //    sqlMap.CloseConnection();
            //}
            //=================================================================

            //=================================================================
            //Can't use commented code below, test faild.
            //sqlMap.OpenConnection();
            //try
            //{
            using (IDalSession session = sqlMap.BeginTransaction())
            {
                invocation.Proceed();
                session.Complete();
            }
            //}
            //finally
            //{
            //    if (_logger.IsDebugEnabled)
            //        _logger.Debug("Close connection on method :" + invocation.Method.Name);

            //    sqlMap.CloseConnection();
            //}
            //=================================================================
        }

        protected String ObtainSqlMapKeyFor(MethodInfo info)
        {
            String sqlMapID = String.Empty;

            if (info.IsDefined(typeof(TransactionAttribute), true))
            {
                TransactionAttribute[] attributs = info.GetCustomAttributes(typeof(TransactionAttribute), true) as TransactionAttribute[];
                sqlMapID = attributs[0].SqlMapId;
            }

            return sqlMapID;
        }

        protected ISqlMapper ObtainSqlMapperFor(String key)
        {
            if (string.IsNullOrEmpty(key))
                return _kernel.Resolve<ISqlMapper>();
            else
                return (ISqlMapper)_kernel.Resolve(key, typeof(ISqlMapper));
        }
    }
}
