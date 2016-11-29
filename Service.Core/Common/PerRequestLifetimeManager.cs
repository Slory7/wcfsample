using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace Service.Core.Common
{
    /// <summary>
    /// Represents the WCF Service Instance extension of the <see cref="InstanceContext"/> class.
    /// </summary>
    class ContainerExtension : IExtension<OperationContext>
    {
        /// <summary>
        /// Backing store for relating keys to object instances for Unity.
        /// </summary>
        private Dictionary<Guid, object> instances = new Dictionary<Guid, object>();

        /// <summary>
        /// Enables an extension object to find out when it has been aggregated. Called when the extension is added to the IExtensibleObject.Extensions property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        public void Attach(OperationContext owner)
        {
        }

        /// <summary>
        /// Enables an object to find out when it is no longer aggregated. Called when an extension is removed from the IExtensibleObject.Extensions property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        public void Detach(OperationContext owner)
        {
            // If we are being detached, let's go ahead and clean up, just in case.
            List<Guid> keys = new List<Guid>(this.instances.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                this.RemoveInstance(keys[i]);
            }
        }

        /// <summary>
        /// Registers the given instance with the given key into the backing store.
        /// </summary>
        /// <param name="key">Key to associate with the object instance.</param>
        /// <param name="value">Object instance to associate with the given key in the backing store.</param>
        public void RegisterInstance(Guid key, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.instances.Add(key, value);
        }

        /// <summary>
        /// Finds the object associated with the given key.
        /// </summary>
        /// <param name="key">Key used to find the associated object instance.</param>
        /// <returns>The object instance associated with the supplied key.  If no instance is registered, null is returned.</returns>
        public object FindInstance(Guid key)
        {
            object obj = null;

            // We don't care whether or not this succeeds or fails.
            this.instances.TryGetValue(key, out obj);
            return obj;
        }

        /// <summary>
        /// Removes the given key from the backing store.  This method will also dispose of the associated object instance if it implements <see cref="System.IDisposable"/>.
        /// </summary>
        /// <param name="key">Key to remove from the backing store.</param>
        public void RemoveInstance(Guid key)
        {
            // We don't want to use FindInstance JUST IN CASE somehow a key gets in there with a null object.
            object instance = null;

            if (this.instances.ContainsKey(key))
            {
                // Get the instance.
                instance = this.instances[key];

                // See if it needs disposing.
                IDisposable disposable = instance as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                // Remove it from the instances list.
                this.instances.Remove(key);
            }
        }
    }

    /// <summary>
    /// Represents the lifetime manager which controls the lifetime of the instances based
    /// on each WCF call.
    /// </summary>
    public class PerRequestLifetimeManager : LifetimeManager
    {
        #region Private Fields
        private readonly Guid key = Guid.NewGuid();
        #endregion

        /// <summary>
        /// Initializes a new instance of <c>WcfPerRequestLifetimeManager</c> class.
        /// </summary>
        public PerRequestLifetimeManager() : this(Guid.NewGuid()) { }

        PerRequestLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
                throw new ArgumentException("Key is empty.");

            this.key = key;
        }

        #region Public Methods
        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>The object desired, or null if no such object is currently stored.</returns>
        public override object GetValue()
        {
            object result = null;

            //Get object depending on  execution environment ( WCF without HttpContext,HttpContext or CallContext)

            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    result = containerExtension.FindInstance(key);
                }
            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[key.ToString()] != null)
                    result = HttpContext.Current.Items[key.ToString()];
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                result = CallContext.GetData(key.ToString());
            }

            return result;
        }
        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                   containerExtension.RemoveInstance(key);

            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[key.ToString()] != null)
                    HttpContext.Current.Items[key.ToString()] = null;
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                CallContext.FreeNamedDataSlot(key.ToString());
            }
        }
        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension == null)
                {
                    containerExtension = new ContainerExtension();

                    OperationContext.Current.Extensions.Add(containerExtension);
                }
                containerExtension.RegisterInstance(key, newValue);
            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[key.ToString()] == null)
                    HttpContext.Current.Items[key.ToString()] = newValue;
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                CallContext.SetData(key.ToString(), newValue);
            }
        }
        #endregion
    }
}
