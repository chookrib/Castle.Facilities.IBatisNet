using System;
using System.Xml;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Facilities;

using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

namespace Castle.Facilities.IBatisNet
{
    public class SqlMapActivator : AbstractComponentActivator
    {
        public SqlMapActivator(ComponentModel model, IKernelInternal kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction)
            : base(model, kernel, onCreation, onDestruction) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override object InternalCreate(CreationContext context)
        {
            String fileName = (String)Model.ExtendedProperties[IBatisNetFacility.MAPPER_CONFIG_FILE];
            bool isEmbedded = (bool)Model.ExtendedProperties[IBatisNetFacility.MAPPER_CONFIG_EMBEDDED];
            String connectionString = (String)Model.ExtendedProperties[IBatisNetFacility.MAPPER_CONFIG_CONNECTION_STRING];

            DomSqlMapBuilder domSqlMapBuilder = new DomSqlMapBuilder();
            ISqlMapper sqlMapper;

            if (isEmbedded)
            {
                XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument(fileName);
                sqlMapper = domSqlMapBuilder.Configure(sqlMapConfig);
            }
            else
            {
                sqlMapper = domSqlMapBuilder.Configure(fileName);
            }

            if (connectionString != null && connectionString.Length > 0)
            {
                sqlMapper.DataSource.ConnectionString = connectionString;
            }

            if (sqlMapper != null)
            {
                return sqlMapper;
            }
            else
            {
                throw new FacilityException(
                    string.Format("The IBatisNet Facility was unable to successfully configure SqlMapper ID [{0}] with File [{1}] that was set to Embedded [{2}]."
                    , Model.Name, Model.ExtendedProperties[IBatisNetFacility.MAPPER_CONFIG_FILE].ToString()
                    , Model.ExtendedProperties[IBatisNetFacility.MAPPER_CONFIG_EMBEDDED].ToString()));
            }
        }

        protected override void InternalDestroy(object instance) { }
    }
}
