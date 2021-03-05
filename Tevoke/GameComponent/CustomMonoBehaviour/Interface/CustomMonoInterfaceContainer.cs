using System.Collections.Generic;

namespace Framework
{
  /// <summary>
  /// Handles interfaces Implemented by other class
  /// </summary>
  public class CustomMonoInterfaceContainer : Singleton<CustomMonoInterfaceContainer>
  {
    #region Properties    
    public List<IPointerDown> PointerDownList { get { return pointerDownList; } }
    public List<IPointerHold> PointerHoldList { get { return pointerHoldList; } }
    public List<IPointerUp> PointerUpList { get { return pointerUpList; } }

    #endregion

    #region Private Fields
    private List<IPointerDown> pointerDownList;
    private List<IPointerHold> pointerHoldList;
    private List<IPointerUp> pointerUpList;

    #endregion

    #region public Methods
    public void AddInterface(object interfaceRef)
    {
      if (interfaceRef is IPointerDown IPointerDownInterface)
        RegisterIPointerDownInterface(IPointerDownInterface);

      if (interfaceRef is IPointerHold IPointerHoldInterface)
        RegisterIPointerHoldInterface(IPointerHoldInterface);

      if (interfaceRef is IPointerUp IPointerUpInterface)
        RegisterIPointerUpInterface(IPointerUpInterface);
    }
    public void RemoveInterface(object interfaceRef)
    {
      if (interfaceRef is IPointerDown IPointerDownInterface)
        RemoveIPointerDownInterface(IPointerDownInterface);

      if (interfaceRef is IPointerHold IPointerHoldInterface)
        RemoveIPointerHoldInterface(IPointerHoldInterface);

      if (interfaceRef is IPointerUp IPointerUpInterface)
        RemoveIPointerUpInterface(IPointerUpInterface);
    }
    #endregion

    #region Interface Registration
    private void RegisterIPointerDownInterface(IPointerDown interfaceRef)
    {
      if (pointerDownList == null)
        pointerDownList = new List<IPointerDown>(1);

      pointerDownList.Add(interfaceRef);
    }
    private void RegisterIPointerHoldInterface(IPointerHold interfaceRef)
    {
      if (pointerHoldList == null)
        pointerHoldList = new List<IPointerHold>(1);

      pointerHoldList.Add(interfaceRef);
    }
    private void RegisterIPointerUpInterface(IPointerUp interfaceRef)
    {
      if (pointerUpList == null)
        pointerUpList = new List<IPointerUp>();

      pointerUpList.Add(interfaceRef);
    }
    #endregion

    #region Interface Remove
    private void RemoveIPointerDownInterface(IPointerDown interfaceRef)
    {
      if (pointerDownList != null)
        pointerDownList.Remove(interfaceRef);
    }
    private void RemoveIPointerHoldInterface(IPointerHold interfaceRef)
    {
      if (pointerHoldList != null)
        pointerHoldList.Remove(interfaceRef);
    }
    private void RemoveIPointerUpInterface(IPointerUp interfaceRef)
    {
      if (pointerUpList != null)
        pointerUpList.Remove(interfaceRef);
    }
    #endregion
  }
}
