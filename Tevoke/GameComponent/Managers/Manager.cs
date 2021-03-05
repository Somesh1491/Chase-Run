using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
  public class Manager : CustomMonoBehaviour
  {
    private static readonly Dictionary<Type, Manager> managers = new Dictionary<Type, Manager>();
    public static void CreateManager<T>() where T : Manager, new()
    {
      //Check for manager existence
      if (managers.ContainsKey(typeof(T)))
      {
        Debug.LogError("Can't create more than one Manager of same type : Manager of type " + typeof(T) + " already exist");
        return;
      }

      managers.Add(typeof(T), new T());
    }
    public static void DestroyAllManager()
    {
      managers.Clear();
    }
    protected static T GetManager<T>() where T : Manager, new()
    {
      //Cheack for key existence
      var manager = GetHandler(typeof(T));
      if (manager == null)
      {
        Debug.LogError("Manager of Type " + typeof(T) + " not exist");
        return null;
      }

      return managers[typeof(T)] as T;
    }
    private static Manager GetHandler(Type key)
    {
      if (managers.TryGetValue(key, out Manager handler))
      {
        return handler;
      }

      return default;
    }
  }
}
