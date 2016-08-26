using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Core.Interfaces
{
    public interface IUnityConfigHandler
    {
        void ConfigureContainer(IUnityContainer container);
    }
}