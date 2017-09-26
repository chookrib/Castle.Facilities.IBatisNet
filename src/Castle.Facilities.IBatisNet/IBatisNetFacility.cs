using System;
using System.Configuration;
using System.Reflection;

using Castle.Core.Configuration;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

using IBatisNet.Common.Logging;
using IBatisNet.DataMapper;

namespace Castle.Facilities.IBatisNet
{
    public class IBatisNetFacility : AbstractFacility
    {
        public static readonly String MAPPER_CONFIG_FILE = "_IBATIS_MAPPER_CONFIG_FILE_";
        public static readonly String MAPPER_CONFIG_EMBEDDED = "_IBATIS_MAPPER_CONFIG_EMBEDDED_";
        public static readonly String MAPPER_CONFIG_CONNECTION_STRING = "_IBATIS_MAPPER_CONFIG_CONNECTIONSTRING_";

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IBatisNetFacility()
        {
        }

        #region IFacility Members

        protected override void Init()
        {
            if (FacilityConfig == null)
            {
                String message = "The IBatisNetFacility requires an external configuration";

                throw new ConfigurationErrorsException(message);
            }

            Kernel.ComponentModelBuilder.AddContributor(new AutomaticSessionInspector());
            Kernel.Register(Component.For<AutomaticSessionInterceptor>()
                //.ImplementedBy<AutomaticSessionInterceptor>()
                //.Named("IBatis.session.interceptor")
                .LifestyleTransient()
            );

            int factories = 0;

            foreach (IConfiguration factoryConfig in FacilityConfig.Children)
            {
                if (factoryConfig.Name == "sqlMap")
                {
                    ConfigureFactory(factoryConfig);
                    factories++;
                }
            }

            if (factories == 0)
            {
                String message = "You need to configure at least one sqlMap for IBatisNetFacility";
                throw new ConfigurationErrorsException(message);
            }
        }

        #endregion

        private void ConfigureFactory(IConfiguration config)
        {
            String id = config.Attributes["id"];
            if (id == string.Empty)
            {
                String message = "The IBatisNetFacility requires each SqlMapper to have an ID.";
                throw new ConfigurationErrorsException(message);
            }
            else
            {
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug(string.Format("[{0}] was specified as the SqlMapper ID.", id));
                }
            }

            String fileName = config.Attributes["config"];
            if (fileName == String.Empty)
            {
                if (_logger.IsDebugEnabled)
                {
                    _logger.Debug("No filename was specified, using [sqlMap.config].");
                }
                fileName = "sqlMap.config"; // default name
            }

            String connectionString = config.Attributes["connectionString"];

            bool isEmbedded = false;
            String embedded = config.Attributes["embedded"];
            if (embedded != null)
            {
                try
                {
                    isEmbedded = Convert.ToBoolean(embedded);
                    if (_logger.IsDebugEnabled)
                    {
                        _logger.Debug("The SqlMap.config was set to embedded.");
                    }
                }
                catch (Exception ex)
                {
                    if (_logger.IsWarnEnabled)
                    {
                        _logger.Warn(
                            string.Format(
                                "The SqlMap.config had a value set for embedded, [{0}], but it was not able to parsed as a Boolean.",
                                embedded.ToString()), ex);
                    }
                    isEmbedded = false;
                }
            }

            Kernel.Register(Component.For<ISqlMapper>()
                .Named(id)
                .ExtendedProperties(new Property(MAPPER_CONFIG_FILE, fileName), new Property(MAPPER_CONFIG_EMBEDDED, isEmbedded), new Property(MAPPER_CONFIG_CONNECTION_STRING, connectionString))
                .Activator<SqlMapActivator>()
                .LifeStyle.Singleton
            );
        }
    }
}
