using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework
{
  public abstract class MainThread : MonoBehaviour
  {
    #region MonoBehaviour Methods
    public virtual void Awake()
    {

    }
    public virtual void OnEnable()
    {
      List<CustomMonoBehaviour> customMonoBehaviours = CustomMonoBehaviourContainer.Instance.CustomMonoBehaviourList;

      if (customMonoBehaviours == null)
        return;

      foreach (var reference in customMonoBehaviours.ToList())
        reference.OnEnable();
    }
    public virtual void OnDisable()
    {
      List<CustomMonoBehaviour> customMonoBehaviours = CustomMonoBehaviourContainer.Instance.CustomMonoBehaviourList;

      if (customMonoBehaviours == null)
        return;

      foreach (var reference in customMonoBehaviours.ToList())
        reference.OnDisable();
    }
    public virtual void Start()
    {
      List<CustomBehaviour> customBehaviours = CustomBehaviourContainer.Instance.CustomBehaviourList;

      if (customBehaviours == null)
        return;

      foreach (var reference in customBehaviours.ToList())
        reference.Start();
    }
    public virtual void OnDestroy()
    {
      List<CustomMonoBehaviour> customMonoBehaviours = CustomMonoBehaviourContainer.Instance.CustomMonoBehaviourList;

      if (customMonoBehaviours == null)
        return;

      foreach (var reference in customMonoBehaviours.ToList())
        reference.OnDestroy();
    }
    public virtual void Update()
    {
      List<CustomBehaviour> customBehaviours = CustomBehaviourContainer.Instance.CustomBehaviourList;

      if (customBehaviours == null)
        return;

      foreach (var reference in customBehaviours.ToList())
        reference.Update();

      //Mouse Events
      if (Input.GetMouseButtonDown(0))
      {
        IPointerDownCall(Input.mousePosition);
      }

      else if (Input.GetMouseButton(0))
      {
        IPointerHoldCall(Input.mousePosition);
      }

      else if (Input.GetMouseButtonUp(0))
      {
        IPointerUpCall(Input.mousePosition);
      }
    }
    public virtual void LateUpdate()
    {
      List<CustomBehaviour> customBehaviours = CustomBehaviourContainer.Instance.CustomBehaviourList;

      if (customBehaviours == null)
        return;

      foreach (var reference in customBehaviours.ToList())
        reference.LateUpdate();
    }
    public virtual void FixedUpdate()
    {
      List<CustomBehaviour> customBehaviours = CustomBehaviourContainer.Instance.CustomBehaviourList;

      if (customBehaviours == null)
        return;

      foreach (var reference in customBehaviours.ToList())
        reference.FixedUpdate();
    }
    #endregion

    #region Interface Methods Calling    
    private void IPointerDownCall(Vector2 position)
    {
      List<IPointerDown> interfaceList = CustomMonoInterfaceContainer.Instance.PointerDownList;

      if (interfaceList == null)
        return;

      foreach (var reference in interfaceList)
      {
        reference.OnPointerDown(position);
      }
    }
    private void IPointerHoldCall(Vector2 position)
    {
      List<IPointerHold> interfaceList = CustomMonoInterfaceContainer.Instance.PointerHoldList;

      if (interfaceList == null)
        return;

      foreach (var reference in interfaceList)
      {
        reference.OnPointerHold(position);
      }
    }
    private void IPointerUpCall(Vector2 position)
    {
      List<IPointerUp> interfaceList = CustomMonoInterfaceContainer.Instance.PointerUpList;

      if (interfaceList == null)
        return;

      foreach (var reference in interfaceList)
      {
        reference.OnPointerUp(position);
      }
    }
    #endregion
  }
}
