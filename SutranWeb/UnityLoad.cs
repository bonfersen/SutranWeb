using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace SutranWeb
{
    public class UnityLoad<I>
    {
        public static Object getUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            var impl = section.Configure(container).Resolve<I>();

            return impl;
        }
    }
}