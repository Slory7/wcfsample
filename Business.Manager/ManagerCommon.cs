using Business.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Business.Manager
{
    public class ManagerCommon : IManagerCommon
    {
        public void DoSomeThing()
        {
            //try to crash app
            //for (var i = 0; i < 10; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(c =>
            //    {
            //        Thread.Sleep(10 * 1000);
            //        throw new Exception("Crashed!");
            //    });
            //}

        }
    }
}