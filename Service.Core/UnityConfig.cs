using Framework.Core.Reflection;
using Microsoft.Practices.Unity;
using Repository.Pattern;
using Repository.Pattern.Interface;
using Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Core
{
    public class UnityConfig
    {
        public static void ConfigureContainer(IUnityContainer container)
        {
            // register all your components with the container here
            container
               .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
               .RegisterType<IReadOnlyUnitOfWork, ReadOnlyUnitOfWork>(new HierarchicalLifetimeManager())
               //.RegisterType<DataContext>(new HierarchicalLifetimeManager());
               ;

            //dynamic register components by IUnityConfigHandler type
            TypeLocator objTypeLocator = new TypeLocator();
            var types = objTypeLocator.GetAllMatchingTypes((t) =>
            {
                return t.IsClass && !t.IsAbstract && t.IsVisible && typeof(IUnityConfigHandler).IsAssignableFrom(t);
            });
            foreach (var configType in types)
            {
                var configHandler = Activator.CreateInstance(configType) as IUnityConfigHandler;
                configHandler.ConfigureContainer(container);
            }

            ServiceGlobals.UnityContainer = container;
        }
    }
}
