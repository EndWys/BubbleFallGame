using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public interface IService { }
    public class ServiceLocator
    {
        public static ServiceLocator Local { get; private set; }

        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static ServiceLocator CreateLocalSceneServiceLocator()
        {
            if (Local == null)
                Local = new ServiceLocator();

            return Local;
        }

        public void Register<T>(T service) where T : IService
        {
            var key = typeof(T);
            if (_services.ContainsKey(key))
            {
                return;
            }

            _services.Add(key, service);

            return;
        }

        public void Unregister<T>() where T : IService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                return;
            }

            _services.Remove(key);
        }

        public T Get<T>() where T : IService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered service!");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }
    }
}