using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace bankLedger.Web.App_Start
{
    public sealed class StructureMapResolver : IDependencyResolver
    {
        public StructureMapResolver(IContainer container)
        {
            m_container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
                return m_container.TryGetInstance(serviceType);

            return m_container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return m_container.GetAllInstances(serviceType).Cast<object>();
        }

        readonly IContainer m_container;
    }
}
