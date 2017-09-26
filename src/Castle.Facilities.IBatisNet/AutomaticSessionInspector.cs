using System;
using System.Collections;
using System.Reflection;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.ModelBuilder;

namespace Castle.Facilities.IBatisNet
{
    public class AutomaticSessionInspector : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (model.Implementation.IsDefined(typeof(TransactionalAttribute), true))
            {
                ArrayList problematicMethods = new ArrayList();
                foreach (MethodInfo method in model.Implementation.GetMethods())
                {
                    if (method.IsDefined(typeof(TransactionAttribute), true) && !method.IsVirtual)
                        problematicMethods.Add(method.Name);
                }

                if (problematicMethods.Count != 0)
                {
                    String[] methodNames = (String[]) problematicMethods.ToArray(typeof(String));
                    String message = String.Format("The class {0} wants to use transaction interception, " +
                                                   "however the methods must be marked as virtual in order to do so. Please correct " +
                                                   "the following methods: {1}", model.Implementation.FullName,
                        String.Join(", ", methodNames));

                    throw new FacilityException(message);
                }

                model.Interceptors.Add(new InterceptorReference(typeof(AutomaticSessionInterceptor)));
            }
        }
    }
}
