using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
  public class ContainerUtility
  {
    private static readonly Dictionary<Type, Container> containers = new Dictionary<Type, Container>();

    public static void CreateContainer<T>() where T : Container, new()
    {
      //Check for container existence
      if (containers.ContainsKey(typeof(T)))
      {
        Debug.LogError("Can't create more than one Container of same type : Container of type " + typeof(T) + " already exist");
        return;
      }

      containers.Add(typeof(T), new T());
    }

    public static void DestroyAllContainer()
    {
      containers.Clear();
    }
    public static T GetContainer<T>() where T : Container, new()
    {

      //Cheack for key existence
      var container = GetHandler(typeof(T));
      if (container == null)
      {
        Debug.LogError("Container of Type " + typeof(T) + " not exist");
        return null;
      }

      return containers[typeof(T)] as T;
    }
    private static Container GetHandler(Type key)
    {
      if (containers.TryGetValue(key, out Container handler))
      {
        return handler;
      }

      return default;
    }
  }
}
