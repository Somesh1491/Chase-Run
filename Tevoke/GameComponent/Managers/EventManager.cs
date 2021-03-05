using System;
using System.Collections.Generic;

namespace Framework
{
  public class EventManager : Singleton<EventManager>
  {
    #region Private Fields
    private readonly Dictionary<Type, List<Action<object>>> subscribersTable;
    #endregion

    #region Constructor
    public EventManager()
    {
      subscribersTable = new Dictionary<Type, List<Action<object>>>();
    }
    #endregion

    #region Event
    public void AddListener<TSignal>(Action<object> callBackHandler)
    {
      var key = typeof(TSignal);
      var handlers = GetHandler(key);

      if (handlers == null)
      {
        handlers = new List<Action<object>>(1);
        subscribersTable.Add(key, handlers);
      }

      handlers.Add(callBackHandler);
    }


    public void RemoveListener<TSignal>(Action<object> callBackHandler)
    {
      Type key = typeof(TSignal);
      var handler = GetHandler(key);
      handler?.Remove(callBackHandler);
      CheckToRemoveKey(key);
    }

    public void Trigger<TSignal>(TSignal signal)
    {
      Type key = typeof(TSignal);
      var handler = GetHandler(key)?.GetEnumerator();
      if (handler != null)
      {
        InvokeSignal(handler, signal);
      }
    }
    #endregion

    #region Private Methods
    private List<Action<object>> GetHandler(Type key)
    {
      if (subscribersTable.TryGetValue(key, out List<Action<object>> handler))
      {
        return handler;
      }

      return default;
    }

    private void InvokeSignal(IEnumerator<Action<object>> handlers, object signal)
    {
      while (handlers.MoveNext())
      {
        handlers.Current?.Invoke(signal);
      }
    }

    private void CheckToRemoveKey(Type key)
    {
      if (subscribersTable.TryGetValue(key, out List<Action<object>> handlers))
      {
        if (handlers.Count <= 0)
        {
          subscribersTable.Remove(key);
        }
      }
    }
    #endregion
  }
}
