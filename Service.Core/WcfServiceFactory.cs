using Microsoft.Practices.Unity;
using System.Reflection;
using Unity.Wcf;
using System.Linq;
using Repository.Pattern.Interface;
using Repository.Pattern;
using System;
using Service.Core.Interfaces;
using Framework.Core.Reflection;

namespace Service.Core
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            UnityConfig.ConfigureContainer(container);
        }
    }
}